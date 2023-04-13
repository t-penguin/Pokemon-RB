using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeechLife : SimpleAttackMove
{
    public LeechLife(BattleStateManager battle)
        : base (
            name: "LEECH LIFE",
            type: Type.BUG,
            category: Category.Physical,
            basePP: 15,
            accuracy: 100,
            power: 20,
            battle: battle ) 
    {
        sapsHealth = true;
    }
}