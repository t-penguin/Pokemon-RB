using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeechSeed : TransitiveStatusMove
{
    public LeechSeed(BattleStateManager battle)
        : base (
            name: "LEECH SEED",
            type: Type.GRASS,
            basePP: 10,
            accuracy: 90,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.Seed;
    }
}