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
            // ANIMATION OFF
            // ???
            yield return Battle.StartCoroutine(DealDamageAndRegainHealth(user, opponent));
            yield return Battle.StartCoroutine(OnDreamEaten(opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }

    private IEnumerator OnDreamEaten(BattlePokemon opponent)
    {
        string text;
        if (opponent.TrainerIsPlayer)
            text = $"{opponent.Name}<\ndream was eaten!";
        else
            text = $"Enemy {opponent.Name}<\ndream was eaten!";

        yield return Battle.StartCoroutine(Battle.DisplayMessage(text, true));
        yield return new WaitForSeconds(4 / 60f);
    }
}