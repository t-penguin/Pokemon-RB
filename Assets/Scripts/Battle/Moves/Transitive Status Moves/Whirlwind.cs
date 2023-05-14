using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : TransitiveStatusMove
{
    public Whirlwind(BattleStateManager battle)
        : base (
            name: "WHIRLWIND",
            type: Type.NORMAL,
            basePP: 20,
            accuracy: 85,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.BattleEnding;
        battleEndingMessage = BattleMessages.EFFECT_WHIRLWIND;
    }
}