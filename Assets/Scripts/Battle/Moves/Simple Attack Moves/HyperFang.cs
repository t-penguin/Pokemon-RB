using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperFang : SimpleAttackMove
{
    public HyperFang(BattleStateManager battle)
        : base (
            name: "HYPER FANG",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 15,
            accuracy: 90,
            power: 80,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Flinch;
        secondaryEffectChance = 10;
    }
}