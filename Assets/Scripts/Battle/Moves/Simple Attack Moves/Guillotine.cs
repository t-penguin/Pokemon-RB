using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : SimpleAttackMove
{
    public Guillotine(BattleStateManager battle)
        : base (
            name: "GUILLOTINE",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 5,
            accuracy: 30,
            power: 0,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(ExecuteOneHitKO(user, opponent));
    }
}