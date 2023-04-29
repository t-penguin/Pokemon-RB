using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScreen : ReflexiveStatusMove
{
    public LightScreen(BattleStateManager battle)
        : base (
            name: "LIGHT SCREEN",
            type: Type.PSYCHIC,
            basePP: 30,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.LightScreen;
    }
}