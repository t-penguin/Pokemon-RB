using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydroPump : SimpleAttackMove
{
    public HydroPump(BattleStateManager battle)
        : base (
            name: "HYDRO PUMP",
            type: Type.WATER,
            category: Category.Special,
            basePP: 5,
            accuracy: 80,
            power: 120,
            battle: battle )
    { }
}