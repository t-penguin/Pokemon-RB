using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : SimpleAttackMove
{
    public Thunder(BattleStateManager battle)
        : base (
            name: "THUNDER",
            type: Type.ELECTRIC,
            category: Category.Special,
            basePP: 10,
            accuracy: 70,
            power: 120,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Paralysis;
        secondaryEffectChance = 10;
    }
}