using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBomb : SimpleAttackMove
{
    public EggBomb(BattleStateManager battle)
        : base (
            name: "EGG BOMB",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 10,
            accuracy: 75,
            power: 100,
            battle: battle )
    { }
}