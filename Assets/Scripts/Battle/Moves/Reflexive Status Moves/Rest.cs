using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : ReflexiveStatusMove
{
    public Rest(BattleStateManager battle)
        : base (
            name: "REST",
            type: Type.PSYCHIC,
            basePP: 10,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.Rest;
    }
}