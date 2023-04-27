using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrash : MultiTurnAttackMove
{
    public Thrash(BattleStateManager battle)
        : base (
            name: "THRASH",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 100,
            power: 90,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Thrashing;
    }
}