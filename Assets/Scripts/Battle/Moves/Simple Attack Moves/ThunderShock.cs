using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderShock : SimpleAttackMove
{
    public ThunderShock(BattleStateManager battle)
        : base (
            name: "THUNDERSHOCK",
            type: Type.ELECTRIC,
            category: Category.Special,
            basePP: 30,
            accuracy: 100,
            power: 40,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Paralysis;
        secondaryEffectChance = 10;
    }
}