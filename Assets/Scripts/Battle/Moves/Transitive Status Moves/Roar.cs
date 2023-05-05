using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roar : TransitiveStatusMove
{
    private const string RAN_AWAY_SCARED = "ran away scared!";

    public Roar(BattleStateManager battle)
        : base (
            name: "ROAR",
            type: Type.NORMAL,
            basePP: 20,
            accuracy: 100,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.BattleEnding;
        battleEndingMessage = RAN_AWAY_SCARED;
    }
}