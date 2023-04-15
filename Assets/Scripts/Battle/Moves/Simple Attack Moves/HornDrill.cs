using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornDrill : SimpleAttackMove
{
    public HornDrill(BattleStateManager battle)
        : base (
            name: "HORN DRILL",
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