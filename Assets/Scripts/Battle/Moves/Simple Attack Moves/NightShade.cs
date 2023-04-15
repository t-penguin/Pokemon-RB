using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightShade : SimpleAttackMove
{
    public NightShade(BattleStateManager battle)
        : base (
            name: "NIGHT SHADE",
            type: Type.GHOST,
            category: Category.Physical,
            basePP: 15,
            accuracy: 100,
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
            int damage = user.Level;
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