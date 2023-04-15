using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleAttackMove : AttackMove
{
    protected bool requiresInvulnerabilityCheck = true;
    protected bool requiresAccuracyCheck = true;
    protected bool requiresFailureCheck = false;

    protected bool sapsHealth = false;

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
            yield return Battle.StartCoroutine(OnNoEffect());
            EndMove(user, opponent);
        }

        // Failure Check
        if(requiresFailureCheck)
        {
            
        }

        // Hit
        if (sapsHealth)
        {
            yield return Battle.StartCoroutine(DealDamageAndRegainHealth(user, opponent));
            yield return Battle.StartCoroutine(OnHealthSapped(opponent));
        }
        else
            yield return Battle.StartCoroutine(DealDamage(user, opponent));

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
     * These moves always deal 65535 damage */
    protected IEnumerator ExecuteOneHitKO(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
            yield return Battle.StartCoroutine(OnMissed(user));
        else if (opponent.Stats.Speed > user.Stats.Speed)
            yield return Battle.StartCoroutine(OnFailed());
        else if (MoveData.HasNoEffect(this, opponent))
            yield return Battle.StartCoroutine(OnNoEffect());
        else
        {
            yield return Battle.StartCoroutine(opponent.RecieveDamge(65535, Type));
            yield return new WaitForSeconds(60 / 60f);
        }

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
        if (target.IsBideActive)
        {
            target.LastDamageRecieved = damage;
            target.BideDamage += 2 * damage;
        }
        yield return Battle.StartCoroutine(target.RecieveDamge(damage, Type));
        if (isCrit)
            yield return Battle.StartCoroutine(OnCriticalHit());
        yield return Battle.StartCoroutine(OnEffective(effectiveness));

        // ADD HERE: Check for substitute break. Do NOT regain health if substitute breaks

        int sappedHealth = Mathf.CeilToInt(Mathf.Min(damage, remainingHP) / 2f);
        yield return Battle.StartCoroutine(user.RestoreHealth(sappedHealth));
    }

    protected IEnumerator OnHealthSapped(BattlePokemon opponent)
    {
        string text;
        if (opponent.TrainerIsPlayer)
            text = $"Sucked health from\n{opponent.Name}!";
        else
            text = $"Sucked health from\nEnemy {opponent.Name}!";

        yield return Battle.StartCoroutine(Battle.DisplayMessage(text, true));
        yield return new WaitForSeconds(4 / 60f);
    }

    protected IEnumerator ApplySecondaryEffect(SecondaryEffects effect, BattlePokemon opponent)
    {
        switch(effect)
        {
            default: yield break;
            case SecondaryEffects.LowerAttack:
                if (opponent.CanStatBeLowered(StatType.Attack))
                {
                    opponent.ModifyStatAsSecondary(StatType.Attack, -1);
                    yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.Attack));
                }
                yield break;
            case SecondaryEffects.LowerDefense:
                if (opponent.CanStatBeLowered(StatType.Defense))
                {
                    opponent.ModifyStatAsSecondary(StatType.Defense, -1);
                    yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.Defense));
                }
                yield break;
            case SecondaryEffects.LowerSpecial:
                if (opponent.CanStatBeLowered(StatType.Special))
                {
                    opponent.ModifyStatAsSecondary(StatType.Special, -1);
                    yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.Special));
                }
                yield break;
            case SecondaryEffects.LowerSpeed:
                if (opponent.CanStatBeLowered(StatType.Speed))
                {
                    opponent.ModifyStatAsSecondary(StatType.Speed, -1);
                    yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.Speed));
                }
                yield break;
            case SecondaryEffects.Flinch:
                opponent.Flinch();
                yield break;
            case SecondaryEffects.Confusion:

                yield break;
            case SecondaryEffects.Freeze:
                opponent.Freeze();
                yield break;
            case SecondaryEffects.Burn:
                opponent.Burn();
                yield break;
            case SecondaryEffects.Poison:

                yield break;
            case SecondaryEffects.Paralysis:
                
                yield break;
            case SecondaryEffects.Sleep:

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