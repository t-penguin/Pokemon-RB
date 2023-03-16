using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : SimpleAttackMove
{
    public Blizzard(BattleStateManager battle) : base("BLIZZARD", Type.ICE, Category.Special, 0, 5, 90, 120, false, battle) { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
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
                opponent.Freeze();
            }
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}