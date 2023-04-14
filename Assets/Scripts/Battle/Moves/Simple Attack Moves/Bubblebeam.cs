using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubblebeam : SimpleAttackMove
{
    public Bubblebeam(BattleStateManager battle)
        : base (
            name: "BUBBLEBEAM",
            type: Type.WATER,
            category: Category.Special,
            basePP: 20,
            accuracy: 100,
            power: 65,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.LowerSpeed;
        secondaryEffectChance = 33;
    }
}