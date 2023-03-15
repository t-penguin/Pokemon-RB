using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackMove : TransitiveMove
{
    public int Power { get; }
    public bool HasHighCritRatio { get; }

    private const string SUPER_EFFECTIVE = "It was\nsuper effective!";
    private const string NOT_EFFECTIVE = "It was not\nvery effective...";
    private const string CRITICAL_HIT = "Critical hit!";

    /// <summary>
    /// Creates a damaging move that affects the target.
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
    protected AttackMove(string name, Type type, Category category, int priority, int basePP, int accuracy, int power, 
        bool highCrit, BattleStateManager battle) : base (name, type, category, priority, basePP, accuracy, battle)
    {
        Power = power;
        HasHighCritRatio = highCrit;
    }

    // Calculates the amount of damage to deal to the target and then deals it
    protected virtual IEnumerator DealDamage(BattlePokemon user, BattlePokemon target)
    {
        float effectiveness = MoveData.GetMatchupMultiplier(this, target);
        int damage = MoveData.CalculateDamage(this, user, target, out bool isCrit);
        if (target.IsBideActive)
        {
            target.LastDamageRecieved = damage;
            target.BideDamage += 2 * damage;
        }
        yield return Battle.StartCoroutine(target.RecieveDamge(damage, Type));
        if (isCrit)
            yield return Battle.StartCoroutine(OnCriticalHit());
        yield return Battle.StartCoroutine(OnEffective(effectiveness));
        yield return new WaitForSeconds(60 / 60f);
    }

    // Displays a message depending on the effectiveness of the move
    protected IEnumerator OnEffective(float effectiveness)
    {
        if (effectiveness == 1)
            yield break;

        string text;
        if (effectiveness < 1)
            text = NOT_EFFECTIVE;
        else
            text = SUPER_EFFECTIVE;

        yield return Battle.StartCoroutine(Battle.DisplayMessage(text, true));
        yield return new WaitForSeconds(4 / 60f);
    }

    protected IEnumerator OnCriticalHit()
    {
        yield return Battle.StartCoroutine(Battle.DisplayMessage(CRITICAL_HIT, true));
        yield return new WaitForSeconds(4 / 60f);
    }
}