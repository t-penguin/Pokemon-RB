using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriAttack : SimpleAttackMove
{
    public TriAttack(BattleStateManager battle)
        : base (
            name: "TRI ATTACK",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 10,
            accuracy: 100,
            power: 80,
            battle: battle )
    { }
}