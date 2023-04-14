using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePunch : SimpleAttackMove
{
    public FirePunch(BattleStateManager battle)
        : base (
            name: "FIRE PUNCH",
            type: Type.FIRE,
            category: Category.Special,
            basePP: 15,
            accuracy: 100,
            power: 75,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Burn;
        secondaryEffectChance = 10;
    }
}