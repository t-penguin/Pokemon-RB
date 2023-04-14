using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabhammer : SimpleAttackMove
{
    public Crabhammer(BattleStateManager battle)
        : base (
            name: "CRABHAMMER",
            type: Type.WATER,
            category: Category.Physical,
            basePP: 10,
            accuracy: 85,
            power: 90,
            battle: battle,
            highCrit: true )
    { }
}