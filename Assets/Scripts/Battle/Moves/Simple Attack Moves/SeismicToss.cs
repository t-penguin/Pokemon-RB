using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeismicToss : SimpleAttackMove
{
    public SeismicToss(BattleStateManager battle)
        : base (
            name: "SEISMIC TOSS",
            type: Type.FIGHTING,
            category: Category.Physical,
            basePP: 20,
            accuracy: 100,
            power: 0,
            battle: battle )
    {
        dealsFixedDamage = true;
        fixedDamage = -1;
    }
}