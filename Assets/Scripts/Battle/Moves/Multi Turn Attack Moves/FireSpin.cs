using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpin : MultiTurnAttackMove
{
    public FireSpin(BattleStateManager battle)
        : base (
            name: "FIRE SPIN",
            type: Type.FIRE,
            category: Category.Special,
            basePP: 15,
            accuracy: 70,
            power: 15,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Binding;
    }
}