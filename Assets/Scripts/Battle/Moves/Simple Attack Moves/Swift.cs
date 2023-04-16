using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swift : SimpleAttackMove
{
    public Swift(BattleStateManager battle)
        : base (
            name: "SWIFT",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 0,
            power: 60,
            battle: battle )
    {
        requiresInvulnerabilityCheck = false;
        requiresAccuracyCheck = false;
    }
}