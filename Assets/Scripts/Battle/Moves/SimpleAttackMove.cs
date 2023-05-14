using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleAttackMove : AttackMove
{
    protected bool requiresInvulnerabilityCheck = true;
    protected bool requiresAccuracyCheck = true;

    protected bool sapsHealth = false;

    protected bool dealsFixedDamage = false;
    protected int fixedDamage = 0;

    protected SecondaryEffects secondaryEffect = SecondaryEffects.None;
    protected int secondaryEffectChance = 0;

    protected bool hasRecoil = false;

    /// <summary>
    /// Creates an attack move that only hits the target once.
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="category">The category of this move. (Physical or Special)</param>
    /// <param name="priority">This move's priority. Higher priority goes first.</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="accuracy">The accuracy of this move. Min: 30, Max: 100, Increment: 5</param>
    /// <param name="power">The power of this move. Increment: 5</param>
    /// <param name="highCrit">Whether or not this move has a high critical hit ratio</param>
    /// <param name="battle">A reference to the current battle this move is in.</param>
    protected SimpleAttackMove(string name, Type type, Category category, int basePP, int accuracy, int power,
        BattleStateManager battle, int priority = 0, bool highCrit = false)
        : base(name, type, category, basePP, accuracy, power, battle, priority: priority, highCrit: highCrit) { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        opponent.LastDamageRecieved = 0;

        // Invulnerability and Accuracy Checks
        bool missed = requiresInvulnerabilityCheck && opponent.IsSemiInvulnerable;
        if(!missed)
            missed = requiresAccuracyCheck && !AccuracyCheck(user, opponent);

        if (missed)
        {
            yield return Battle.StartCoroutine(OnMissed(user));
            EndMove(user, opponent);
        }

        // No Effect Check
        if(MoveData.HasNoEffect(this, opponent))
        {
            yield return Battle.StartCoroutine(OnDoesNotAffect());
            EndMove(user, opponent);
        }

        // Hit
        if (dealsFixedDamage)
            yield return Battle.StartCoroutine(DealFixedDamage(user, opponent));
        else if (sapsHealth)
            yield return Battle.StartCoroutine(DealDamageAndRegainHealth(user, opponent));
        else
        {
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
            // Recoil Damage Check
            if(hasRecoil)
            {
                int recoilDamage = Mathf.Max(1, opponent.LastDamageRecieved / 4);
                yield return Battle.StartCoroutine(user.RecieveDamge(recoilDamage, Type));
                yield return Battle.StartCoroutine(OnRecoil(user));
            }
        }

        // Secondary Effect Check
        bool hasSecondaryEffect = secondaryEffect != SecondaryEffects.None;
        if(hasSecondaryEffect)
        {
            bool isOpponentAlive = opponent.Status != StatusEffect.FNT;
            // Replace with actual check
            int random = Random.Range(0, 100);
            bool effectHits = random < secondaryEffectChance;
            if(isOpponentAlive && effectHits)
                yield return Battle.StartCoroutine(ApplySecondaryEffect(secondaryEffect, opponent));
        }

        EndMove(user, opponent);
    }

    /* One-Hit KO moves automatically fail if the user's speed stat is lower than the target
     * These moves always deal damage equal to the opponent's remaining health */
    protected IEnumerator ExecuteOneHitKO(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        opponent.LastDamageRecieved = 0;

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
            yield return Battle.StartCoroutine(OnMissed(user));
        else if (opponent.Stats.Speed > user.Stats.Speed)
            yield return Battle.StartCoroutine(OnFailed());
        else if (MoveData.HasNoEffect(this, opponent))
            yield return Battle.StartCoroutine(OnDoesNotAffect());
        else
        {
            int damage = opponent.CurrentHP;
            yield return Battle.StartCoroutine(opponent.RecieveDamge(damage, Type));
            opponent.LastDamageRecieved = damage;
            yield return new WaitForSeconds(60 / 60f);
        }

        EndMove(user, opponent);
    }

    protected IEnumerator ExecuteExplosiveMove(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        opponent.LastDamageRecieved = 0;

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
            yield return Battle.StartCoroutine(OnMissed(user));
        else if (MoveData.HasNoEffect(this, opponent))
            yield return Battle.StartCoroutine(OnDoesNotAffect());
        else
            yield return Battle.StartCoroutine(DealDamage(user, opponent));

        yield return Battle.StartCoroutine(user.Explode());
        EndMove(user, opponent);
    }

    /* Calculates the amount of damage to deal to the user, then deals it.
     * The user is healed for half the amount of damage that was dealt.
     * The user is NOT healed if this move broke the target's substitute. */
    protected IEnumerator DealDamageAndRegainHealth(BattlePokemon user, BattlePokemon target)
    {
        int remainingHP = target.CurrentHP;
        float effectiveness = MoveData.GetMatchupMultiplier(this, target);
        int damage = MoveData.CalculateDamage(this, user, target, out bool isCrit);
        target.LastDamageRecieved = damage;

        yield return Battle.StartCoroutine(target.RecieveDamge(damage, Type));
        if (isCrit)
            yield return Battle.StartCoroutine(OnCriticalHit());
        yield return Battle.StartCoroutine(OnEffectiveness(effectiveness));

        // ADD HERE: Check for substitute break. Do NOT regain health if substitute breaks

        int sappedHealth = Mathf.CeilToInt(Mathf.Min(damage, remainingHP) / 2f);
        yield return Battle.StartCoroutine(user.RestoreHealth(sappedHealth));
        yield return new WaitForSeconds(20 / 60f);
        yield return Battle.StartCoroutine(OnHealthSapped(target));
    }

    protected IEnumerator DealFixedDamage(BattlePokemon user, BattlePokemon target)
    {
        // -1 fixedDamage means the damage should equal the user's level
        int damage = fixedDamage == -1 ? user.Level : fixedDamage;
        target.LastDamageRecieved = damage;

        yield return Battle.StartCoroutine(target.RecieveDamge(damage, Type));
        yield return new WaitForSeconds(60 / 60f);
    }

    protected IEnumerator ApplySecondaryEffect(SecondaryEffects effect, BattlePokemon opponent)
    {
        switch(effect)
        {
            default: yield break;
            case SecondaryEffects.LowerAttack:
                if (opponent.CanStatBeLowered(StatType.ATTACK))
                {
                    opponent.ModifyStatAsSecondary(StatType.ATTACK, -1);
                    yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.ATTACK));
                }
                yield break;
            case SecondaryEffects.LowerDefense:
                if (opponent.CanStatBeLowered(StatType.DEFENSE))
                {
                    opponent.ModifyStatAsSecondary(StatType.DEFENSE, -1);
                    yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.DEFENSE));
                }
                yield break;
            case SecondaryEffects.LowerSpecial:
                if (opponent.CanStatBeLowered(StatType.SPECIAL))
                {
                    opponent.ModifyStatAsSecondary(StatType.SPECIAL, -1);
                    yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.SPECIAL));
                }
                yield break;
            case SecondaryEffects.LowerSpeed:
                if (opponent.CanStatBeLowered(StatType.SPEED))
                {
                    opponent.ModifyStatAsSecondary(StatType.SPEED, -1);
                    yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.SPEED));
                }
                yield break;
            case SecondaryEffects.Flinch:
                opponent.Flinch();
                yield break;
            case SecondaryEffects.Confusion:
                opponent.Confuse();
                yield return Battle.StartCoroutine(OnConfused(opponent));
                yield break;
            case SecondaryEffects.Freeze:
                opponent.Freeze();
                yield return Battle.StartCoroutine(OnFrozen(opponent));
                yield break;
            case SecondaryEffects.Burn:
                opponent.Burn();
                yield return Battle.StartCoroutine(OnBurned(opponent));
                yield break;
            case SecondaryEffects.Poison:
                opponent.Poison();
                yield return Battle.StartCoroutine(OnPoisoned(opponent));
                yield break;
            case SecondaryEffects.Paralysis:
                opponent.Paralyze();
                yield return Battle.StartCoroutine(OnParalyzed(opponent));
                yield break;
            case SecondaryEffects.Sleep:
                opponent.Sleep();
                yield return Battle.StartCoroutine(OnSlept(opponent));
                yield break;
        }
    }

    protected void EndMove(BattlePokemon user, BattlePokemon opponent)
    {
        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
        Battle.StopCoroutine(Execute(user, opponent));
    }
}

public enum SecondaryEffects
{
    None,
    LowerAttack,
    LowerDefense,
    LowerSpecial,
    LowerSpeed,
    Flinch,
    Confusion,
    Freeze,
    Burn,
    Poison,
    Paralysis,
    Sleep
}