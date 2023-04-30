using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : ReflexiveStatusMove
{
    public Recover(BattleStateManager battle)
        : base (
            name: "RECOVER",
            type: Type.NORMAL,
            basePP: 20,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RestoreHealth;
    }
}