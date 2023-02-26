using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : SimpleAttackMove
{
    public Scratch(BattleStateManager battle) : base("SCRATCH", Type.NORMAL, Category.Physical, 0, 35, 100, 40, false, battle) { }

    public sealed override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        yield return new WaitForSeconds(2 / 60f);

        if (!AccuracyCheck(user, opponent))
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
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
    }
}
