using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaPunch : SimpleAttackMove
{
    public MegaPunch(BattleStateManager battle)
        : base (
            name: "MEGA PUNCH",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 85,
            power: 80,
            battle: battle )
    { }
}