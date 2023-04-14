using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confusion : SimpleAttackMove
{
    public Confusion(BattleStateManager battle)
        : base (
            name: "CONFUSION",
            type: Type.PSYCHIC,
            category: Category.Special,
            basePP: 25,
            accuracy: 100,
            power: 50,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Confusion;
        secondaryEffectChance = 10;
    }
}