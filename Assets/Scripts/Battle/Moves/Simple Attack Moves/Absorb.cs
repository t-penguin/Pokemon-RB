using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : SimpleAttackMove
{
    public Absorb(BattleStateManager battle)
        : base (
            name: "ABSORB",
            type: Type.GRASS,
            category: Category.Special,
            basePP: 20,
            accuracy: 100,
            power: 20,
            battle: battle )
    {
        sapsHealth = true;
    }
}