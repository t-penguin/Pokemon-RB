using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiHitAttackMove : AttackMove
{
    protected int NumberOfHits { get; private set; }
    protected int Damage { get; private set; }

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
    protected MultiHitAttackMove(string name, Type type, Category category, int basePP, int accuracy, int power, BattleStateManager battle)
        : base(name, type, category, 0, basePP, accuracy, power, false, battle) => NumberOfHits = 0;

    // Randomly sets the number of hits
    protected void SetNumberOfHits()
    {
        int random = Random.Range(0, 256);

        /* 3/8 chance each for 2 and 3 hits
         * 1/8 chance each for 4 and 5 hits */
        if (random < 96)        NumberOfHits = 2;
        else if (random < 192)  NumberOfHits = 3;
        else if (random < 224)  NumberOfHits = 4;
        else                    NumberOfHits = 5;
    }

    // Sets the amount of damage this move will do with each hit
    protected void SetDamage(int damage) => Damage = damage;
}