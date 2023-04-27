using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperBeam : MultiTurnAttackMove
{
    public HyperBeam(BattleStateManager battle)
        : base (
            name: "HYPER BEAM",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 5,
            accuracy: 90,
            power: 150,
            battle: battle )
    {
        AttackType = MultiTurnAttackType.Recharging;
    }
}