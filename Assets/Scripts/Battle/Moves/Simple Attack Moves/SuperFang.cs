using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFang : SimpleAttackMove
{
    public SuperFang(BattleStateManager battle)
        : base (
            name: "SUPER FANG",
            type: Type.NORMAL,
            category: Category.Physical,
            basePP: 10,
            accuracy: 90,
            power: 0,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
            yield return Battle.StartCoroutine(OnMissed(user));
        else
        {
            int damage = Mathf.Max(1, opponent.CurrentHP / 2);
            if (opponent.IsBideActive)
            {
                opponent.LastDamageRecieved = damage;
                opponent.BideDamage += 2 * damage;
            }
            yield return Battle.StartCoroutine(opponent.RecieveDamge(damage, Type));
            yield return new WaitForSeconds(60 / 60f);
        }

        EndMove(user, opponent);
    }
}