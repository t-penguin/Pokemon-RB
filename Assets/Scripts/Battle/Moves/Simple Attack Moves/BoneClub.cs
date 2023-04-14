using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneClub : SimpleAttackMove
{
    public BoneClub(BattleStateManager battle)
        : base (
            name: "BONE CLUB",
            type: Type.GROUND,
            category: Category.Physical,
            basePP: 20,
            accuracy: 85,
            power: 65,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Flinch;
        secondaryEffectChance = 10;
    }
}