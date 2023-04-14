using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSlap : MultiHitAttackMove
{
    public DoubleSlap(BattleStateManager battle)
        : base (
            name: "DOUBLE SLAP",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 10,
            accuracy: 85,
            power: 15,
            battle: battle )
    {
        RandomHits = true;
    }
}