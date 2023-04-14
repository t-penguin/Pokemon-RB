using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleKick : MultiHitAttackMove
{
    public DoubleKick(BattleStateManager battle)
        : base (
            name: "DOUBLE KICK",
            type: Type.FIGHTING,
            category: Category.Physical,
            basePP: 30,
            accuracy: 100,
            power: 30,
            battle: battle )
    {
        RandomHits = false;
        SetNumberOfHits(2);
    }
}