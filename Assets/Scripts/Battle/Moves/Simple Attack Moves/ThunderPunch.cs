using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPunch : SimpleAttackMove
{
    public ThunderPunch(BattleStateManager battle)
        : base (
            name: "THUNDERPUNCH",
            type: Type.ELECTRIC,
            category: Category.Special,
            basePP: 15,
            accuracy: 100,
            power: 75,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Paralysis;
        secondaryEffectChance = 10;
    }
}