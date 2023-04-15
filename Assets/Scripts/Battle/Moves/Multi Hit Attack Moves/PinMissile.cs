using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinMissile : MultiHitAttackMove
{
    public PinMissile(BattleStateManager battle)
        : base (
            name: "PIN MISSILE",
            type: Type.BUG,
            category: Category.Physical,
            basePP: 20,
            accuracy: 85,
            power: 14,
            battle: battle )
    {
        RandomHits = true;
    }
}