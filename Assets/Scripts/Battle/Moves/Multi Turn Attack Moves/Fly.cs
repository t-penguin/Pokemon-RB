using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MultiTurnAttackMove
{
    public Fly(BattleStateManager battle) : base("FLY", Type.FLYING, Category.Physical, 15, 95, 70, battle) { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        if(TurnsLeft == 0)
        {
            yield return Battle.StartCoroutine(FirstTurn(user));
            yield break;
        }

        yield return Battle.StartCoroutine(SecondTurn(user, opponent));
    }

    private IEnumerator FirstTurn(BattlePokemon user)
    {
        TurnsLeft = 1;
        user.IsSemiInvulnerable = true;
        SetActionLock(user, true);

        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{user.Name}\nflew up high!", true));
        yield return new WaitForSeconds(6 / 60f);
    }

    private IEnumerator SecondTurn(BattlePokemon user, BattlePokemon opponent)
    {
        TurnsLeft = 0;
        user.IsSemiInvulnerable = false;
        SetActionLock(user, false);

        yield return Battle.StartCoroutine(OnUsed(user));
        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
        }
        else
        {
            // ANIMATION OFF
            // ???
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}