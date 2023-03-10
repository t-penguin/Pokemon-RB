using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : SimpleAttackMove
{
    public Absorb(BattleStateManager battle) : base("ABSORB", Type.GRASS, Category.Special, 0, 20, 100, 20, false, battle) { }

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