using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitiveStatusMove : TransitiveMove
{
    /// <summary>
    /// Creates a status move that affects the target.
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="accuracy">The accuracy of this move. Min: 30, Max: 100, Increment: 5</param>
    /// <param name="battle">A reference to the current battle this move is in.</param>
    protected TransitiveStatusMove(string name, Type type, int basePP, int accuracy, BattleStateManager battle)
        : base(name, type, Category.Status, 0, basePP, accuracy, battle) { }

    /* Transitive Status moves have a chance to fail when used on the player
     * check < failedthreshold -> hit
     * failedthreshold < check < missedthreshold -> fail
     * missedthreshold < check -> miss */
    protected bool AccuracyCheck(BattlePokemon user, BattlePokemon defender, out bool failed)
    {
        // Randomly generated accuracy check [0, 256)
        int check = Random.Range(0, 256);
        failed = false;

        int missThreshold = (int)(Accuracy / 100f * 255);
        missThreshold = (int)(missThreshold * user.Accuracy * defender.Evasion);

        if (defender == Battle.PlayerSide.ActivePokemon)
        {
            int failThreshold = missThreshold - 64;
            failThreshold = (int)(failThreshold * user.Accuracy * defender.Evasion);

            if (failThreshold < check && check < missThreshold)
                failed = true;

            return check < failThreshold;
        }

        return check < missThreshold;
    }
}