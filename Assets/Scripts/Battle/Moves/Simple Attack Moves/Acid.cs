using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : SimpleAttackMove
{
    public Acid(BattleStateManager battle) : base("ACID", Type.POISON, Category.Special, 0, 30, 100, 40, false, battle) { }

    public sealed override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
        }
        else
        {
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
            bool opponentAlive = opponent.Status != StatusEffect.FNT;
            bool secondaryEffect = opponent.CanStatBeLowered(StatType.Defense) && Random.Range(0, 3) == 0;
            if (opponentAlive && secondaryEffect)
            {
                opponent.ModifyStatAsSecondary(StatType.Defense, -1);
                yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.Defense, false));
            }
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}