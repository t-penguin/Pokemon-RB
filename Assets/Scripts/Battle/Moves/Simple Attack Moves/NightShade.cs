using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightShade : SimpleAttackMove
{
    public NightShade(BattleStateManager battle)
        : base (
            name: "NIGHT SHADE",
            type: Type.GHOST,
            category: Category.Physical,
            basePP: 15,
            accuracy: 100,
            power: 0,
            battle: battle )
    {
        dealsFixedDamage = true;
        fixedDamage = -1;
    }
}