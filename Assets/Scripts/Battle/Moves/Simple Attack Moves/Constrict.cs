using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrict : SimpleAttackMove
{
    public Constrict(BattleStateManager battle)
        : base (
            name: "CONSTRICT",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 35,
            accuracy: 100,
            power: 10,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.LowerSpeed;
        secondaryEffectChance = 33;
    }
}