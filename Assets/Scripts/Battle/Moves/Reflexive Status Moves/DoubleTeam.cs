using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTeam : ReflexiveStatusMove
{
    public DoubleTeam(BattleStateManager battle)
        : base (
            name: "DOUBLE TEAM",
            type: Type.NORMAL,
            basePP: 15,
            battle: battle)
    {
        Effect = ReflexiveStatusEffect.RaiseEvasion;
    }
}