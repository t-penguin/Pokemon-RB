using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidArmor : ReflexiveStatusMove
{
    public AcidArmor(BattleStateManager battle)
        : base (
            name: "ACID ARMOR",
            type: Type.POISON, 
            basePP: 40,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseDefense;
        greatlyRaiseStat = true;
    }
}