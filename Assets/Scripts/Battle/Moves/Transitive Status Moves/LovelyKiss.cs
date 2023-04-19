using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovelyKiss : TransitiveStatusMove
{
    public LovelyKiss(BattleStateManager battle)
        : base (
            name: "LOVELY KISS",
            type: Type.NORMAL,
            basePP: 10,
            accuracy: 75,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.Sleep;
    }
}