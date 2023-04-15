using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peck : SimpleAttackMove
{
    public Peck(BattleStateManager battle)
        : base (
            name: "PECK",
            type: Type.FLYING,
            category: Category.Physical,
            basePP: 35,
            accuracy: 100,
            power: 35,
            battle: battle )
    { }
}