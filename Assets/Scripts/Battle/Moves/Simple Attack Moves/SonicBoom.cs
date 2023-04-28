using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicBoom : SimpleAttackMove
{
    public SonicBoom(BattleStateManager battle)
        : base (
            name: "SONICBOOM",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 90,
            power: 0,
            battle: battle )
    {
        dealsFixedDamage = true;
        fixedDamage = 20;
    }
}