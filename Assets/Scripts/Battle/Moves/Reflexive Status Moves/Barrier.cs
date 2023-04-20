using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ReflexiveStatusMove
{
    public Barrier(BattleStateManager battle)
        : base (
            name: "BARRIER",
            type: Type.PSYCHIC,
            basePP: 30,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseDefense;
        greatlyRaiseStat = true;
    }
}