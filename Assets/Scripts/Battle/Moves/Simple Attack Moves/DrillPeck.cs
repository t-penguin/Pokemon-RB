using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPeck : SimpleAttackMove
{
    public DrillPeck(BattleStateManager battle)
        : base (
            name: "DRILL PECK",
            type: Type.FLYING,
            category: Category.Physical,
            basePP: 20,
            accuracy: 100,
            power: 80,
            battle: battle )
    { }
}