using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : SimpleAttackMove
{
    public Cut(BattleStateManager battle)
        : base(
            name: "CUT",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 30,
            accuracy: 95,
            power: 50,
            battle: battle )
    { }
}