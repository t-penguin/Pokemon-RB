using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mist : ReflexiveStatusMove
{
    public Mist(BattleStateManager battle)
        : base (
            name: "MIST",
            type: Type.ICE,
            basePP: 30,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.Mist;
    }
}