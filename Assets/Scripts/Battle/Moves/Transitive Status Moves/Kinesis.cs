using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinesis : TransitiveStatusMove
{
    public Kinesis(BattleStateManager battle)
        : base (
            name: "KINESIS",
            type: Type.PSYCHIC,
            basePP: 15,
            accuracy: 80,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.LowerAccuracy;
    }
}