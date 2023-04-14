using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyPunch : SimpleAttackMove
{
    public DizzyPunch(BattleStateManager battle)
        : base (
            name: "DIZZY PUNCH",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 10,
            accuracy: 100,
            power: 70,
            battle: battle )
    { }
}