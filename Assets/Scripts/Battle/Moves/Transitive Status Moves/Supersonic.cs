using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supersonic : TransitiveStatusMove
{
    public Supersonic(BattleStateManager battle)
        : base (
            name: "SUPERSONIC",
            type: Type.NORMAL,
            basePP: 20,
            accuracy: 55,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.Confuse;
    }
    
    // This move fails if the target is behind a substitute
}