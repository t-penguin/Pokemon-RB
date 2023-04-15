using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarateChop : SimpleAttackMove
{
    public KarateChop(BattleStateManager battle)
        : base (
            name: "KARATE CHOP",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 25,
            accuracy: 100,
            power: 50,
            battle: battle,
            highCrit: true )
    { }
}