using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDown : SimpleAttackMove
{
    public TakeDown(BattleStateManager battle)
        : base (
            name: "TAKE DOWN",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 85,
            power: 90,
            battle: battle )
    {
        hasRecoil = true;
    }
}