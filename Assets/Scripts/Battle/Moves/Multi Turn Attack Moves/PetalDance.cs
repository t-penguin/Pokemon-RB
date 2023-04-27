using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalDance : MultiTurnAttackMove
{
    public PetalDance(BattleStateManager battle)
        : base (
            name: "PETAL DANCE",
            type: Type.GRASS,
            category: Category.Special,
            basePP: 20,
            accuracy: 100,
            power: 70,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Thrashing;
    }
}