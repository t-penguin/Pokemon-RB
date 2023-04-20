using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agility : ReflexiveStatusMove
{
    public Agility(BattleStateManager battle)
        : base (
            name: "AGILITY",
            type: Type.PSYCHIC,
            basePP: 30,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseSpeed;
        greatlyRaiseStat = true;
    }
}