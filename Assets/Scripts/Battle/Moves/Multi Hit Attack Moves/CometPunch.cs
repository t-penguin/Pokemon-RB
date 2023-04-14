using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometPunch : MultiHitAttackMove
{
    public CometPunch(BattleStateManager battle)
        : base (
            name: "COMET PUNCH",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 15,
            accuracy: 85,
            power: 18,
            battle: battle )
    {
        RandomHits = true;
    }
}