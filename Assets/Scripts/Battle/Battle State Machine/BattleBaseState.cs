using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BattleBaseState
{
    protected BattleStateManager _battle;

    /// <summary>
    /// Runs when first entering this state.
    /// Subscribe to input events here if needed.
    /// </summary>
    public abstract void EnterState(BattleStateManager battle);

    /// <summary>
    /// Runs when exiting this state.
    /// Unsubscribe from input events here if needed.
    /// </summary>
    public abstract void ExitState();
}