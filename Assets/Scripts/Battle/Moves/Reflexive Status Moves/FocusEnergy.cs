using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusEnergy : ReflexiveStatusMove
{
    public FocusEnergy(BattleStateManager battle)
        : base (
            name: "FOCUS ENERGY",
            type: Type.NORMAL,
            basePP: 30,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.Focus;
    }
}