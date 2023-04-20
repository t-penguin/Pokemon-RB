using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKick : SimpleAttackMove
{
    public JumpKick(BattleStateManager battle)
        : base(
            name: "JUMP KICK",
            type: Type.FIGHTING,
            category: Category.Physical,
            basePP: 25,
            accuracy: 95,
            power: 70,
            battle: battle )
    { }

    /* If the user misses this move, they take 1 HP of crash damage
     * Ghost-type immunity is considered a miss for this move */
    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent) || MoveData.HasNoEffect(this, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
            yield return Battle.StartCoroutine(user.RecieveDamge(1, Type.NONE));
        }
        else
            yield return Battle.StartCoroutine(DealDamage(user, opponent));

        EndMove(user, opponent);
    }
}