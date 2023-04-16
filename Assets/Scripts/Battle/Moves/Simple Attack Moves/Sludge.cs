using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sludge : SimpleAttackMove
{
    public Sludge(BattleStateManager battle)
        : base (
            name: "SLUDGE",
            type: Type.POISON,
            category: Category.Physical,
            basePP: 20,
            accuracy: 100,
            power: 65,
            battle: battle )
    {
        secondaryEffect = SecondaryEffects.Poison;
        secondaryEffectChance = 40;
    }
}