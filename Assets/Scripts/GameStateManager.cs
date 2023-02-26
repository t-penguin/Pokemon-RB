using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TitleScreenHandler))]
[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(BattleStateManager))]

public class GameStateManager : MonoBehaviour
{
    public static IGameState currentGameState;

    public static TitleScreenHandler TitleScreenHandler;
    public static GameManager GameManager;
    public static InteractionManager InteractionManager;
    public static BattleStateManager BattleStateManager;

    public static event Action<IGameState> StateChanged;
    public static void ChangeState(IGameState state) => StateChanged?.Invoke(state);

    // Initialize State Managers in Awake
    private void Awake()
    {
        TitleScreenHandler = GetComponent<TitleScreenHandler>();
        GameManager = GetComponent<GameManager>();
        InteractionManager = GetComponent<InteractionManager>();
        BattleStateManager = GetComponent<BattleStateManager>();

        SwitchState(TitleScreenHandler);
    }

    private void OnEnable()
    {
        EventManager.current.InteractionStart += EnterInteraction;
        EventManager.current.InteractionEnd += ExitInteraction;
    }

    private void OnDisable()
    {
        EventManager.current.InteractionStart -= EnterInteraction;
        EventManager.current.InteractionEnd -= ExitInteraction;
    }

    public static void SwitchState(IGameState newState)
    {
        // Set the new state and enter the new state
        currentGameState = newState;
        currentGameState.EnterState();
        ChangeState(newState);
        Debug.Log($"Active state: {currentGameState.GetType()}");
    }

    private void EnterInteraction(Interaction interaction)
    {
        currentGameState.ExitState();
        InteractionManager.SetCurrentInteraction(interaction);
        SwitchState(InteractionManager);
    }

    private void ExitInteraction(Interaction interaction)
    {
        currentGameState.ExitState();
        SwitchState(GameManager);
    }
}