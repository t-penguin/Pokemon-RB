using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : SimpleAttackMove
{
    public Blizzard(BattleStateManager battle)
        : base (
            name: "BLIZZARD",
            type: Type.ICE,
            category: Category.Special,
            basePP: 5,
            accuracy: 90,
            power: 120,
            battle: battle ) 
    {
        secondaryEffect = SecondaryEffects.Freeze;
        secondaryEffectChance = 10;
    }
}