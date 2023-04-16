using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViceGrip : SimpleAttackMove
{
    public ViceGrip(BattleStateManager battle)
        : base (
            name: "VICEGRIP",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 30,
            accuracy: 100,
            power: 55,
            battle: battle )
    { }
}