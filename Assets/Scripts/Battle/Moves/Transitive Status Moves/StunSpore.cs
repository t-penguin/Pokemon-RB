using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunSpore : TransitiveStatusMove
{
    public StunSpore(BattleStateManager battle)
        : base (
            name: "STUN SPORE",
            type: Type.GRASS,
            basePP: 30,
            accuracy: 75,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.Paralyze;
    }
}