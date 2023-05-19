using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRage : SimpleAttackMove
{
    public DragonRage(BattleStateManager battle)
        : base (
            name: "DRAGON RAGE",
            type: Type.DRAGON,
            category: Category.Special,
            basePP: 10,
            accuracy: 100,
            power: 0,
            battle: battle )
    {
        dealsFixedDamage = true;
        fixedDamage = 40;
    }
}