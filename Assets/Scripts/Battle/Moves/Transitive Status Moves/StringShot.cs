using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringShot : TransitiveStatusMove
{
    public StringShot(BattleStateManager battle)
        : base (
            name: "STRING SHOT",
            type: Type.BUG,
            basePP: 40,
            accuracy: 95,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.LowerSpeed;
    }
}