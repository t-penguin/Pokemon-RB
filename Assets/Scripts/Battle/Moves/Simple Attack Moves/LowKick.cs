using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowKick : SimpleAttackMove
{
    public LowKick(BattleStateManager battle)
        : base (
            name: "LOW KICK",
            type: Type.FIGHTING,
            category: Category.Physical,
            basePP: 20,
            accuracy: 90,
            power: 50,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Flinch;
        secondaryEffectChance = 30;
    }
}