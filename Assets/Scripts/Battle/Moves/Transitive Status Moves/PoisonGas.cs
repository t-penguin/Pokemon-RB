using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : TransitiveStatusMove
{
    public PoisonGas(BattleStateManager battle)
        : base (
            name: "POISON GAS",
            type: Type.POISON,
            basePP: 40,
            accuracy: 55,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.Poison;
    }
}