using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiTurnAttackMove : AttackMove
{
    protected MultiTurnAttackType AttackType = MultiTurnAttackType.None;
    protected string ChargingText = string.Empty;
    protected bool ShowUseAndChargeText = false;
    protected bool SemiInvulnerability = false;

    protected int TurnsLeft = 0;
    protected int Damage = 0;

    /// <summary>
    /// Creates an attack move that damages the opponent multiple times in one turn.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="category"></param>
    /// <param name="basePP"></param>
    /// <param name="accuracy"></param>
    /// <param name="power"></param>
    /// <param name="battle"></param>
    protected MultiTurnAttackMove(string name, Type type, Category category, int basePP, int accuracy, int power, BattleStateManager battle)
        : base(name, type, category, basePP, accuracy, power, battle) { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        switch(AttackType)
        {
            case MultiTurnAttackType.None: yield break;
            case MultiTurnAttackType.Binding:
                yield return Battle.StartCoroutine(ExecuteBindingMove(user, opponent));
                yield break;
            case MultiTurnAttackType.Charging:
                yield return Battle.StartCoroutine(ExecuteChargingMove(user, opponent));
                yield break;
            case MultiTurnAttackType.Recharging:
                yield return Battle.StartCoroutine(ExecuteRechargingMove(user, opponent));
                yield break;
            case MultiTurnAttackType.Thrashing:
                yield return Battle.StartCoroutine(ExecuteThrashingMove(user, opponent));
                yield break;
        }
    }

    private IEnumerator ExecuteBindingMove(BattlePokemon user, BattlePokemon opponent)
    {
        // First turn of a binding move
        if (TurnsLeft == 0)
        {
            yield return Battle.StartCoroutine(OnUsed(user));
            SetMaxTurns();
            if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
                yield return Battle.StartCoroutine(OnMissed(user));
            else if (MoveData.HasNoEffect(this, opponent))
                yield return Battle.StartCoroutine(OnDoesNotAffect(opponent));
            else
            {
                SetMoveLock(user, true);
                opponent.Bound = true;
                Damage = MoveData.CalculateDamage(this, user, opponent, out bool isCrit);
                yield return Battle.StartCoroutine(opponent.RecieveDamge(Damage, Type));
                if (isCrit)
                    yield return Battle.StartCoroutine(OnCriticalHit());
                yield return new WaitForSeconds(60 / 60f);
            }

            TurnsLeft--;
            SetLastMoveUsed(user);
            SetMirrorMove(opponent);
            CurrentPP--;
            yield break;
        }

        // Subsequent turns of a binding move
        yield return Battle.StartCoroutine(OnAttackContinues(user));
        yield return Battle.StartCoroutine(opponent.RecieveDamge(Damage, Type));
        yield return new WaitForSeconds(60 / 60f);
        if (TurnsLeft == 1)
        {
            SetMoveLock(user, false);
            opponent.Bound = false;
        }
        TurnsLeft--;
    }

    private IEnumerator ExecuteChargingMove(BattlePokemon user, BattlePokemon opponent)
    {
        // First turn of a charging attack
        if (TurnsLeft == 0)
        {
            if (ShowUseAndChargeText)
            {
                yield return Battle.StartCoroutine(OnUsed(user));
                yield return new WaitForSeconds(30 / 60f);
            }

            yield return Battle.StartCoroutine(OnCharging(user, ChargingText));
            user.IsSemiInvulnerable = SemiInvulnerability;
            SetActionLock(user, true);
            TurnsLeft = 1;
            yield break;
        }

        // Second turn of a charging attack
        yield return Battle.StartCoroutine(OnUsed(user));
        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
            yield return Battle.StartCoroutine(OnMissed(user));
        else if (MoveData.HasNoEffect(this, opponent))
            yield return Battle.StartCoroutine(OnDoesNotAffect(opponent));
        else
            yield return Battle.StartCoroutine(DealDamage(user, opponent));

        TurnsLeft = 0;
        SetActionLock(user, false);
        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }

    private IEnumerator ExecuteRechargingMove(BattlePokemon user, BattlePokemon opponent)
    {
        // First turn of a recharging move
        if (TurnsLeft == 0)
        {
            yield return Battle.StartCoroutine(OnUsed(user));
            if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
                yield return Battle.StartCoroutine(OnMissed(user));
            else if (MoveData.HasNoEffect(this, opponent))
                yield return Battle.StartCoroutine(OnDoesNotAffect(opponent));
            else
            {
                yield return Battle.StartCoroutine(DealDamage(user, opponent));
                TurnsLeft = 1;
                SetActionLock(user, true);
            }

            user.Recharging = true;
            SetLastMoveUsed(user);
            SetMirrorMove(opponent);
            CurrentPP--;
            yield break;
        }

        // Second turn of a recharging move
        yield return Battle.StartCoroutine(OnRecharging(user));
        TurnsLeft = 0;
        SetActionLock(user, false);
        user.Recharging = false;
    }

    private IEnumerator ExecuteThrashingMove(BattlePokemon user, BattlePokemon opponent)
    {
        // First turn of a thrashing move
        if (TurnsLeft == 0)
        {
            yield return Battle.StartCoroutine(OnUsed(user));
            SetActionLock(user, true);
            SetMaxTurns();
            SetLastMoveUsed(user);
            SetMirrorMove(opponent);
            CurrentPP--;
        }
        // Subsequent turns of a thrashing move
        else
            yield return Battle.StartCoroutine(OnThrashing(user));

        // Every turn of a thrashing move performs these checks
        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
            yield return Battle.StartCoroutine(OnMissed(user));
        else if (MoveData.HasNoEffect(this, opponent))
            yield return Battle.StartCoroutine(OnDoesNotAffect(opponent));
        else
            yield return Battle.StartCoroutine(DealDamage(user, opponent));

        // The final turn of a thrashing move leaves the user confused
        if (TurnsLeft == 1)
        {
            SetActionLock(user, false);
            user.Confuse();
            yield return Battle.StartCoroutine(OnFatigued(user));
        }

        TurnsLeft--;
    }

    // Sets the random max amount of turns this move will last
    protected void SetMaxTurns()
    {
        int random = Random.Range(0, 256);

        // Binding moves last 2-5 turns
        if (AttackType == MultiTurnAttackType.Binding)
        {
            /* 3/8 chance each for 2 and 3 turns
             * 1/8 chance each for 4 and 5 turns */
            if (random < 96) TurnsLeft = 2;
            else if (random < 192) TurnsLeft = 3;
            else if (random < 224) TurnsLeft = 4;
            else TurnsLeft = 5;
        }
        // Thrashing moves last 3-4 turns
        else
        {
            if (random < 128) TurnsLeft = 3;
            else TurnsLeft = 4;
        }
    }

    // Stops the multi turn attack
    public void Abort(BattlePokemon user)
    {
        TurnsLeft = 0;
        SetActionLock(user, false);
        SetMoveLock(user, false);
    }

    protected void SetActionLock(BattlePokemon user, bool locked)
    {
        if (user == Battle.PlayerSide.ActivePokemon)
            Battle.PlayerSide.LockedIntoAction = locked;
        else
            Battle.OpponentSide.LockedIntoAction = locked;
    }

    protected void SetMoveLock(BattlePokemon user, bool locked)
    {
        if (user == Battle.PlayerSide.ActivePokemon)
            Battle.PlayerSide.LockedIntoMove = locked;
        else
            Battle.OpponentSide.LockedIntoMove = locked;
    }
}

public enum MultiTurnAttackType
{
    None,
    Binding,
    Charging,
    Recharging,
    Thrashing
}