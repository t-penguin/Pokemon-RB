using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScreen : TransitiveStatusMove
{
    public SmokeScreen(BattleStateManager battle)
        : base (
            name: "SMOKESCREEN",
            type: Type.NORMAL,
            basePP: 20,
            accuracy: 100,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.LowerAccuracy;
    }
}