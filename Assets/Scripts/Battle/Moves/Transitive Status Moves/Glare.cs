using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glare : TransitiveStatusMove
{
    public Glare(BattleStateManager battle)
        : base (
            name: "GLARE",
            type: Type.NORMAL,
            basePP: 30,
            accuracy: 75,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.Paralyze;
    }
}