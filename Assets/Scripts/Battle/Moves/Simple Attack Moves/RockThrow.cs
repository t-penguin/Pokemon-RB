using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : SimpleAttackMove
{
    public RockThrow(BattleStateManager battle)
        : base (
            name: "ROCK THROW",
            type: Type.ROCK,
            category: Category.Physical,
            basePP: 15,
            accuracy: 65,
            power: 50,
            battle: battle )
    { }
}