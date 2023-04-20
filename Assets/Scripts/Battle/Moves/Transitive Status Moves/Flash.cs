using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : TransitiveStatusMove
{
    public Flash(BattleStateManager battle)
        : base (
            name: "FLASH",
            type: Type.NORMAL,
            basePP: 20,
            accuracy: 70,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.LowerAccuracy;
    }
}