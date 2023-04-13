using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : SimpleAttackMove
{
    public Gust(BattleStateManager battle)
        : base (
            name: "GUST",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 35,
            accuracy: 100,
            power: 40, 
            battle: battle ) 
    { }
}