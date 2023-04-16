using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : SimpleAttackMove
{
    public Strength(BattleStateManager battle)
        : base (
            name: "STRENGTH",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 15,
            accuracy: 100,
            power: 80,
            battle: battle )
    { }
}