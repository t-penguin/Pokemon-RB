using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypnosis : TransitiveStatusMove
{
    public Hypnosis(BattleStateManager battle)
        : base (
            name: "HYPNOSIS",
            type: Type.PSYCHIC,
            basePP: 20,
            accuracy: 60,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.Sleep;
    }
}