using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCurl : ReflexiveStatusMove
{
    public DefenseCurl(BattleStateManager battle)
        : base (
            name: "DEFENSE CURL",
            type: Type.NORMAL,
            basePP: 40,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseDefense;
    }
}