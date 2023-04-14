using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonemerang : MultiHitAttackMove
{
    public Bonemerang(BattleStateManager battle)
        : base (
            name: "BONEMERANG",
            type: Type.GROUND,
            category: Category.Physical,
            basePP: 10,
            accuracy: 90,
            power: 50,
            battle: battle )
    {
        SetNumberOfHits(2);
        RandomHits = false;
    }
}