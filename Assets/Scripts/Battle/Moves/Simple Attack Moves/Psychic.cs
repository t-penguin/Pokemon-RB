using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psychic : SimpleAttackMove
{
    public Psychic(BattleStateManager battle)
        : base (
            name: "PSYCHIC",
            type: Type.PSYCHIC,
            category: Category.Special,
            basePP: 10,
            accuracy: 100,
            power: 90,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.LowerSpecial;
        secondaryEffectChance = 33;
    }
}