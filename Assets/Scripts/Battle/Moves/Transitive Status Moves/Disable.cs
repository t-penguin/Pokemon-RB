using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : TransitiveStatusMove
{
    public Disable(BattleStateManager battle)
        : base (
            name: "DISABLE",
            type: Type.NORMAL,
            basePP: 20,
            accuracy: 55,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.Disable;
    }
}