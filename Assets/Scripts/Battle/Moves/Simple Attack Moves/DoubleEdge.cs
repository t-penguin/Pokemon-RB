using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEdge : SimpleAttackMove
{
    public DoubleEdge(BattleStateManager battle)
        : base (
            name: "DOUBLE-EDGE",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 15,
            accuracy: 100,
            power: 100,
            battle: battle )
    {
        hasRecoil = true;
    }
}