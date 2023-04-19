using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : TransitiveStatusMove
{
    public Spore(BattleStateManager battle)
        : base (
            name: "SPORE",
            type: Type.GRASS,
            basePP: 15,
            accuracy: 100,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.Sleep;
    }
}