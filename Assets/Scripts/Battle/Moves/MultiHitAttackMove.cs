using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiHitAttackMove : AttackMove
{
    protected int NumberOfHits { get; private set; }
    protected bool RandomHits { get; set; }
    protected int Damage { get; private set; }

    /// <summary>
    /// Creates an attack move that damages the opponent multiple times in one turn.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="category"></param>
    /// <param name="basePP"></param>
    /// <param name="accuracy"></param>
    /// <param name="power"></param>
    /// <param name="battle"></param>
    protected MultiHitAttackMove(string name, Type type, Category category, int basePP, int accuracy, int power, BattleStateManager battle)
        : base(name, type, category, basePP, accuracy, power, battle) => NumberOfHits = 0;

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
        {
            yield return Battle.StartCoroutine(OnMissed(user));
        }
        else if (MoveData.HasNoEffect(this, opponent))
        {
            yield return Battle.StartCoroutine(OnNoEffect());
        }
        else
        {
            if(RandomHits)
                SetNumberOfHits();
            yield return Battle.StartCoroutine(DealDamage(user, opponent));
        }

        SetLastMoveUsed(user);
        SetMirrorMove(opponent);
        CurrentPP--;
    }

    protected void SetNumberOfHits(int hits) => NumberOfHits = hits;

    // Randomly sets the number of hits
    protected void SetNumberOfHits()
    {
        int random = Random.Range(0, 256);

        /* 3/8 chance each for 2 and 3 hits
         * 1/8 chance each for 4 and 5 hits */
        if (random < 96)        NumberOfHits = 2;
        else if (random < 192)  NumberOfHits = 3;
        else if (random < 224)  NumberOfHits = 4;
        else                    NumberOfHits = 5;
    }

    // Sets the amount of damage this move will do with each hit
    protected void SetDamage(int damage) => Damage = damage;

    protected override IEnumerator DealDamage(BattlePokemon user, BattlePokemon target)
    {
        int totalHits = 0;
        float effectiveness = MoveData.GetMatchupMultiplier(this, target);
        SetDamage(MoveData.CalculateDamage(this, user, target, out bool isCrit));
        for(int i = 0; i < NumberOfHits; i++)
        {
            // ADD HERE: Animation
            yield return Battle.StartCoroutine(target.RecieveDamge(Damage, Type));
            totalHits++;
            if (isCrit && i == 0)
                yield return Battle.StartCoroutine(OnCriticalHit());

            // REMOVE HERE when the animation has been added
            yield return new WaitForSeconds(40 / 60f);

            if (target.Status == StatusEffect.FNT)
                break;

            // ADD HERE: End immediately if substitute breaks
        }
        
        yield return Battle.StartCoroutine(OnEffective(effectiveness));
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"Hit the enemy\n{totalHits} times!", true));
        yield return new WaitForSeconds(60 / 60f);
    }
}