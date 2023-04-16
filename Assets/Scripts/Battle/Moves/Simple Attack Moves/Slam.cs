using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : SimpleAttackMove
{
    public Slam(BattleStateManager battle)
        : base (
            name: "SLAM",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 75,
            power: 80,
            battle: battle )
    { }
}