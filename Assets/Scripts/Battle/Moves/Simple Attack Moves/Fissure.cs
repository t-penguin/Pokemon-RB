using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fissure : SimpleAttackMove
{
    public Fissure(BattleStateManager battle)
        : base (
            name: "FISSURE",
            type: Type.GROUND,
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