using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiTurnAttackMove : AttackMove
{
    public int Damage { get; protected set; }
    public int TurnsLeft { get; protected set; }

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
        : base(name, type, category, basePP, accuracy, power, battle)
    {
        Damage = 0;
        TurnsLeft = 0;
    }

    // Sets the max amount of turns this move last
    protected void SetMaxTurns()
    {
        int random = Random.Range(0, 256);

        /* 3/8 chance each for 2 and 3 turns
         * 1/8 chance each for 4 and 5 turns */
        if (random < 96) TurnsLeft = 2;
        else if (random < 192) TurnsLeft = 3;
        else if (random < 224) TurnsLeft = 4;
        else TurnsLeft = 5;
    }

    // Stops the multi turn attack
    public void Abort()
    {
        TurnsLeft = 0;
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