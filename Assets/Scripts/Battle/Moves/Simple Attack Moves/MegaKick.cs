using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaKick : SimpleAttackMove
{
    public MegaKick(BattleStateManager battle)
        : base (
            name: "MEGA KICK",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 5,
            accuracy: 75,
            power: 120,
            battle: battle )
    { }
}