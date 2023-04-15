using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pound : SimpleAttackMove
{
    public Pound(BattleStateManager battle)
        : base (
            name: "POUND",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 35,
            accuracy: 100,
            power: 40,
            battle: battle )
    { }
}