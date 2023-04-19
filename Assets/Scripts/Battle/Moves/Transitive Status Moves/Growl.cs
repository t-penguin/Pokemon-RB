using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growl : TransitiveStatusMove
{
    public Growl(BattleStateManager battle)
        : base (
            name: "GROWL",
            type: Type.NORMAL,
            basePP: 40,
            accuracy: 100,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.LowerAttack;
    }
}