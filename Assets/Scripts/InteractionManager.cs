using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : StateManager, IGameState
{
    Interaction _currentInteraction;

    public override void EnterState()
    {
        Debug.Log("Entering the Interaction State.");
        base.EnterState();
        _currentInteraction.StartInteraction(this);
    }

    public override void ExitState()
    {
        Debug.Log("Leaving the Interaction State.");
        base.ExitState();
        GameStateManager.SwitchState(GameStateManager.GameManager);
    }

    public override void OnNavigate(InputAction.CallbackContext context) { }

    public override void OnConfirm(InputAction.CallbackContext context) => _currentInteraction.OnConfirm(context);

    public override void OnCancel(InputAction.CallbackContext context) => _currentInteraction.OnCancel(context);

    public void SetCurrentInteraction(Interaction interaction) => _currentInteraction = interaction;
}