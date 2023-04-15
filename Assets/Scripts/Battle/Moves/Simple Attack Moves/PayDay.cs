using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayDay : SimpleAttackMove
{
    public PayDay(BattleStateManager battle)
        : base (
            name: "PAY DAY",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 100,
            power: 40,
            battle: battle )
    { }

    /* ADD HERE: This move should scatter coins on every use that the player
     * picks up after the battle, unless they flee, capture the opponent, or black out */
}