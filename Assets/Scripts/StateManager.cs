using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class StateManager : MonoBehaviour, IGameState
{
    public bool IsActiveState { get; set; }

    public virtual void EnterState()
    {
        IsActiveState = true;
        StartCoroutine(ListenForInput());
    }

    public virtual void ExitState()
    {
        IsActiveState = false;
        StopListeningForInput();
    }

    public IEnumerator ListenForInput()
    {
        // Delay listening to prevent functions being called immediately
        yield return new WaitForSeconds(6 / 60f);

        TestInputManager.MoveAction.performed += OnNavigate;
        TestInputManager.MoveAction.canceled += OnNavigate;
        TestInputManager.ConfirmAction.started += OnConfirm;
        TestInputManager.CancelAction.started += OnCancel;
    }

    public void StopListeningForInput()
    {
        TestInputManager.MoveAction.performed -= OnNavigate;
        TestInputManager.MoveAction.canceled -= OnNavigate;
        TestInputManager.ConfirmAction.started -= OnConfirm;
        TestInputManager.CancelAction.started -= OnCancel;
    }

    public abstract void OnNavigate(InputAction.CallbackContext context);
    public abstract void OnConfirm(InputAction.CallbackContext context);
    public abstract void OnCancel(InputAction.CallbackContext context);
}