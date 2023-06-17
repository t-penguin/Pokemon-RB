using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnOrderState : BattleBaseState
{
    #region Battle State Callbacks

    public override void EnterState(BattleStateManager battle)
    {
        if (_battle == null)
            _battle = battle;

        _battle.OpponentSide.Action = DetermineOpponentAction();
        SetTurnOrder();
        _battle.SwitchState(_battle.ExecutionState);
    }

    public override void ExitState() { }

    #endregion

    private BattleAction DetermineOpponentAction()
    {
        bool moveIsNull = _battle.OpponentSide.Move == null;
        bool lockedIntoAction = _battle.OpponentSide.LockedIntoAction;
        bool lockedIntoMove = _battle.OpponentSide.LockedIntoMove;
        if (moveIsNull || !lockedIntoAction || !lockedIntoMove)
            SetRandomMove(_battle.OpponentSide);

        return BattleAction.UseMove;
    }

    private void SetRandomMove(BattleSide side)
    {
        BattlePokemon pokemon = side.ActivePokemon;

        if(!pokemon.HasUsableMove())
        {
            int StruggleIndex = 135;
            side.Move = MoveCreator.CreateMove(_battle, StruggleIndex);
            return;
        }

        bool validMove = false;
        int moveIndex = 0;
        List<int> indexesWithPP = pokemon.GetMovesWithPP();

        while (!validMove)
        {
            int r = Random.Range(0, indexesWithPP.Count);
            moveIndex = indexesWithPP[r];
            validMove = !(pokemon.Disabled && moveIndex == pokemon.DisableIndex);
        }

        side.Move = pokemon.Moves[moveIndex];
    }

    private void SetTurnOrder()
    {
        int playerPriority;
        int opponentPriority;

        // Player is using a move
        if (_battle.PlayerSide.Action == BattleAction.UseMove)
            playerPriority = _battle.PlayerSide.Move == null ? 0 : _battle.PlayerSide.Move.Priority;
        // Player is switching, using an item, or attempting to run
        else
            playerPriority = 3;

        // Opponent is using a move
        if (_battle.OpponentSide.Action == BattleAction.UseMove)
            opponentPriority = _battle.OpponentSide.Move == null ? 0 : _battle.OpponentSide.Move.Priority;
        // Opponent is switching or using an item
        else
            opponentPriority = 2;

        if (playerPriority > opponentPriority)
        {
            Debug.Log("Player has higher priority, going first.");
            _battle.FirstSide = _battle.PlayerSide;
            _battle.SecondSide = _battle.OpponentSide;
            return;
        }
        else if (playerPriority < opponentPriority)
        {
            Debug.Log("Opponent has higher priority, going second.");
            _battle.FirstSide = _battle.OpponentSide;
            _battle.SecondSide = _battle.PlayerSide;
            return;
        }

        // Speed checks
        int playerSpeed = CalculateModifiedSpeed(_battle.PlayerSide.ActivePokemon);
        int opponentSpeed = CalculateModifiedSpeed(_battle.OpponentSide.ActivePokemon);
        Debug.Log($"Player Speed: {playerSpeed}\nOpponent Speed: {opponentSpeed}");

        if (playerSpeed > opponentSpeed)
        {
            Debug.Log("Player's pokemon is faster, going first.");
            _battle.FirstSide = _battle.PlayerSide;
            _battle.SecondSide = _battle.OpponentSide;
            return;
        }
        else if (playerSpeed < opponentSpeed)
        {
            Debug.Log("Player's pokemon is slower, going second.");
            _battle.FirstSide = _battle.OpponentSide;
            _battle.SecondSide = _battle.PlayerSide;
            return;
        }

        // Priority and speed ties: random order
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            Debug.Log("Speed tie! Going first.");
            _battle.FirstSide = _battle.PlayerSide;
            _battle.SecondSide = _battle.OpponentSide;
        }
        else
        {
            Debug.Log("Speed tie! Going second.");
            _battle.FirstSide = _battle.OpponentSide;
            _battle.SecondSide = _battle.PlayerSide;
        }
    }

    private int CalculateModifiedSpeed(BattlePokemon pokemon)
    {
        int speed = pokemon.BattleStats.Speed;
        return pokemon.Paralyzed ? Mathf.Max(speed / 4, 1) : speed;
    }
}