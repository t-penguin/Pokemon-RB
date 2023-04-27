using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MultiTurnAttackMove
{
    public Fly(BattleStateManager battle)
        : base (
            name: "FLY",
            type: Type.FLYING,
            category: Category.Physical,
            basePP: 15,
            accuracy: 95,
            power: 70,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Charging;
        ChargingText = "flew up high!";
        SemiInvulnerability = true;
    }
}