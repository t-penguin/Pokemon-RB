using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunderbolt : SimpleAttackMove
{
    public Thunderbolt(BattleStateManager battle)
        : base (
            name: "THUNDERBOLT",
            type: Type.ELECTRIC,
            category: Category.Special,
            basePP: 15,
            accuracy: 100,
            power: 95,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Paralysis;
        secondaryEffectChance = 10;
    }
}