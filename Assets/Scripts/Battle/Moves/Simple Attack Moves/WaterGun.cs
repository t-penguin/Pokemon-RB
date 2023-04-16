using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : SimpleAttackMove
{
    public WaterGun(BattleStateManager battle)
        : base (
            name: "WATER GUN",
            type: Type.WATER,
            category: Category.Special,
            basePP: 25,
            accuracy: 100,
            power: 40,
            battle: battle )
    { }
}