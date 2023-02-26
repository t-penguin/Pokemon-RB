using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interaction : MonoBehaviour
{
    public static event Action ConfirmPressed;
    public static event Action ConfirmReleased;
    public static event Action CancelPressed;
    public static event Action CancelReleased;
    public static event Action<InputAction.CallbackContext> Navigate;

    protected InteractionManager _manager;

    public virtual void StartInteraction(InteractionManager manager) =>_manager = manager;

    public virtual void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.started)
            ConfirmPressed?.Invoke();
        else if (context.canceled)
            ConfirmReleased?.Invoke();
    }

    public virtual void OnCancel(InputAction.CallbackContext context)
    {
        if (context.started)
            CancelPressed?.Invoke();
        else if (context.canceled)
            CancelReleased?.Invoke();
    }

    public virtual void OnNavigate(InputAction.CallbackContext context) => Navigate?.Invoke(context);
    public abstract void EndInteraction();
}