using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePunch : SimpleAttackMove
{
    public IcePunch(BattleStateManager battle)
        : base (
            name: "ICE PUNCH",
            type: Type.ICE,
            category: Category.Special,
            basePP: 15,
            accuracy: 100,
            power: 75,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Freeze;
        secondaryEffectChance = 10;
    }
}