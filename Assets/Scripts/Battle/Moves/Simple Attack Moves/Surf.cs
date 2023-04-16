using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surf : SimpleAttackMove
{
    public Surf(BattleStateManager battle)
        : base (
            name: "SURF",
            type: Type.WATER,
            category: Category.Special,
            basePP: 15,
            accuracy: 100,
            power: 95,
            battle: battle )
    { }
}