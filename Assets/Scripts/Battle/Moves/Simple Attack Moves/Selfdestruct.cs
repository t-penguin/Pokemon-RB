using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruct : SimpleAttackMove
{
    public Selfdestruct(BattleStateManager battle)
        : base (
            name: "SELFDESTRUCT",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 5,
            accuracy: 100,
            power: 130,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(ExecuteExplosiveMove(user, opponent));
    }
}