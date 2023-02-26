using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiTurnAttackMove : AttackMove
{
    protected int Damage { get; private set; }
    protected int TurnsLeft { get; private set; }

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
        : base(name, type, category, 0, basePP, accuracy, power, false, battle)
    {
        Damage = 0;
        TurnsLeft = 0;
    }

    // Sets the max amount of turns this move last
    private void SetMaxTurns()
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
}