using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackle : SimpleAttackMove
{
    public Tackle(BattleStateManager battle) : base("TACKLE", Type.NORMAL, Category.Physical, 0, 35, 100, 40, false, battle) { }

    public sealed override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if(opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
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
            // FLASH ENEMY IMAGE 6 TIMES
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}