using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MultiTurnAttackMove
{
    public Clamp(BattleStateManager battle)
        : base (
            name: "CLAMP",
            type: Type.WATER,
            category: Category.Special,
            basePP: 10,
            accuracy: 75,
            power: 35,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Binding;
    }
}