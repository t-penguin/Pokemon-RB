using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaDrain : SimpleAttackMove
{
    public MegaDrain(BattleStateManager battle) : base("MEGA DRAIN", Type.GRASS, Category.Special, 0, 10, 100, 40, false, battle) { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
        }
        else
        {
            // ANIMATION OFF
            // ???
            yield return Battle.StartCoroutine(DealDamageAndRegainHealth(user, opponent));
            yield return Battle.StartCoroutine(OnHealthSapped(opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}