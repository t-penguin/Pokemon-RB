using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditate : ReflexiveStatusMove
{
    public Meditate(BattleStateManager battle)
        : base (
            name: "MEDITATE",
            type: Type.PSYCHIC,
            basePP: 40,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseAttack;
    }
}