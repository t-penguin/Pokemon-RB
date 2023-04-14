using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : SimpleAttackMove
{
    public Flamethrower(BattleStateManager battle)
        : base (
            name: "FLAMETHROWER",
            type: Type.FIRE,
            category: Category.Special,
            basePP: 15,
            accuracy: 100,
            power: 95,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Burn;
        secondaryEffectChance = 10;
    }
}