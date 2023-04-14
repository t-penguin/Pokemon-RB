using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlast : SimpleAttackMove
{
    public FireBlast(BattleStateManager battle)
        : base (
            name: "FIRE BLAST",
            type: Type.FIRE,
            category: Category.Special,
            basePP: 5,
            accuracy: 85,
            power: 120,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Burn;
        secondaryEffectChance = 30;
    }
}