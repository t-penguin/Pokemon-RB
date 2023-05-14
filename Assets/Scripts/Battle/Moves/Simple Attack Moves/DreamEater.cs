using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamEater : SimpleAttackMove
{
    public DreamEater(BattleStateManager battle)
        : base (
            name: "DREAM EATER",
            type: Type.PSYCHIC,
            category: Category.Special,
            basePP: 15,
            accuracy: 100,
            power: 100,
            battle: battle ) 
    {
        sapsHealth = true;
    }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
        }
        else if (opponent.Status != StatusEffect.SLP)
        {
            yield return Battle.StartCoroutine(OnFailed());
        }
        else
        {
            yield return Battle.StartCoroutine(DealDamageAndRegainHealth(user, opponent));
            yield return Battle.StartCoroutine(OnDreamEaten(opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}