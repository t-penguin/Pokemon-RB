using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bind : MultiTurnAttackMove
{
    public Bind(BattleStateManager battle)
        : base (
            name: "BIND",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 75,
            power: 15,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Binding;
    }
}
