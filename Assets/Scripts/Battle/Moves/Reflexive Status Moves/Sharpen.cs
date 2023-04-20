using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharpen : ReflexiveStatusMove
{
    public Sharpen(BattleStateManager battle)
        : base (
            name: "SHARPEN",
            type: Type.NORMAL,
            basePP: 30,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseAttack;
    }
}