using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    // Singleton reference
    public static InputManager current;

    // Private fields for input
    private PlayerInput _pInput;
    private InputActionMap _playerActions;

    // Public Properties for actions
    public InputAction MoveAction { get; private set; }
    public InputAction ConfirmAction { get; private set; }
    public InputAction CancelAction { get; private set; }
    public InputAction StartAction { get; private set; }
    public InputAction SelectAction { get; private set; }

    // Initialize in Awake
    private void Awake()
    {
        current = this;
        _pInput = GetComponent<PlayerInput>();

        _playerActions = _pInput.actions.FindActionMap("Player", true);
        MoveAction = _playerActions.FindAction("Move", true);
        ConfirmAction = _playerActions.FindAction("Confirm", true);
        CancelAction = _playerActions.FindAction("Cancel", true);
        StartAction = _playerActions.FindAction("Start", true);
        SelectAction = _playerActions.FindAction("Select", true);
    }
}