using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorLeaf : SimpleAttackMove
{
    public RazorLeaf(BattleStateManager battle)
        : base (
            name: "RAZOR LEAF",
            type: Type.GRASS,
            category: Category.Special,
            basePP: 25,
            accuracy: 95,
            power: 55,
            battle: battle,
            highCrit: true)
    { }
}