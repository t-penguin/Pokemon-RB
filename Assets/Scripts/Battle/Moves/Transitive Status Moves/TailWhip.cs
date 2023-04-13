using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWhip : TransitiveStatusMove
{
    public TailWhip(BattleStateManager battle) : base("TAIL WHIP", Type.NORMAL, 30, 100, battle) { }

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
        else if (opponent.CanStatBeLowered(StatType.Defense))
        {
            // ANIMATION OFF
            // MOVE SCREEN TO THE RIGHT BY 4 AND BACK TWICE
            opponent.ModifyStatAsPrimary(StatType.Defense, -1);
            yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.Defense));
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
