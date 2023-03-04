using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnOrderState : BattleBaseState
{
    #region Battle State Callbacks

    public override void EnterState(BattleStateManager battle)
    {
        battle.OpponentSide.Action = DetermineOpponentAction(battle);
        SetTurnOrder(battle);
        battle.SwitchState(battle.ExecutionState);
    }

    public override void UpdateState(BattleStateManager battle) { }

    public override void ExitState(BattleStateManager battle) { }

    public override void OnNavigate(BattleStateManager battle, InputAction.CallbackContext context) { }

    public override void OnConfirm(BattleStateManager battle, InputAction.CallbackContext context) { }

    public override void OnCancel(BattleStateManager battle, InputAction.CallbackContext context) { }

    #endregion

    private BattleAction DetermineOpponentAction(BattleStateManager battle)
    {
        BaseMove move = battle.OpponentSide.Move;
        if (move == null)
            SetRandomMove(battle.OpponentSide);
        else if (move.GetType() == typeof(MultiTurnAttackMove))
        {
            MultiTurnAttackMove multiMove = (MultiTurnAttackMove)move;
            if (multiMove.TurnsLeft == 0)
                SetRandomMove(battle.OpponentSide);
        }
        else
            SetRandomMove(battle.OpponentSide);

        return BattleAction.UseMove;
    }

    private void SetRandomMove(BattleSide side)
    {
        BattlePokemon pokemon = side.ActivePokemon;
        int r = Random.Range(0, pokemon.ReferencePokemon.GetNumberOfMoves());
        side.Move = pokemon.Moves[r];
    }

    private void SetTurnOrder(BattleStateManager battle)
    {
        int playerPriority;
        int opponentPriority;

        // Player is using a move
        if (battle.PlayerSide.Action == BattleAction.UseMove)
            playerPriority = battle.PlayerSide.Move == null ? 0 : battle.PlayerSide.Move.Priority;
        // Player is switching, using an item, or attempting to run
        else
            playerPriority = 3;

        // Opponent is using a move
        if (battle.OpponentSide.Action == BattleAction.UseMove)
            opponentPriority = battle.OpponentSide.Move == null ? 0 : battle.OpponentSide.Move.Priority;
        // Opponent is switching or using an item
        else
            opponentPriority = 2;

        if (playerPriority > opponentPriority)
        {
            Debug.Log("Player has higher priority, going first.");
            battle.FirstSide = battle.PlayerSide;
            battle.SecondSide = battle.OpponentSide;
            return;
        }
        else if (playerPriority < opponentPriority)
        {
            Debug.Log("Opponent has higher priority, going second.");
            battle.FirstSide = battle.OpponentSide;
            battle.SecondSide = battle.PlayerSide;
            return;
        }

        // Speed checks
        int playerSpeed = CalculateModifiedSpeed(battle.PlayerSide.ActivePokemon);
        int opponentSpeed = CalculateModifiedSpeed(battle.OpponentSide.ActivePokemon);
        Debug.Log($"Player Speed: {playerSpeed}\nOpponent Speed: {opponentSpeed}");

        if (playerSpeed > opponentSpeed)
        {
            Debug.Log("Player's pokemon is faster, going first.");
            battle.FirstSide = battle.PlayerSide;
            battle.SecondSide = battle.OpponentSide;
            return;
        }
        else if (playerSpeed < opponentSpeed)
        {
            Debug.Log("Player's pokemon is slower, going second.");
            battle.FirstSide = battle.OpponentSide;
            battle.SecondSide = battle.PlayerSide;
            return;
        }

        // Priority and speed ties: random order
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            Debug.Log("Speed tie! Going first.");
            battle.FirstSide = battle.PlayerSide;
            battle.SecondSide = battle.OpponentSide;
        }
        else
        {
            Debug.Log("Speed tie! Going second.");
            battle.FirstSide = battle.OpponentSide;
            battle.SecondSide = battle.PlayerSide;
        }
    }

    private int CalculateModifiedSpeed(BattlePokemon pokemon)
    {
        int speed = pokemon.BattleStats.Speed;
        return pokemon.Status == StatusEffect.PAR ? Mathf.Max(speed / 4, 1) : speed;
    }
}