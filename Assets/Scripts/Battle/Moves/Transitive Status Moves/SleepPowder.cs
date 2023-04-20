using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepPowder : TransitiveStatusMove
{
    public SleepPowder(BattleStateManager battle)
        : base (
            name: "SLEEP POWDER",
            type: Type.GRASS,
            basePP: 15,
            accuracy: 75,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.Sleep;
    }
}