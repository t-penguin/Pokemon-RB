using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psywave : SimpleAttackMove
{
    public Psywave(BattleStateManager battle)
        : base (
            name: "PSYWAVE",
            type: Type.PSYCHIC,
            category: Category.Special,
            basePP: 15,
            accuracy: 80,
            power: 0,
            battle: battle )
    { }

    /* Psywave deals a random amount of damage 
     * between 1 HP and 1.5x the user's level. */
    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        opponent.LastDamageRecieved = 0;

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
            yield return Battle.StartCoroutine(OnMissed(user));
        else
        {
            int damage = Random.Range(1, (int)(1.5f * user.Level + 1));
            opponent.LastDamageRecieved = damage;
            
            yield return Battle.StartCoroutine(opponent.RecieveDamge(damage));
        }

        EndMove(user, opponent);
    }
}