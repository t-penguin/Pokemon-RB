using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryAttack : MultiHitAttackMove
{
    public FuryAttack(BattleStateManager battle) : base("FURY ATTACK", Type.NORMAL, Category.Physical, 20, 85, 15, battle) { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
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
            // ANIMATION OFF
            // ??
            SetNumberOfHits();
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}