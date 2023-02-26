using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Menu : MonoBehaviour
{
    protected Sprite _filledArrow;
    protected Sprite _emptyArrow;
    protected float _blinkDelay;

    protected virtual void Awake()
    {
        _filledArrow = Resources.Load<Sprite>("UI/Filled Arrow");
        _emptyArrow = Resources.Load<Sprite>("UI/Empty Arrow");
    }

    protected abstract void OnNavigate(InputAction.CallbackContext context);
    protected abstract void OnConfirm(InputAction.CallbackContext context);
    protected abstract void OnCancel(InputAction.CallbackContext context);

    protected virtual void ListenForInput()
    {
        // Navigation input
        TestInputManager.MoveAction.performed   += OnNavigate;
        TestInputManager.MoveAction.canceled    += OnNavigate;
        // Confirmation input
        TestInputManager.ConfirmAction.started  += OnConfirm;
        // Cancelation input
        TestInputManager.CancelAction.started   += OnCancel;
    }

    protected virtual void StopListeningForInput()
    {
        // Navigation input
        TestInputManager.MoveAction.performed   -= OnNavigate;
        TestInputManager.MoveAction.canceled    -= OnNavigate;
        // Confirmation input
        TestInputManager.ConfirmAction.started  -= OnConfirm;
        // Cancelation input
        TestInputManager.CancelAction.started   -= OnCancel;
    }

    // Flashes a GameObject on and off
    protected void BlinkObject(GameObject gObject, float maxDelay)
    {
        if (_blinkDelay <= 0)
            gObject.SetActive(!gObject.activeSelf);

        _blinkDelay = _blinkDelay <= 0 ? maxDelay : _blinkDelay - Time.deltaTime;
    }

    // Flashes a List of GameObjects on and off in sync
    protected void BlinkObjects(List<GameObject> gObjects, float maxDelay)
    {
        if (_blinkDelay <= 0)
        {
            foreach (GameObject o in gObjects)
                o.SetActive(!o.activeSelf);
        }

        _blinkDelay = _blinkDelay <= 0 ? maxDelay : _blinkDelay - Time.deltaTime;
    }
}