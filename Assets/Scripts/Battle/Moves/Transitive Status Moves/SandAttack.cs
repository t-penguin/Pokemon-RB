using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandAttack : TransitiveStatusMove
{
    public SandAttack(BattleStateManager battle)
        : base (
            name: "SAND-ATTACK",
            type: Type.NORMAL,
            basePP: 15,
            accuracy: 100,
            battle: battle)
    {
        Effect = TransitiveStatusEffect.LowerAccuracy;
    }
}