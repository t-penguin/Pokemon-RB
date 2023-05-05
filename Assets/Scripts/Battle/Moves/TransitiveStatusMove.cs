using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitiveStatusMove : TransitiveMove
{
    protected TransitiveStatusEffect Effect = TransitiveStatusEffect.None;
    protected bool greatlyLowerStat = false;

    /// <summary>
    /// Creates a status move that affects the target.
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="accuracy">The accuracy of this move. Min: 30, Max: 100, Increment: 5</param>
    /// <param name="battle">A reference to the current battle this move is in.</param>
    protected TransitiveStatusMove(string name, Type type, int basePP, int accuracy, BattleStateManager battle)
        : base(name, type, Category.Status, 0, basePP, accuracy, battle) { }

    /* Transitive Status moves have a chance to fail when used on the player
     * check < failedthreshold -> hit
     * failedthreshold < check < missedthreshold -> fail
     * missedthreshold < check -> miss */
    protected bool AccuracyCheck(BattlePokemon user, BattlePokemon defender, out bool failed)
    {
        // Randomly generated accuracy check [0, 256)
        int check = Random.Range(0, 256);
        failed = false;

        int missThreshold = (int)(Accuracy / 100f * 255);
        missThreshold = (int)(missThreshold * user.Accuracy * defender.Evasion);

        if (defender == Battle.PlayerSide.ActivePokemon)
        {
            int failThreshold = missThreshold - 64;
            failThreshold = (int)(failThreshold * user.Accuracy * defender.Evasion);

            if (failThreshold < check && check < missThreshold)
                failed = true;

            return check < failThreshold;
        }

        return check < missThreshold;
    }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        bool failed = false;
        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent, out failed))
        {
            if (failed)
                yield return Battle.StartCoroutine(OnFailed());
            else
                yield return Battle.StartCoroutine(OnMissed(user));
        }
        else
            yield return Battle.StartCoroutine(ApplyStatusEffect(opponent));

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }

    private IEnumerator ApplyStatusEffect(BattlePokemon target)
    {
        switch(Effect)
        {
            default: yield break;
            case TransitiveStatusEffect.LowerAttack:
                yield return Battle.StartCoroutine(LowerStatAttempt(target, StatType.Attack));
                yield break;
            case TransitiveStatusEffect.LowerDefense:
                yield return Battle.StartCoroutine(LowerStatAttempt(target, StatType.Defense));
                yield break;
            case TransitiveStatusEffect.LowerSpecial:
                yield return Battle.StartCoroutine(LowerStatAttempt(target, StatType.Special));
                yield break;
            case TransitiveStatusEffect.LowerSpeed:
                yield return Battle.StartCoroutine(LowerStatAttempt(target, StatType.Speed));
                yield break;
            case TransitiveStatusEffect.LowerAccuracy:
                yield return Battle.StartCoroutine(LowerStatAttempt(target, StatType.Accuracy));
                yield break;
            case TransitiveStatusEffect.Paralyze:
                yield return Battle.StartCoroutine(ApplyNonVolatileStatus(target, StatusEffect.PAR));
                yield break;
            case TransitiveStatusEffect.Poison:
                yield return Battle.StartCoroutine(ApplyNonVolatileStatus(target, StatusEffect.PSN));
                yield break;
            case TransitiveStatusEffect.Sleep:
                yield return Battle.StartCoroutine(ApplyNonVolatileStatus(target, StatusEffect.SLP));
                yield break;
            case TransitiveStatusEffect.Confuse:
                target.Confuse();
                yield return Battle.StartCoroutine(OnConfused(target));
                yield break;
            case TransitiveStatusEffect.Seed:
                target.Seeded = true;
                yield return Battle.StartCoroutine(OnSeeded(target));
                yield break;
            case TransitiveStatusEffect.Disable:
                yield return Battle.StartCoroutine(Disable(target));
                yield break;
        }
    }

    private IEnumerator Disable(BattlePokemon target)
    {
        List<int> MovesWithPP = target.GetMovesWithPP();
        bool failed = target.Disabled || MovesWithPP.Count == 0;

        if (failed)
            yield return Battle.StartCoroutine(OnFailed());
        else
        {
            int index = Random.Range(0, MovesWithPP.Count);
            target.DisableMove(MovesWithPP[index]);
            string moveName = target.Moves[MovesWithPP[index]].Name;
            yield return Battle.StartCoroutine(OnDisabled(target, moveName));
        }
    }

    private IEnumerator LowerStatAttempt (BattlePokemon target, StatType stat)
    {
        if (target.CanStatBeLowered(stat))
        {
            int statChange = greatlyLowerStat ? -2 : -1;
            target.ModifyStatAsPrimary(stat, statChange);
            yield return Battle.StartCoroutine(OnLoweredStat(target, stat, greatlyLowerStat));
        }
        else
            yield return Battle.StartCoroutine(OnFailed());
    }

    private IEnumerator ApplyNonVolatileStatus(BattlePokemon target, StatusEffect effect)
    {
        if (target.HasNonVolatileStatus())
        {
            yield return Battle.StartCoroutine(OnFailed());
            yield break;
        }

        switch (effect)
        {
            default: yield break;
            case StatusEffect.PAR:
                target.Paralyze();
                yield return Battle.StartCoroutine(OnParalyzed(target));
                yield break;
            case StatusEffect.PSN:
                target.Poison();
                yield return Battle.StartCoroutine(OnPoisoned(target));
                yield break;
            case StatusEffect.SLP:
                target.Sleep();
                yield return Battle.StartCoroutine(OnSlept(target));
                yield break;
        }
    }

    private IEnumerator OnSeeded(BattlePokemon target)
    {
        string targetName = target == Battle.PlayerSide.ActivePokemon ? target.Name : $"Enemy {target.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{targetName}\nwas seeded!", true));
        yield return new WaitForSeconds(6 / 60f);
    }

    private IEnumerator OnDisabled(BattlePokemon target, string moveName)
    {
        string targetName = target == Battle.PlayerSide.ActivePokemon ? target.Name : $"Enemy {target.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{targetName}<\n{moveName} was\ndisabled!", true));
        yield return new WaitForSeconds(6 / 60f);
    }
}

public enum TransitiveStatusEffect
{
    None,
    LowerAttack,
    LowerDefense,
    LowerSpecial,
    LowerSpeed,
    LowerAccuracy,
    Paralyze,
    Poison,
    Sleep,
    Confuse,
    Seed,
    Disable
}