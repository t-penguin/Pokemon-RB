using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : SimpleAttackMove
{
    public Scratch(BattleStateManager battle)
        : base (
            name: "SCRATCH",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 35,
            accuracy: 100,
            power: 40,
            battle: battle ) 
    { }
}