using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarBeam : MultiTurnAttackMove
{
    public SolarBeam(BattleStateManager battle)
        : base (
            name: "SOLARBEAM",
            type: Type.GRASS,
            category: Category.Special,
            basePP: 10,
            accuracy: 100,
            power: 120,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Charging;
        ChargingText = "took in sunlight!";
    }
}