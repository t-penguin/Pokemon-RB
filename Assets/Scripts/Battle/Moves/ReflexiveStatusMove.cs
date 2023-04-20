using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReflexiveStatusMove : BaseMove
{
    protected ReflexiveStatusEffect Effect = ReflexiveStatusEffect.None;
    protected bool greatlyRaiseStat = false;

    /// <summary>
    /// Creates a status move that affects only the user.
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="battle">A reference to the battle this move is in.</param>
    protected ReflexiveStatusMove(string name, Type type, int basePP, BattleStateManager battle)
        : base(name, type, Category.Status, 0, basePP, battle) { }

    // Executes the user's move
    protected virtual IEnumerator Execute(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        yield return Battle.StartCoroutine(ApplyReflexiveEffect(user));

        SetLastMoveUsed(user);
        CurrentPP--;
    }

    /* Passes execution to the single parameter Execute method
     * as most reflexive moves only need a reference to the user.*/
    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(Execute(user));
    }

    private IEnumerator ApplyReflexiveEffect(BattlePokemon user)
    {
        switch (Effect)
        {
            case ReflexiveStatusEffect.RaiseAttack:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Attack));
                yield break;
            case ReflexiveStatusEffect.RaiseDefense:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Defense));
                yield break;
            case ReflexiveStatusEffect.RaiseSpecial:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Special));
                yield break;
            case ReflexiveStatusEffect.RaiseSpeed:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Speed));
                yield break;
            case ReflexiveStatusEffect.RaiseEvasion:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Evasion));
                yield break;
        }
    }

    private IEnumerator RaiseStatAttempt(BattlePokemon user, StatType stat)
    {
        if(user.CanStatBeRaised(stat))
        {
            int statChange = greatlyRaiseStat ? 2 : 1;
            user.ModifyStatAsPrimary(stat, statChange);
            yield return Battle.StartCoroutine(OnRaisedStat(user, stat, greatlyRaiseStat));
        }
        else
            yield return Battle.StartCoroutine(OnFailed());
    }
}

public enum ReflexiveStatusEffect
{
    None,
    RaiseAttack,
    RaiseDefense,
    RaiseSpecial,
    RaiseSpeed,
    RaiseEvasion
}