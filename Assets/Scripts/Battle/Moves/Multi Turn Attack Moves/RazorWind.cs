using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorWind : MultiTurnAttackMove
{
    public RazorWind(BattleStateManager battle)
        : base (
            name: "RAZOR WIND",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 10,
            accuracy: 75,
            power: 80,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Charging;
        ChargingText = "made a whirlwind!";
    }
}