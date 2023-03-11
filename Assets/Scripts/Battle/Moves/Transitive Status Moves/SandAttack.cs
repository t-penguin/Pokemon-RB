using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandAttack : TransitiveStatusMove
{
    public SandAttack(BattleStateManager battle) : base("SAND-ATTACK", Type.NORMAL, 15, 100, battle) { }

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
        else if (opponent.CanStatBeLowered(StatType.Accuracy))
        {
            // ANIMATION OFF
            // MOVE SCREEN TO THE RIGHT BY 4 AND BACK TWICE
            opponent.ModifyStatAsPrimary(StatType.Accuracy, -1);
            yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.Accuracy, false));
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