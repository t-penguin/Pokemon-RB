using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amnesia : ReflexiveStatusMove
{
    public Amnesia(BattleStateManager battle)
        : base (
            name: "AMNESIA",
            type: Type.PSYCHIC,
            basePP: 30, 
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseSpecial;
        greatlyRaiseStat = true;
    }
}