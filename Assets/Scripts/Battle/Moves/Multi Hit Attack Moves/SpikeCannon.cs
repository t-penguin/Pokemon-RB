using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCannon : MultiHitAttackMove
{
    public SpikeCannon(BattleStateManager battle)
        : base (
            name: "SPIKE CANNON",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 15,
            accuracy: 100,
            power: 20,
            battle: battle )
    {
        RandomHits = true;
    }
}