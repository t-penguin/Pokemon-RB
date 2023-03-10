using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : SimpleAttackMove
{
    public Gust(BattleStateManager battle) : base("GUST", Type.NORMAL, Category.Physical, 0, 35, 100, 40, false, battle) { }

    public sealed override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
        }
        else if (MoveData.HasNoEffect(this, opponent))
        {
            yield return Battle.StartCoroutine(OnNoEffect());
        }
        else
        {
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}