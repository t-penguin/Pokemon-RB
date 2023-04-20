using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackle : SimpleAttackMove
{
    public Tackle(BattleStateManager battle)
        : base (
            name: "TACKLE",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 35,
            accuracy: 100,
            power: 40,
            battle: battle ) 
    { }
}