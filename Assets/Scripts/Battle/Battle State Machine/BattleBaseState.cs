using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BattleBaseState
{
    protected BattleStateManager _battle;

    public abstract void EnterState(BattleStateManager battle);

    public abstract void OnNavigate(InputAction.CallbackContext context);
    public abstract void OnConfirm(InputAction.CallbackContext context);
    public abstract void OnCancel(InputAction.CallbackContext context);
}