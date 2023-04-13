using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : SimpleAttackMove
{
    public Bite(BattleStateManager battle)
        : base(
            name: "BITE",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 25,
            accuracy: 100,
            power: 60,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Flinch;
        secondaryEffectChance = 10;
    }
}