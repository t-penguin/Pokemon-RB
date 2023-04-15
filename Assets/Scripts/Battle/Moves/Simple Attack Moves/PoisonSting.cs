using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSting : SimpleAttackMove
{
    public PoisonSting(BattleStateManager battle)
        : base (
            name: "POISON STING",
            type: Type.POISON,
            category: Category.Physical,
            basePP: 35,
            accuracy: 100,
            power: 15,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Poison;
        secondaryEffectChance = 20;
    }
}