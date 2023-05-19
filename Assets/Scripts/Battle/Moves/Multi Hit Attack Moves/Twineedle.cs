using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twineedle : MultiHitAttackMove
{
    public Twineedle(BattleStateManager battle)
        : base (
            name: "TWINEEDLE",
            type: Type.BUG,
            category: Category.Physical,
            basePP: 20,
            accuracy: 100,
            power: 25,
            battle: battle )
    {
        RandomHits = false;
        SetNumberOfHits(2);
    }

    /* The second hit of this move has a 20% chance of poisoning the target,
     * unless the target is Posion-typed. */
    protected override IEnumerator DealDamage(BattlePokemon user, BattlePokemon target)
    {
        int totalHits = 0;
        float effectiveness = MoveData.GetMatchupMultiplier(this, target);
        SetDamage(MoveData.CalculateDamage(this, user, target, out bool isCrit));
        for (int i = 0; i < NumberOfHits; i++)
        {
            // ADD HERE: Animation
            yield return Battle.StartCoroutine(target.RecieveDamge(Damage));
            totalHits++;
            if (isCrit && i == 0)
                yield return Battle.StartCoroutine(OnCriticalHit());

            // REMOVE HERE when the animation has been added
            yield return new WaitForSeconds(40 / 60f);

            if (target.Status == StatusEffect.FNT)
                break;

            // ADD HERE: End immediately if substitute breaks
        }

        yield return Battle.StartCoroutine(OnEffectiveness(effectiveness));
        yield return Battle.StartCoroutine(OnMultiHit(totalHits));

        bool targetIsAlive = target.Status != StatusEffect.FNT;
        bool poisonHits = Random.Range(0, 10) < 2;
        if(targetIsAlive && !target.IsType(Type.POISON) && poisonHits)
        {
            target.Poison();
            yield return Battle.StartCoroutine(OnPoisoned(target));
        }
    }
}