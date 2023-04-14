using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bind : MultiTurnAttackMove
{
    public Bind(BattleStateManager battle)
        : base (
            name: "BIND",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 20,
            accuracy: 75,
            power: 15,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        // First Turn
        if (TurnsLeft == 0)
        {
            SetMaxTurns();
            SetActionLock(user, true);
            yield return Battle.StartCoroutine(OnUsed(user));
        }

        yield return Battle.StartCoroutine(DealDamage(user, opponent));
        TurnsLeft--;
    }
}
