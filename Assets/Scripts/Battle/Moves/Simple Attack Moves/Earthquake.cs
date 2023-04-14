using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : SimpleAttackMove
{
    public Earthquake(BattleStateManager battle)
        : base (
            name: "EARTHQUAKE",
            type: Type.GROUND,
            category: Category.Physical,
            basePP: 10,
            accuracy: 100,
            power: 100,
            battle: battle )
    { }
}