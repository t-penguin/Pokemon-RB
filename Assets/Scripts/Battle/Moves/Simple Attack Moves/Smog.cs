using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smog : SimpleAttackMove
{
    public Smog(BattleStateManager battle)
        : base (
            name: "SMOG",
            type: Type.POISON,
            category: Category.Physical,
            basePP: 20,
            accuracy: 70,
            power: 20,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Poison;
        secondaryEffectChance = 40;
    }
}