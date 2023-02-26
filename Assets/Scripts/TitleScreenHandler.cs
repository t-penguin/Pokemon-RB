using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenHandler : MonoBehaviour, IGameState
{
    public bool IsActiveState { get; set; }
    public GameStateManager Manager { get; set; }

    public void EnterState()
    {
        // Set this to be the active state
        IsActiveState = true;

        Debug.Log("Entering the Title Screen State.");
    }

    private void Update()
    {
        if (IsActiveState)
        {
            // Implement the Title Screen
            Debug.Log("The Title Screen is not yet implemented... Moving to the next state.");
            ExitState();
        }
    }

    public void ExitState()
    {
        IsActiveState = false;
        Debug.Log("Exiting the Title Screen State.");

        GameStateManager.SwitchState(GameStateManager.GameManager);
    }
}