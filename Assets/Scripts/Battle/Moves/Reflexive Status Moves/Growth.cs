using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : ReflexiveStatusMove
{
    public Growth(BattleStateManager battle)
        : base (
            name: "GROWTH",
            type: Type.NORMAL,
            basePP: 40,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseSpecial;
    }
}