using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbutt : SimpleAttackMove
{
    public Headbutt(BattleStateManager battle)
        : base (
            name: "HEADBUTT",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 15,
            accuracy: 100,
            power: 70,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Flinch;
        secondaryEffectChance = 30;
    }
}