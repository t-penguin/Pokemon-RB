using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MultiTurnAttackMove
{
    public Wrap(BattleStateManager battle)
        : base (
            name: "WRAP",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 85,
            power: 15,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Binding;
    }
}