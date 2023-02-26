using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growl : TransitiveStatusMove
{
    public Growl(BattleStateManager battle) : base("GROWL", Type.NORMAL, 40, 100, battle) { }

    public sealed override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        yield return new WaitForSeconds(2 / 60f);
        if(!AccuracyCheck(user, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
        }
        else if(opponent.CanStatBeLowered(StatType.Attack))
        {
            // ANIMATION OFF
            // MOVE SCREEN TO THE RIGHT BY 4 AND BACK TWICE
        }
        else
        {
            yield return Battle.StartCoroutine(OnFailed());
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
    }
}