using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lick : SimpleAttackMove
{
    public Lick(BattleStateManager battle)
        : base (
            name: "LICK",
            type: Type.GHOST,
            category: Category.Physical,
            basePP: 30,
            accuracy: 100,
            power: 20,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Paralysis;
        secondaryEffectChance = 30;
    }
}