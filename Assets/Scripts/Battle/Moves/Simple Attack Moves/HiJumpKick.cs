using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiJumpKick : SimpleAttackMove
{
    public HiJumpKick(BattleStateManager battle)
        : base (
            name: "HI JUMP KICK",
            type: Type.FIGHTING,
            category: Category.Physical,
            basePP: 20,
            accuracy: 90,
            power: 85,
            battle: battle )
    { }

    /* If the user misses this move, they take 1 HP of crash damage
     * Missing does not include the type immunity from Ghost-types */
    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        opponent.LastDamageRecieved = 0;

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
            yield return Battle.StartCoroutine(user.RecieveDamge(1, Type.NONE));
        }
        else if (MoveData.HasNoEffect(this, opponent))
            yield return Battle.StartCoroutine(OnNoEffect());
        else
            yield return Battle.StartCoroutine(DealDamage(user, opponent));

        EndMove(user, opponent);
    }
}