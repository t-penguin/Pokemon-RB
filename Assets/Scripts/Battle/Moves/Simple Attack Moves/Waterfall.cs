using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : SimpleAttackMove
{
    public Waterfall(BattleStateManager battle)
        : base (
            name: "WATERFALL",
            type: Type.WATER,
            category: Category.Special,
            basePP: 15,
            accuracy: 100,
            power: 80,
            battle: battle )
    { }
}