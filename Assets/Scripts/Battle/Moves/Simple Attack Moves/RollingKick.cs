using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingKick : SimpleAttackMove
{
    public RollingKick(BattleStateManager battle)
        : base (
            name: "ROLLING KICK",
            type: Type.FIGHTING,
            category: Category.Physical,
            basePP: 15,
            accuracy: 85,
            power: 60,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Flinch;
        secondaryEffectChance = 30;
    }
}