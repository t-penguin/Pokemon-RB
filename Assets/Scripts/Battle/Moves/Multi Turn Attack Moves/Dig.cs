using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : MultiTurnAttackMove
{
    public Dig(BattleStateManager battle)
        : base (
            name: "DIG",
            type: Type.GROUND,
            category: Category.Physical,
            basePP: 10,
            accuracy: 100,
            power: 100,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Charging;
        ChargingText = "dug a hole!";
        ShowUseAndChargeText = true;
        SemiInvulnerability = true;
    }
}