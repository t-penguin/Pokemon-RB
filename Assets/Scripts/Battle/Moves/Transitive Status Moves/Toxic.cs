using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toxic : TransitiveStatusMove
{
    public Toxic(BattleStateManager battle)
        : base (
            name: "TOXIC",
            type: Type.POISON,
            basePP: 10,
            accuracy: 85,
            battle: battle )
    { }

    /* This move badly poisons the target.
     * The damage taken from this poison is calculated as N * x
     * where N is a counter starting at 1 and x is 1/16 of the target's max health (min. 1)
     * N is increased by 1 each time the target takes Toxic OR Leech Seed damage.
     * When target is affected by Haze, switches out, or when the battle ends, the poison becomes normal.
     * 
     * If a badly poisoned Pokemon successfully uses Rest, the poison will be cured but N will not reset.
     * N will then reset if the target is poisoned with Toxic again.
     * 
     * This move cannot affect Poison-types. */
    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        bool failed = false;
        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent, out failed))
        {
            if (failed)
                yield return Battle.StartCoroutine(OnFailed());
            else
                yield return Battle.StartCoroutine(OnMissed(user));
        }
        else if (opponent.HasNonVolatileStatus())
            yield return Battle.StartCoroutine(OnFailed());
        else
        {
            opponent.BadlyPoison();
            yield return Battle.StartCoroutine(OnBadlyPoisoned(opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}