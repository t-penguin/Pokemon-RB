using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimize : ReflexiveStatusMove
{
    public Minimize(BattleStateManager battle)
        : base (
            name: "MINIMIZE",
            type: Type.NORMAL,
            basePP: 20,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseEvasion;
    }
}