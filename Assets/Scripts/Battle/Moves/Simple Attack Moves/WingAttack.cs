using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingAttack : SimpleAttackMove
{
    public WingAttack(BattleStateManager battle)
        : base (
            name: "WIGN ATTACK",
            type: Type.FLYING,
            category: Category.Physical,
            basePP: 35,
            accuracy: 100,
            power: 35,
            battle: battle )
    { }
}