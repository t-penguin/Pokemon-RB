using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : SimpleAttackMove
{
    public Bubble(BattleStateManager battle)
        : base (
            name: "BUBBLE",
            type: Type.WATER,
            category: Category.Special,
            basePP: 30,
            accuracy: 100,
            power: 20,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.LowerSpeed;
        secondaryEffectChance = 33;
    }
}