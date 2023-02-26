using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class TestInputManager : MonoBehaviour
{
    public static InputAction MoveAction { get; private set; }
    public static InputAction ConfirmAction { get; private set; }
    public static InputAction CancelAction { get; private set; }
    public static InputAction StartAction { get; private set; }
    public static InputAction SelectAction { get; private set; }

    private PlayerInput _playerInput;
    private InputActionMap _playerActions;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerActions = _playerInput.actions.FindActionMap("Player", true);
        MoveAction = _playerActions.FindAction("Move", true);
        ConfirmAction = _playerActions.FindAction("Confirm", true);
        CancelAction = _playerActions.FindAction("Cancel", true);
        StartAction = _playerActions.FindAction("Start", true);
        SelectAction = _playerActions.FindAction("Select", true);
    }
}