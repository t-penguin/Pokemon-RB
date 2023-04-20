using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leer : TransitiveStatusMove
{
    public Leer(BattleStateManager battle)
        : base (
            name: "LEER",
            type: Type.NORMAL,
            basePP: 30,
            accuracy: 100,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.LowerDefense;
    }
}