using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPowder : TransitiveStatusMove
{
    public PoisonPowder(BattleStateManager battle)
        : base (
            name: "POISONPOWDER",
            type: Type.POISON,
            basePP: 35,
            accuracy: 75,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.Poison;
    }
}