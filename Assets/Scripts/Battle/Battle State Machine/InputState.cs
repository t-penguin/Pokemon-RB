using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputState : BattleBaseState
{
    int _selection;
    InputMenu currentMenu;

    #region Battle State Callbacks

    public override void EnterState(BattleStateManager battle)
    {
        if (_battle == null)
            _battle = battle;

        _selection = 0;
        battle.SelectionArrow.localPosition = new Vector3(8, -16);
        currentMenu = InputMenu.Selection;
        MessageBox.Clear();
        battle.SelectionBox.SetActive(true);
    }

    public override void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (currentMenu == InputMenu.None)
            return;

        Vector2 direction = context.ReadValue<Vector2>();
        Vector3 position;
        switch (currentMenu)
        {
            case InputMenu.Selection:
                if (direction == Vector2.zero)
                    return;

                position = _battle.SelectionArrow.localPosition;
                if (direction.y > 0 && _selection > 1)
                {
                    _selection -= 2;
                    position.y += 16;
                    _battle.SelectionArrow.localPosition = position;
                }
                else if (direction.y < 0 && _selection < 2)
                {
                    _selection += 2;
                    position.y -= 16;
                    _battle.SelectionArrow.localPosition = position;
                }
                else if (direction.x > 0 && _selection % 2 == 0)
                {
                    _selection++;
                    position.x += 48;
                    _battle.SelectionArrow.localPosition = position;
                }
                else if (direction.x < 0 && _selection % 2 == 1)
                {
                    _selection--;
                    position.x -= 48;
                    _battle.SelectionArrow.localPosition = position;
                }
                break;
            case InputMenu.Moves:
                if (direction.y == 0)
                    return;

                int totalMoves = _battle.PlayerSide.ActivePokemon.ReferencePokemon.GetNumberOfMoves();
                int selectionChange;
                position = _battle.MoveArrow.localPosition;
                if (direction.y > 0)
                    selectionChange = _battle.MoveSelection > 0 ? -1 : totalMoves - 1;
                else
                    selectionChange = _battle.MoveSelection < totalMoves - 1 ? 1 : 1 - totalMoves;

                _battle.MoveSelection += selectionChange;
                position.y = _battle.MoveSelection * (-8) - 8;
                _battle.MoveArrow.localPosition = position;
                _battle.SetMoveInfo(_battle.PlayerSide.ActivePokemon.Moves[_battle.MoveSelection]);
                break;
        }
    }

    public override void OnConfirm(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (currentMenu == InputMenu.None)
            return;

        switch (currentMenu)
        {
            case InputMenu.Selection:
                HandleActionSelection();
                break;
            case InputMenu.Moves:
                HandleMoveSelection();
                break;
        }
    }

    public override void OnCancel(InputAction.CallbackContext context) 
    {
        if (!context.started)
            return;

        switch (currentMenu)
        {
            default:
                return;
            case InputMenu.Moves:
                _battle.StartCoroutine(CloseMovesMenu());
                break;
        }
    }

    #endregion

    private void HandleActionSelection()
    {
        switch (_selection)
        {
            case 0: // FIGHT
                if (_battle.PlayerSide.LockedIntoMove)
                    _battle.SwitchState(_battle.TurnOrderState);
                else
                    _battle.StartCoroutine(OpenMovesMenu());
                break;
            case 1: // POKEMON
                BattleStateManager.SwappedPokemon += OnSwapPokemon;
                PokemonMenu.ClosedFromBattle += OnClosedPokemonMenu;
                BattleStateManager.OpenPokemonMenu(_battle);
                _battle.BattleUI.SetActive(false);
                MessageBox.Close();
                currentMenu = InputMenu.None;
                break;
            case 2: // ITEM

                break;
            case 3: // RUN
                if (_battle.BattleType == BattleType.WILD_BATTLE)
                {
                    _battle.PlayerSide.Action = BattleAction.RunFromBattle;
                    _battle.SwitchState(_battle.TurnOrderState);
                }
                else
                    _battle.StartCoroutine(AttemptRunFromTrainer());
                break;
        }
    }

    private void HandleMoveSelection()
    {
        BattlePokemon pokemon = _battle.PlayerSide.ActivePokemon;
        int index = _battle.MoveSelection;
        if (pokemon.Moves[index].CurrentPP <= 0)
        {
            _battle.StartCoroutine(MoveHasNoPP());
            return;
        }

        _battle.PlayerSide.SetMove(pokemon.Moves[index]);
        Debug.Log($"{pokemon.Name} will use {pokemon.Moves[index].Name}");
        _battle.StartCoroutine(CloseMovesMenu());
        _battle.SwitchState(_battle.TurnOrderState);
    }

    private IEnumerator OpenMovesMenu()
    {
        currentMenu = InputMenu.None;
        yield return new WaitForSeconds(2 / 60f);
        BattlePokemon activePokemon = _battle.PlayerSide.ActivePokemon;
        _battle.SetMoveNames(activePokemon);
        _battle.SetMoveInfo(activePokemon.Moves[_battle.MoveSelection]);
        Vector3 position = _battle.MoveArrow.localPosition;
        position.y = _battle.MoveSelection * (-8) - 8;
        _battle.MoveArrow.localPosition = position;
        _battle.MovesBox.SetActive(true);
        _battle.MoveInfoBox.SetActive(true);
        yield return new WaitForSeconds(2 / 60f);
        currentMenu = InputMenu.Moves;
    }

    private IEnumerator CloseMovesMenu()
    {
        currentMenu = InputMenu.None;
        yield return new WaitForSeconds(2 / 60f);
        _battle.MovesBox.SetActive(false);
        _battle.MoveInfoBox.SetActive(false);
        yield return new WaitForSeconds(2 / 60f);
        currentMenu = InputMenu.Selection;
    }

    private void OnClosedPokemonMenu()
    {
        _battle.BattleUI.SetActive(true);
        MessageBox.Open();
        MessageBox.Clear();
        currentMenu = InputMenu.Selection;

        if (!_battle.Swap)
            BattleStateManager.SwappedPokemon -= OnSwapPokemon;
        
        PokemonMenu.ClosedFromBattle -= OnClosedPokemonMenu;
    }

    private void OnSwapPokemon(Pokemon pokemon)
    {
        BattleStateManager.SwappedPokemon -= OnSwapPokemon;
        _battle.PlayerSide.SetSwitch(pokemon);
        Debug.Log($"Switching out to {pokemon.Nickname}");
        _battle.SelectionBox.SetActive(false);
        _battle.SwitchState(_battle.TurnOrderState);
    }

    private IEnumerator AttemptRunFromTrainer()
    {
        currentMenu = InputMenu.None;
        _battle.SelectionBox.SetActive(false);
        yield return new WaitForSeconds(2 / 60f);

        yield return _battle.StartCoroutine(OnNoRunning());

        MessageBox.Clear();
        yield return new WaitForSeconds(2 / 60f);
        _battle.SelectionBox.SetActive(true);
        currentMenu = InputMenu.Selection;
    }

    private IEnumerator MoveHasNoPP()
    {
        currentMenu = InputMenu.None;
        yield return new WaitForSeconds(2 / 60f);
        MessageBox.BringToFront();

        yield return _battle.StartCoroutine(OnMoveNoPP());

        yield return new WaitForSeconds(2 / 60f);
        MessageBox.Clear();
        MessageBox.ResetSortOrder();
        currentMenu = InputMenu.Moves;
    }

    #region Messages

    private IEnumerator OnNoRunning()
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.NO_RUNNING));
    }

    private IEnumerator OnMoveNoPP()
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.NO_PP));
    }

    #endregion
}

public enum InputMenu
{
    Selection,
    Moves,
    Items,
    Pokemon,
    None
}

public enum BattleAction
{
    UseMove,
    SwitchPokemon,
    UseItem,
    RunFromBattle
}