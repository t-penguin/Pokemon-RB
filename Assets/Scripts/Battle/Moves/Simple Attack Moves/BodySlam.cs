using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySlam : SimpleAttackMove
{
    public BodySlam(BattleStateManager battle)
        : base (
            name: "BODY SLAM",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 15,
            accuracy: 100,
            power: 85,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Paralysis;
        secondaryEffectChance = 30;
    }
}