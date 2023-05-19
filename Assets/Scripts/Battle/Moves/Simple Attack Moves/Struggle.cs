using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Struggle : SimpleAttackMove
{
    public Struggle(BattleStateManager battle)
        : base (
            name: "STRUGGLE",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 10,
            accuracy: 100,
            power: 50,
            battle: battle )
    {
        hasRecoil = true;
    }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        bool missed = opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent);
        if (missed)
            yield return Battle.StartCoroutine(OnMissed(user));
        else if (MoveData.HasNoEffect(this, opponent))
            yield return Battle.StartCoroutine(OnDoesNotAffect(opponent));
        else
        {
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
            int recoilDamage = Mathf.Max(1, opponent.LastDamageRecieved / 2);
            yield return Battle.StartCoroutine(user.RecieveDamge(recoilDamage));
            yield return Battle.StartCoroutine(OnRecoil(user));
        }

        SetMirrorMove(opponent);
        SetLastMoveUsed(user);
    }
}