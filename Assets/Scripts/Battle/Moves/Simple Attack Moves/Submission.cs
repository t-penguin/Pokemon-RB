using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submission : SimpleAttackMove
{
    public Submission(BattleStateManager battle)
        : base (
            name: "SUBMISSION",
            type: Type.FIGHTING,
            category: Category.Physical,
            basePP: 25,
            accuracy: 80,
            power: 80,
            battle: battle )
    {
        hasRecoil = true;
    }
}