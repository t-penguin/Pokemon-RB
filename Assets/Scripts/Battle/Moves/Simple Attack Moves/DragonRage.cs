using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonRage : SimpleAttackMove
{
    public DragonRage(BattleStateManager battle)
        : base (
            name: "DRAGON RAGE",
            type: Type.DRAGON,
            category: Category.Special,
            basePP: 10,
            accuracy: 100,
            power: 0,
            battle: battle )
    {
        dealsFixedDamage = true;
        fixedDamage = 40;
    }

    /* This move deals 40 damage no matter what
     * It does not take into account resistances and weaknesses */
    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if(opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
            yield return Battle.StartCoroutine(OnMissed(user));
        else
            yield return Battle.StartCoroutine(opponent.RecieveDamge(40, Type));

        yield return new WaitForSeconds(60 / 60f);
        EndMove(user, opponent);
    }
}