using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : SimpleAttackMove
{
    public Bite(BattleStateManager battle) : base("BITE", Type.NORMAL, Category.Physical, 0, 25, 100, 60, false, battle) { }

    public sealed override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
        }
        else
        {
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
            bool opponentAlive = opponent.Status != StatusEffect.FNT;
            bool secondaryEffect = Random.Range(0, 10) == 0;
            if (opponentAlive && secondaryEffect)
            {
                opponent.Flinch();
            }
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}