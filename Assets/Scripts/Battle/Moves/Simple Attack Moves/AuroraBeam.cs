using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraBeam : SimpleAttackMove
{
    public AuroraBeam(BattleStateManager battle) : base("AURORA BEAM", Type.ICE, Category.Special, 0, 20, 100, 65, false, battle) { }

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
            bool secondaryEffect = opponent.CanStatBeLowered(StatType.Attack) && Random.Range(0, 3) == 0;
            if (opponentAlive && secondaryEffect)
            {
                opponent.ModifyStatAsSecondary(StatType.Attack, -1);
                yield return Battle.StartCoroutine(OnLoweredStat(opponent, StatType.Attack, false));
            }
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }
}