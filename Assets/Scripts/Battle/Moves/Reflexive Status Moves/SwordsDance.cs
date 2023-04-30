using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsDance : ReflexiveStatusMove
{
    public SwordsDance(BattleStateManager battle)
        : base (
            name: "SWORDS DANCE",
            type: Type.NORMAL,
            basePP: 30,
            battle: battle )
    {
        Effect = ReflexiveStatusEffect.RaiseAttack;
        greatlyRaiseStat = true;
    }
}