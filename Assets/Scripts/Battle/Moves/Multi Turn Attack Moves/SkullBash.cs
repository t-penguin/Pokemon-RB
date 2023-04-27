using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBash : MultiTurnAttackMove
{
    public SkullBash(BattleStateManager battle)
        : base (
            name: "SKULL BASH",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 15,
            accuracy: 100,
            power: 100,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Charging;
        ChargingText = "lowered its head!";
    }
}