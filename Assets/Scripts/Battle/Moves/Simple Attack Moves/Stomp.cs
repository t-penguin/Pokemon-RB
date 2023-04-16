using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : SimpleAttackMove
{
    public Stomp(BattleStateManager battle)
        : base (
            name: "STOMP",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 100,
            power: 65,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Flinch;
        secondaryEffectChance = 30;
    }
}