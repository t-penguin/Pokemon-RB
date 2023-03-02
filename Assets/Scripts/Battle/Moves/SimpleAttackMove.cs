using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleAttackMove : AttackMove
{
    /// <summary>
    /// Creates an attack move that only hits the target once.
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="category">The category of this move. (Physical or Special)</param>
    /// <param name="priority">This move's priority. Higher priority goes first.</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="accuracy">The accuracy of this move. Min: 30, Max: 100, Increment: 5</param>
    /// <param name="power">The power of this move. Increment: 5</param>
    /// <param name="highCrit">Whether or not this move has a high critical hit ratio</param>
    /// <param name="battle">A reference to the current battle this move is in.</param>
    protected SimpleAttackMove(string name, Type type, Category category, int priority, int basePP, int accuracy, int power,
        bool highCrit, BattleStateManager battle) : base(name, type, category, priority, basePP, accuracy, power, highCrit, battle) { }

    /* Calculates the amount of damage to deal to the user, then deals it.
     * The user is healed for half the amount of damage that was dealt.
     * The user is NOT healed if this move broke the target's substitute. */
    protected IEnumerator DealDamageAndRegainHealth(BattlePokemon user, BattlePokemon target)
    {
        int remainingHP = target.CurrentHP;
        float effectiveness = MoveData.GetMatchupMultiplier(this, target);
        int damage = MoveData.CalculateDamage(this, user, target, out bool isCrit);
        if (isCrit)
            yield return Battle.StartCoroutine(OnCriticalHit());
        yield return Battle.StartCoroutine(target.RecieveDamge(damage, Type));
        yield return Battle.StartCoroutine(OnEffective(effectiveness));

        // ADD HERE: Check for substitute break. Do NOT regain health if substitute breaks

        int sappedHealth = Mathf.CeilToInt(Mathf.Min(damage, remainingHP) / 2f);
        yield return Battle.StartCoroutine(user.RestoreHealth(sappedHealth));
    }
}