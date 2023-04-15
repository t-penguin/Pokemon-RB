using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psybeam : SimpleAttackMove
{
    public Psybeam(BattleStateManager battle)
        : base (
            name: "PSYBEAM",
            type: Type.PSYCHIC,
            category: Category.Special,
            basePP: 20,
            accuracy: 100,
            power: 65,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Confusion;
        secondaryEffectChance = 10;
    }
}