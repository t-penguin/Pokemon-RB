using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaDrain : SimpleAttackMove
{
    public MegaDrain(BattleStateManager battle)
        : base (
            name: "MEGA DRAIN",
            type: Type.GRASS,
            category: Category.Special,
            basePP: 10,
            accuracy: 100,
            power: 40,
            battle: battle ) 
    {
        sapsHealth = true;
    }
}