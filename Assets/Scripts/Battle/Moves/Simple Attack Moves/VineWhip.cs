using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineWhip : SimpleAttackMove
{
    public VineWhip(BattleStateManager battle)
        : base (
            name: "VINE WHIP",
            type: Type.GRASS,
            category: Category.Special,
            basePP: 10,
            accuracy: 100,
            power: 35,
            battle: battle )
    { }
}