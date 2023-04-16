using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSlide : SimpleAttackMove
{
    public RockSlide(BattleStateManager battle)
        : base (
            name: "ROCK SLIDE",
            type: Type.ROCK,
            category: Category.Physical,
            basePP: 10,
            accuracy: 90,
            power: 75,
            battle: battle )
    { }
}