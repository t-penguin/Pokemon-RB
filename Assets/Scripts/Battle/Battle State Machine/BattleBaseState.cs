using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BattleBaseState
{
    public abstract void EnterState(BattleStateManager battle);
    public abstract void UpdateState(BattleStateManager battle);
    public abstract void ExitState(BattleStateManager battle);

    public abstract void OnNavigate(BattleStateManager battle, InputAction.CallbackContext context);
    public abstract void OnConfirm(BattleStateManager battle, InputAction.CallbackContext context);
    public abstract void OnCancel(BattleStateManager battle, InputAction.CallbackContext context);
}