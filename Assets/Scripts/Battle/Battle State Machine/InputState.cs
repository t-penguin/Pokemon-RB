using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputState : BattleBaseState
{
    private const string NO_RUNNING = "No! There< no\nrunning from a\ntrainer battle!";
    private const string NO_PP = "No PP left for\nthis move!";

    int _selection;
    InputMenu currentMenu;

    #region Battle State Callbacks

    public override void EnterState(BattleStateManager battle)
    {
        _selection = 0;
        battle.SelectionArrow.localPosition = new Vector3(8, -16);
        currentMenu = InputMenu.Selection;
        MessageBox.Clear();
        battle.SelectionBox.SetActive(true);
    }

    public override void UpdateState(BattleStateManager battle)
    {
        
    }

    public override void ExitState(BattleStateManager battle)
    {
        
    }

    public override void OnNavigate(BattleStateManager battle, InputAction.CallbackContext context)
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

                position = battle.SelectionArrow.localPosition;
                if (direction.y > 0 && _selection > 1)
                {
                    _selection -= 2;
                    position.y += 16;
                    battle.SelectionArrow.localPosition = position;
                }
                else if (direction.y < 0 && _selection < 2)
                {
                    _selection += 2;
                    position.y -= 16;
                    battle.SelectionArrow.localPosition = position;
                }
                else if (direction.x > 0 && _selection % 2 == 0)
                {
                    _selection++;
                    position.x += 48;
                    battle.SelectionArrow.localPosition = position;
                }
                else if (direction.x < 0 && _selection % 2 == 1)
                {
                    _selection--;
                    position.x -= 48;
                    battle.SelectionArrow.localPosition = position;
                }
                break;
            case InputMenu.Moves:
                if (direction.y == 0)
                    return;

                int totalMoves = battle.PlayerSide.ActivePokemon.ReferencePokemon.GetNumberOfMoves();
                int selectionChange;
                position = battle.MoveArrow.localPosition;
                if (direction.y > 0)
                    selectionChange = battle.MoveSelection > 0 ? -1 : totalMoves - 1;
                else
                    selectionChange = battle.MoveSelection < totalMoves - 1 ? 1 : 1 - totalMoves;

                battle.MoveSelection += selectionChange;
                position.y = battle.MoveSelection * (-8) - 8;
                battle.MoveArrow.localPosition = position;
                battle.SetMoveInfo(battle.PlayerSide.ActivePokemon.Moves[battle.MoveSelection]);
                break;
        }
    }

    public override void OnConfirm(BattleStateManager battle, InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (currentMenu == InputMenu.None)
            return;

        switch (currentMenu)
        {
            case InputMenu.Selection:
                switch (_selection)
                {
                    case 0: // FIGHT
                        battle.StartCoroutine(OpenMovesMenu(battle));
                        break;
                    case 1: // POKEMON
                        BattleStateManager.SwappedPokemon += OnSwapPokemon;
                        PokemonMenu.ClosedFromBattle += OnClosedPokemonMenu;
                        BattleStateManager.OpenPokemonMenu(battle);
                        battle.BattleUI.SetActive(false);
                        MessageBox.Close();
                        currentMenu = InputMenu.None;
                        break;
                    case 2: // ITEM

                        break;
                    case 3: // RUN
                        if (battle.BattleType == BattleType.WILD_BATTLE)
                        {
                            battle.PlayerSide.Action = BattleAction.RunFromBattle;
                            battle.SwitchState(battle.TurnOrderState);
                        }
                        else
                            battle.StartCoroutine(AttemptRunFromTrainer(battle));
                        break;
                }
                break;
            case InputMenu.Moves:
                BattlePokemon pokemon = battle.PlayerSide.ActivePokemon;
                int index = battle.MoveSelection;
                if (pokemon.Moves[index].CurrentPP <= 0)
                {
                    battle.StartCoroutine(OnNoPPLeft(battle));
                    return;
                }

                battle.PlayerSide.SetMove(pokemon.Moves[index]);
                Debug.Log($"{pokemon.Name} will use {pokemon.Moves[index].Name}");
                battle.StartCoroutine(CloseMovesMenu(battle));
                battle.SwitchState(battle.TurnOrderState);
                break;
        }
    }

    public override void OnCancel(BattleStateManager battle, InputAction.CallbackContext context) 
    {
        if (!context.started)
            return;

        switch (currentMenu)
        {
            default:
                return;
            case InputMenu.Moves:
                battle.StartCoroutine(CloseMovesMenu(battle));
                break;
        }
    }

    #endregion

    private IEnumerator OpenMovesMenu(BattleStateManager battle)
    {
        currentMenu = InputMenu.None;
        yield return new WaitForSeconds(2 / 60f);
        BattlePokemon activePokemon = battle.PlayerSide.ActivePokemon;
        battle.SetMoveNames(activePokemon);
        battle.SetMoveInfo(activePokemon.Moves[battle.MoveSelection]);
        Vector3 position = battle.MoveArrow.localPosition;
        position.y = battle.MoveSelection * (-8) - 8;
        battle.MoveArrow.localPosition = position;
        battle.MovesBox.SetActive(true);
        battle.MoveInfoBox.SetActive(true);
        yield return new WaitForSeconds(2 / 60f);
        currentMenu = InputMenu.Moves;
    }

    private IEnumerator CloseMovesMenu(BattleStateManager battle)
    {
        currentMenu = InputMenu.None;
        yield return new WaitForSeconds(2 / 60f);
        battle.MovesBox.SetActive(false);
        battle.MoveInfoBox.SetActive(false);
        yield return new WaitForSeconds(2 / 60f);
        currentMenu = InputMenu.Selection;
    }

    private void OnClosedPokemonMenu(BattleStateManager battle)
    {
        battle.BattleUI.SetActive(true);
        MessageBox.Open();
        MessageBox.Clear();
        currentMenu = InputMenu.Selection;

        if (!battle.Swap)
            BattleStateManager.SwappedPokemon -= OnSwapPokemon;
        
        PokemonMenu.ClosedFromBattle -= OnClosedPokemonMenu;
    }

    private void OnSwapPokemon(BattleStateManager battle, Pokemon pokemon)
    {
        BattleStateManager.SwappedPokemon -= OnSwapPokemon;
        battle.PlayerSide.SetSwitch(pokemon);
        Debug.Log($"Switching out to {pokemon.Nickname}");
        battle.SelectionBox.SetActive(false);
        battle.SwitchState(battle.TurnOrderState);
    }

    private IEnumerator AttemptRunFromTrainer(BattleStateManager battle)
    {
        currentMenu = InputMenu.None;
        battle.SelectionBox.SetActive(false);
        yield return new WaitForSeconds(2 / 60f);

        yield return battle.StartCoroutine(battle.DisplayMessage(NO_RUNNING, true));

        MessageBox.Clear();
        yield return new WaitForSeconds(2 / 60f);
        battle.SelectionBox.SetActive(true);
        currentMenu = InputMenu.Selection;
    }

    private IEnumerator OnNoPPLeft(BattleStateManager battle)
    {
        currentMenu = InputMenu.None;
        yield return new WaitForSeconds(2 / 60f);
        MessageBox.BringToFront();

        yield return battle.StartCoroutine(battle.DisplayMessage(NO_PP, true));

        yield return new WaitForSeconds(2 / 60f);
        MessageBox.Clear();
        MessageBox.ResetSortOrder();
        currentMenu = InputMenu.Moves;
    }
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