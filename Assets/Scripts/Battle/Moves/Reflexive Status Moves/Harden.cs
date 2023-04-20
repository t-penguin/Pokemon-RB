using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harden : ReflexiveStatusMove
{
    public Harden(BattleStateManager battle)
        : base (
            name: "HARDEN",
            type: Type.NORMAL,
            basePP: 30,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseDefense;
    }
}