using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MultiHitAttackMove
{
    public Barrage(BattleStateManager battle)
        : base (
            name: "BARRAGE",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 85,
            power: 15,
            battle: battle)
    { }
}