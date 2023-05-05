using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : ReflexiveStatusMove
{
    public Teleport(BattleStateManager battle)
        : base (
            name: "TELEPORT",
            type: Type.PSYCHIC,
            basePP: 20,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.Teleport;
    }
}