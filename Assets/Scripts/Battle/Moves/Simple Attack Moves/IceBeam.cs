using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBeam : SimpleAttackMove
{
    public IceBeam(BattleStateManager battle)
        : base (
            name: "ICE BEAM",
            type: Type.ICE,
            category: Category.Special,
            basePP: 10,
            accuracy: 100,
            power: 95,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Freeze;
        secondaryEffectChance = 10;
    }
}