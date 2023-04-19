using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sing : TransitiveStatusMove
{
    public Sing(BattleStateManager battle)
        : base (
            name: "SING",
            type: Type.NORMAL,
            basePP: 15,
            accuracy: 55,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.Sleep;
    }
}