using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMove
{
    public BattleStateManager Battle { get; }
    public string Name { get; }
    public Type Type { get; }
    public Category Category { get; }
    public int Priority { get; }
    public int BasePP { get; }
    public int CurrentPP { get; protected set; }
    public int CurrentMaxPP { get; protected set; }
    public int MaxPP { get; }

    /// <summary>
    /// Base class for a Pokemon's move.
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="category">The category of this move. (Physical, Special, Status)</param>
    /// <param name="priority">This move's priority. Higher priority goes first.</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="battle">A reference to the current battle this move is in.</param>
    protected BaseMove(string name, Type type, Category category, int priority, int basePP, BattleStateManager battle)
    {
        Battle = battle;

        Name = name;
        Type = type;
        Category = category;
        Priority = priority;
        BasePP = basePP;
        CurrentPP = BasePP;
        CurrentMaxPP = BasePP;
        MaxPP = (int)(BasePP * 1.6);
    }

    // Executes the user's move, to be overriden by each move
    public abstract IEnumerator Execute(BattlePokemon user, BattlePokemon opponent);

    // Sets the user's last used move to be this one
    protected void SetLastMoveUsed(BattlePokemon user) => user.SetLastMoveUsed(this);


    #region General Messages

    /// <summary>
    /// Message for when this move is used.
    /// </summary>
    protected IEnumerator OnUsed(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display
            (BattleMessages.MOVE_USED, bPokemon: user, move: this, waitForInput: false));
    }

    /// <summary>
    /// Message for when this move misses.
    /// Displayed after the move is used.
    /// </summary>
    protected IEnumerator OnMissed(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.MOVE_MISSED, bPokemon: user));
    }

    /// <summary>
    /// Message for when this move fails.
    /// Displayed after the move is used.
    /// </summary>
    protected IEnumerator OnFailed()
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.MOVE_FAILED));
    }

    /// <summary>
    /// Message for when the target is immune to this type of move.
    /// Displayed after the move is used.
    /// </summary>
    protected IEnumerator OnDoesNotAffect(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.MOVE_DOES_NOT_AFFECT, bPokemon: target));
    }

    /// <summary>
    /// Message for when this move is super or not very effective against the target.
    /// Displayed immediately after all damage is done, after the critical hit message, if applicable.
    /// </summary>
    protected IEnumerator OnEffectiveness(float effectiveness)
    {
        if (effectiveness == 1)
            yield break;

        string text;
        if (effectiveness < 1)
            text = BattleMessages.MOVE_NOT_EFFECTIVE;
        else
            text = BattleMessages.MOVE_SUPER_EFFECTIVE;

        yield return Battle.StartCoroutine(BattleMessages.Display(text));
    }

    /// <summary>
    /// Message for when this move lands a critical hit.
    /// Displayed immediately after damage is done. </summary>
    protected IEnumerator OnCriticalHit()
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.MOVE_CRITICAL_HIT));
    }

    /// <summary>
    /// Message for when this move hits multiple times in one turn.
    /// Displayed after all the damage has been done, after effectiveness messages, if applicable.
    /// </summary>
    protected IEnumerator OnMultiHit(int hits)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.MOVE_MULTI_HIT, value: hits));
    }

    #endregion

    #region Multi Turn Attack Messages

    /// <summary>
    /// Message for when the user is charging a two-turn move.
    /// Displayed on the first turn.
    /// </summary>
    protected IEnumerator OnCharging(BattlePokemon user, string message)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(message, bPokemon: user));
    }

    /// <summary>
    /// Message for when the user is using a thrashing move.
    /// Displayed on every turn, except the first, in place of the move used message.
    /// </summary>
    protected IEnumerator OnThrashing(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.ATTACK_THRASHING, bPokemon: user));
    }

    /// <summary>
    /// Message for when the user becomes confused due to fatigue.
    /// Displayed after the last turn of a thrashing move.
    /// </summary>
    protected IEnumerator OnFatigued(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_FATIGUED, bPokemon: user));
    }

    /// <summary>
    /// Message for when the user is recharging from a powerful move.
    /// Displayed on the second turn of a recharging move.
    /// </summary>
    protected IEnumerator OnRecharging(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_RECHARGING, bPokemon: user));
    }

    /// <summary>
    /// Message for when the user is using a binding move.
    /// Displayed on every turn, except the first, in place of the move used message.
    /// </summary>
    protected IEnumerator OnAttackContinues(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.ATTACK_CONTINUES, bPokemon: user));
    }

    /// <summary>
    /// Message for when the user unleashes energy.
    /// Displayed on the last turn of Bide, in place of the move used message.
    /// </summary>
    protected IEnumerator OnUnleashedEnergy(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.ATTACK_UNLEASHED_ENERGY, bPokemon: user));
    }

    #endregion

    #region Stat Change Messages

    /// <summary>
    /// Message for when the target's stat stage is lowered.
    /// Displayed after the move is used if the stat change succeeds.
    /// </summary>
    protected IEnumerator OnLoweredStat(BattlePokemon target, StatType stat, bool greatlyLowered = false)
    {
        string text;
        if (greatlyLowered)
            text = BattleMessages.TARGET_STAT_GREATLY_LOWERED;
        else
            text = BattleMessages.TARGET_STAT_LOWERED;

        yield return Battle.StartCoroutine(BattleMessages.Display(text, bPokemon: target, stat: stat));
    }

    /// <summary>
    /// Message for when the user's stat stage is raised.
    /// Displayed after the move is used if the stat change succeeds.
    /// </summary>
    protected IEnumerator OnRaisedStat(BattlePokemon user, StatType stat, bool greatlyRaised = false)
    {
        string text;
        if (greatlyRaised)
            text = BattleMessages.USER_STAT_GREATLY_RAISED;
        else
            text = BattleMessages.USER_STAT_RAISED;

        yield return Battle.StartCoroutine(BattleMessages.Display(text, bPokemon: user, stat: stat));
    }

    /// <summary>
    /// Message for when a stat stage is attempted to be raised/lowered past it's limit.
    /// Displayed after the move is used if the stat change fails.
    /// </summary>
    protected IEnumerator OnNothingHappened()
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.MOVE_NOTHING_HAPPENED));
    }

    #endregion

    #region Status Condition Messages

    /// <summary>
    /// Message for when the target is given the paralyzed status.
    /// Displayed after the condition is given to the target.
    /// </summary>
    protected IEnumerator OnParalyzed(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.TARGET_PARALYZED, bPokemon: target));
    }

    /// <summary>
    /// Message for when the target is given the frozen status.
    /// Displayed after the condition is given to the target.
    /// </summary>
    protected IEnumerator OnFrozen(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.TARGET_FROZEN, bPokemon: target));
    }

    /// <summary>
    /// Message for when the target is given the sleeping status.
    /// Displayed after the condition is given to the target.
    /// </summary>
    protected IEnumerator OnSlept(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.TARGET_SLEPT, bPokemon: target));
    }

    /// <summary>
    /// Message for when the target is given the burned status.
    /// Displayed after the condition is given to the target.
    /// </summary>
    protected IEnumerator OnBurned(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.TARGET_BURNED, bPokemon: target));
    }

    /// <summary>
    /// Message for when the target is given the poisoned status.
    /// Displayed after the condition is given to the target.
    /// </summary>
    protected IEnumerator OnPoisoned(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.TARGET_POISONED, bPokemon: target));
    }

    /// <summary>
    /// Message for when the target is given the badly poisoned status.
    /// Displayed after the condition is given to the target.
    /// </summary>
    protected IEnumerator OnBadlyPoisoned(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.TARGET_BADLY_POISONED, bPokemon: target));
    }

    /// <summary>
    /// Message for when the target is given the confusion status.
    /// Displayed after the condition is given to the target.
    /// </summary>
    protected IEnumerator OnConfused(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.TARGET_CONFUSED, bPokemon: target));
    }

    /// <summary>
    /// Message for when the target is given the seeded status.
    /// Displayed after the condition is given to the target.
    /// </summary>
    protected IEnumerator OnSeeded(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.TARGET_SEEDED, bPokemon: target));
    }

    /// <summary>
    /// Message for when the target's move is disbaled.
    /// Displayed after the condition is given to the target.
    /// </summary>
    protected IEnumerator OnDisabled(BattlePokemon target, BaseMove move)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.TARGET_DISABLED, bPokemon: target, move: move));
    }

    /// <summary>
    /// Message for when the user is getting pumped.
    /// Displayed after the condition is applied to the user.
    /// </summary>
    protected IEnumerator OnFocused(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_FOCUSED, bPokemon: user));
    }

    /// <summary>
    /// Message for when the user puts itself to sleep.
    /// Displayed immediately after the move is used.
    /// </summary>
    protected IEnumerator OnRested(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_RESTED, bPokemon: user, waitForInput: false));
    }

    #endregion

    #region Move Effect Messages

    /// <summary>
    /// Message for when the user saps health from the target.
    /// Displayed after the user would regain the health sapped, if applicable.
    /// </summary>
    protected IEnumerator OnHealthSapped(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_HEALTH_SAPPED, bPokemon: target));
    }

    /// <summary>
    /// Message for when this move regains the user's health.
    /// Displayed after the user regains the health.
    /// </summary>
    protected IEnumerator OnHealthRegained(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_HEALTH_REGAINED, bPokemon: user));
    }

    /// <summary>
    /// Message for when the target has its dream eaten.
    /// Displayed after the user would regain the health sapped, if applicable.
    /// </summary>
    protected IEnumerator OnDreamEaten(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_DREAM_EATER, bPokemon: target));
    }

    /// <summary>
    /// Message for when Reflect's effect takes place.
    /// Displayed after the move is used, if successful.
    /// </summary>
    protected IEnumerator OnReflect(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_REFLECT, bPokemon: user));
    }

    /// <summary>
    /// Message for when Light Screen's effect takes place.
    /// Displayed after the move is used, if successful.
    /// </summary>
    protected IEnumerator OnLightScreen(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_LIGHT_SCREEN, bPokemon: user));
    }

    /// <summary>
    /// Message for when Reflect's effect takes place.
    /// Displayed after the move is used, if successful.
    /// </summary>
    protected IEnumerator OnMist(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_MIST, bPokemon: user));
    }

    /// <summary>
    /// Message for when the battle is ended with Teleport.
    /// Displayed after the move is used, if successful.
    /// </summary>
    protected IEnumerator OnTeleport(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_REFLECT, bPokemon: user));
    }

    /// <summary>
    /// Message for when the user changes type to match the target.
    /// Displayed after the move is used, if successful.
    /// </summary>
    protected IEnumerator OnConversion(BattlePokemon target)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_CONVERSION, bPokemon: target));
    }

    /// <summary>
    /// Message for when all status changes are removed from both sides.
    /// Displayed after the move is used.
    /// </summary>
    protected IEnumerator OnHaze()
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_HAZE));
    }

    /// <summary>
    /// Message for when this move does nothing.
    /// Displayed after the move is used.
    /// </summary>
    protected IEnumerator OnNoEffect()
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_SPLASH));
    }

    /// <summary>
    /// Message for when the user is hurt by recoil damage.
    /// Displayed after the user takes the recoil damage.
    /// </summary>
    protected IEnumerator OnRecoil(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_RECOIL, bPokemon: user));
    }

    /// <summary>
    /// Message for when this move missed and the user crashes.
    /// Displayed after the move misses.
    /// </summary>
    protected IEnumerator OnCrashed(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(BattleMessages.EFFECT_CRASH, bPokemon: user));
    }

    /// <summary>
    /// Message for when the user changes type to match the target.
    /// Displayed after the move is used, if successful.
    /// </summary>
    protected IEnumerator OnBattleForceEnded(BattlePokemon user, string message)
    {
        yield return Battle.StartCoroutine(BattleMessages.Display(message, bPokemon: user));
    }

    #endregion
}