using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screech : TransitiveStatusMove
{
    public Screech(BattleStateManager battle)
        : base (
            name: "SCREECH",
            type: Type.NORMAL,
            basePP: 40,
            accuracy: 85,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.LowerDefense;
        greatlyLowerStat = true;
    }
}