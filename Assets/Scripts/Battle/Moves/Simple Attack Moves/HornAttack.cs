using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornAttack : SimpleAttackMove
{
    public HornAttack(BattleStateManager battle)
        : base (
            name: "HORN ATTACK",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 25,
            accuracy: 100,
            power: 65,
            battle: battle )
    { }
}