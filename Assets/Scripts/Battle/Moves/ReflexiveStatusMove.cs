using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReflexiveStatusMove : BaseMove
{
    /// <summary>
    /// Creates a status move that affects only the user.
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="battle">A reference to the battle this move is in.</param>
    protected ReflexiveStatusMove(string name, Type type, int basePP, BattleStateManager battle)
        : base(name, type, Category.Status, 0, basePP, battle) { }

    // Executes the user's move, to be overriden by each move
    protected abstract IEnumerator Execute(BattlePokemon user);

    /* Passes execution to the single parameter Execute method as reflexive moves only affect the user.
     * Inheritance of this method is stopped using the sealed keyword */
    public sealed override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(Execute(user));
    }
}