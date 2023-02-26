using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : StateManager, IGameState
{
    public Player player;
    public PlayerMovement playerMovement;

    // Handle logic when entering this state
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entering the main game state.");
        Movement.CanMove = true;

        MainMenu.CanOpen = true;
        Movement.CanMove = true;
        //InputManager.current.StartAction.started += OpenMainMenu;
    }

    // Handle logic when exiting this state
    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Leaving the main game state.");

        MainMenu.CanOpen = false;
        Movement.CanMove = false;
        //InputManager.current.StartAction.started -= OpenMainMenu;
    }

    public override void OnNavigate(InputAction.CallbackContext context)
    {
        //if(IsActiveState)
            //playerMovement.OnMovePlayer(context);
    }

    public override void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.started && IsActiveState)
            playerMovement.AttemptInteraction();
    }

    public override void OnCancel(InputAction.CallbackContext context) { }
}