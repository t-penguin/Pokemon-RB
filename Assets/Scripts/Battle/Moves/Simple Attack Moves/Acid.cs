using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : SimpleAttackMove
{
    public Acid(BattleStateManager battle)
        : base (
            name: "ACID",
            type: Type.POISON,
            category: Category.Special,
            basePP: 30,
            accuracy: 100,
            power: 40,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.LowerDefense;
        secondaryEffectChance = 33;
    }
}