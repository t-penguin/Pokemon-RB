using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWhip : TransitiveStatusMove
{
    public TailWhip(BattleStateManager battle)
        : base (
            name: "TAIL WHIP",
            type: Type.NORMAL,
            basePP: 30,
            accuracy: 100,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.LowerDefense;
    }
}
