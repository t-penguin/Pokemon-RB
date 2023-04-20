using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderWave : TransitiveStatusMove
{
    public ThunderWave(BattleStateManager battle)
        : base (
            name: "THUNDER WAVE",
            type: Type.ELECTRIC,
            basePP: 20,
            accuracy: 100,
            battle: battle )
    {
        Effect = TransitiveStatusEffect.Paralyze;
    }

    // This move takes into account type immunities, so it cannot affect Ground-types
}