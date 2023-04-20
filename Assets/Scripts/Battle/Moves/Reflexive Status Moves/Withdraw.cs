using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Withdraw : ReflexiveStatusMove
{
    public Withdraw(BattleStateManager battle)
        : base (
            name: "WITHDRAW",
            type: Type.WATER,
            basePP: 40,
            battle: battle)
    {
        Effect = ReflexiveStatusEffect.RaiseDefense;
    }
}