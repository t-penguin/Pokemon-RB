using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitiveMove : BaseMove
{
    public int Accuracy { get; }

    /// <summary>
    /// Creates a transitive move that affects the opponent
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="category">The category of this move. (Physical, Special, Status)</param>
    /// <param name="priority">This move's priority. Higher priority goes first.</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="accuracy">The accuracy of this move. Min: 30, Max: 100, Increment: 5</param>
    /// <param name="battle">A reference to the current battle this move is in.</param>
    protected TransitiveMove(string name, Type type, Category category, int priority, int basePP, int accuracy, BattleStateManager battle)
        : base(name, type, category, priority, basePP, battle) => Accuracy = accuracy;

    // Sets the defender's Mirror Move to use this move when used
    protected void SetMirrorMove(BattlePokemon opponent) => opponent.SetMirrorMove(this);

    // Performs an accuracy check and returns whether or not this move will hit the target
    protected bool AccuracyCheck(BattlePokemon user, BattlePokemon defender)
    {
        // Randomly generated accuracy check [0, 256)
        int check = Random.Range(0, 256);

        // The threshold of which to compare the accuracy check to
        int threshold = (int)(Accuracy / 100f * 255);
        threshold = (int)(threshold * user.Accuracy * defender.Evasion);
        // Clamps threshold between 1 and 255
        threshold = Mathf.Clamp(threshold, 1, 255);

        return check < threshold;
    }
}
