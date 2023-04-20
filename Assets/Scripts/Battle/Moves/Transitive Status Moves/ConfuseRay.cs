using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfuseRay : TransitiveStatusMove
{
    public ConfuseRay(BattleStateManager battle)
        : base (
            name: "CONFUSE RAY",
            type: Type.GHOST,
            basePP: 10, 
            accuracy: 100,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.Confuse;
    }
}