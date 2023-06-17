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

        TestInputManager.MoveAction.performed += OnNavigate;
        TestInputManager.MoveAction.canceled += OnNavigate;
        TestInputManager.ConfirmAction.performed += OnConfirm;
        TestInputManager.CancelAction.performed += OnCancel;
    }

    public override void ExitState()
    {
        TestInputManager.MoveAction.performed -= OnNavigate;
        TestInputManager.MoveAction.canceled -= OnNavigate;
        TestInputManager.ConfirmAction.performed -= OnConfirm;
        TestInputManager.CancelAction.performed -= OnCancel;
    }

    #endregion

    #region Input Callbacks

    public void OnNavigate(InputAction.CallbackContext context)
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

                BattlePokemon pokemon = _battle.PlayerSide.ActivePokemon;
                int totalMoves = pokemon.ReferencePokemon.GetNumberOfMoves();
                int selectionChange;
                position = _battle.MoveArrow.localPosition;
                if (direction.y > 0)
                    selectionChange = _battle.MoveSelection > 0 ? -1 : totalMoves - 1;
                else
                    selectionChange = _battle.MoveSelection < totalMoves - 1 ? 1 : 1 - totalMoves;

                _battle.MoveSelection += selectionChange;
                position.y = _battle.MoveSelection * (-8) - 8;
                _battle.MoveArrow.localPosition = position;
                bool disabledMove = pokemon.Disabled && _battle.MoveSelection == pokemon.DisableIndex;
                _battle.SetMoveInfo(pokemon.Moves[_battle.MoveSelection], disabledMove);
                break;
        }
    }

    public void OnConfirm(InputAction.CallbackContext context)
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

    public void OnCancel(InputAction.CallbackContext context) 
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
                if(!_battle.PlayerSide.ActivePokemon.HasUsableMove())
                {
                    int StruggleIndex = 135;
                    BaseMove struggle = MoveCreator.CreateMove(_battle, StruggleIndex);
                    _battle.PlayerSide.SetMove(struggle);
                    Debug.Log($"{_battle.PlayerSide.ActivePokemon.Name} will use {struggle.Name}");
                    _battle.SwitchState(_battle.TurnOrderState);
                    return;
                }
                
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
        BaseMove move = pokemon.Moves[index];
        if (move.CurrentPP <= 0)
        {
            _battle.StartCoroutine(MoveHasNoPP());
            return;
        }

        if(pokemon.Disabled && index == pokemon.DisableIndex)
        {
            _battle.StartCoroutine(SelectedDisabledMove(move));
            return;
        }

        _battle.PlayerSide.SetMove(move);
        Debug.Log($"{pokemon.Name} will use {move.Name}");
        _battle.StartCoroutine(CloseMovesMenu());
        _battle.SwitchState(_battle.TurnOrderState);
    }

    private IEnumerator OpenMovesMenu()
    {
        currentMenu = InputMenu.None;
        yield return new WaitForSeconds(2 / 60f);
        BattlePokemon pokemon = _battle.PlayerSide.ActivePokemon;
        _battle.SetMoveNames(pokemon);
        bool disabledMove = pokemon.Disabled && _battle.MoveSelection == pokemon.DisableIndex;
        _battle.SetMoveInfo(pokemon.Moves[_battle.MoveSelection], disabledMove);
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

    private IEnumerator SelectedDisabledMove(BaseMove move)
    {
        currentMenu = InputMenu.None;
        yield return new WaitForSeconds(2 / 60f);
        MessageBox.BringToFront();

        yield return _battle.StartCoroutine(OnSelectedDisabledMove(move));

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

    private IEnumerator OnSelectedDisabledMove(BaseMove move)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.MOVE_DISABLED, move: move));
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