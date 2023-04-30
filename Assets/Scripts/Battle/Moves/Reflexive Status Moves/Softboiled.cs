using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Softboiled : ReflexiveStatusMove
{
    public Softboiled(BattleStateManager battle)
        : base (
            name: "SOFTBOILED",
            type: Type.NORMAL,
            basePP: 10,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RestoreHealth;
    }
}