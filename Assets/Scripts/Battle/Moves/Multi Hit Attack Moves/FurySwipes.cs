using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurySwipes : MultiHitAttackMove
{
    public FurySwipes(BattleStateManager battle)
        : base (
            name: "FURY SWIPES",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 15,
            accuracy: 80,
            power: 18,
            battle: battle )
    {
        RandomHits = true;
    }
}