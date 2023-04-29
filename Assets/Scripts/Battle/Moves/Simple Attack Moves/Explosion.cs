using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : SimpleAttackMove
{
    public Explosion(BattleStateManager battle)
        : base (
            name: "EXPLOSION",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 5,
            accuracy: 100,
            power: 170,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(ExecuteExplosiveMove(user, opponent));
    }
}