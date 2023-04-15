using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAttack : SimpleAttackMove
{
    public QuickAttack(BattleStateManager battle)
        : base (
            name: "QUICK ATTACK",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 30,
            accuracy: 100,
            power: 40,
            battle: battle,
            priority: 1 )
    { }
}