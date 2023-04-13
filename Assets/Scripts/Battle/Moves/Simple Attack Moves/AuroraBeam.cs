using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraBeam : SimpleAttackMove
{
    public AuroraBeam(BattleStateManager battle)
        : base (
            name: "AURORA BEAM",
            type: Type.ICE,
            category: Category.Special,
            basePP: 20,
            accuracy: 100,
            power: 65,
            battle: battle ) 
    {
        secondaryEffect = SecondaryEffects.LowerAttack;
        secondaryEffectChance = 33;
    }
}