using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyAttack : MultiTurnAttackMove
{
    public SkyAttack(BattleStateManager battle)
        : base (
            name: "SKY ATTACK",
            type: Type.FLYING,
            category: Category.Physical,
            basePP: 5,
            accuracy: 90,
            power: 140,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Charging;
        ChargingText = BattleMessages.CHARGING_SKY_ATTACK;
    }
}