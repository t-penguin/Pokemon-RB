using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growl : TransitiveStatusMove
{
    public Growl(BattleStateManager battle)
        : base (
            name: "GROWL",
            type: Type.NORMAL,
            basePP: 40,
            accuracy: 100,
            battle: battle )
    { }

    public sealed override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        bool failed = false;
        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent, out failed))
        {
            if (failed)
                yield return Battle.StartCoroutine(OnFailed());
            else
                yield return Battle.StartCoroutine(OnMissed(user));
        }
        else if (opponent.CanStatBeLowered(StatType.Attack))
        {
            // ANIMATION OFF
            // MOVE SCREEN TO THE RIGHT BY 4 AND BACK TWICE
            opponent.ModifyStatAsPrimary(StatType.Attack, -1);
            yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.Attack));
            if (opponent.IsBideActive)
                opponent.BideDamage += 2 * opponent.LastDamageRecieved;
        }
        else
        {
            yield return Battle.StartCoroutine(OnNothingHappened());
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}