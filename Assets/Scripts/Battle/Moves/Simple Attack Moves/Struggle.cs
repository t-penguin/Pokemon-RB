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
        string userName = user.TrainerIsPlayer ? user.Name : $"Enemy {user.Name}";
        string noMovesMessage = $"{userName} has no\nmoves left!";
        yield return Battle.StartCoroutine(Battle.DisplayMessage(noMovesMessage, false));
        yield return new WaitForSeconds(30 / 60f);

        yield return Battle.StartCoroutine(OnUsed(user));

        bool missed = opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent);
        if (missed)
            yield return Battle.StartCoroutine(OnMissed(user));
        else if (MoveData.HasNoEffect(this, opponent))
            yield return Battle.StartCoroutine(OnNoEffect());
        else
        {
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
            int recoilDamage = Mathf.Max(1, opponent.LastDamageRecieved / 2);
            yield return Battle.StartCoroutine(user.RecieveDamge(recoilDamage, Type));
            yield return Battle.StartCoroutine(OnRecoil(user));
        }

        SetMirrorMove(opponent);
        SetLastMoveUsed(user);
    }
}