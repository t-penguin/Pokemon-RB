using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ember : SimpleAttackMove
{
    public Ember(BattleStateManager battle)
        : base (
            name: "EMBER",
            type: Type.FIRE,
            category: Category.Special,
            basePP: 25,
            accuracy: 100,
            power: 40,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Burn;
        secondaryEffectChance = 10;
    }
}