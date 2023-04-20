using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryAttack : MultiHitAttackMove
{
    public FuryAttack(BattleStateManager battle)
        : base (
            name: "FURY ATTACK",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 85,
            power: 15,
            battle: battle ) 
    {
        RandomHits = true;
    }
}