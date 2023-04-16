using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : SimpleAttackMove
{
    public Slash(BattleStateManager battle)
        : base (
            name: "SLASH",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 100,
            power: 70,
            battle: battle )
    { }
}