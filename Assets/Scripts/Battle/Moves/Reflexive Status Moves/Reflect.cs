using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : ReflexiveStatusMove
{
    public Reflect(BattleStateManager battle)
        : base (
            name: "REFLECT",
            type: Type.PSYCHIC,
            basePP: 20,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.Reflect;
    }
}