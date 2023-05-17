using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnExecutionState : BattleBaseState
{
    private bool _executeSecondTurn;
    private bool _battleEnded;

    private Vector2 _answerBoxPosition = new Vector2(104, -72);

    #region Battle State Callbacks

    public override void EnterState(BattleStateManager battle)
    {
        if (_battle == null)
            _battle = battle;

        _battle.SelectionBox.SetActive(false);
        _battle.StartCoroutine(Execute());
    }

    public override void OnNavigate(InputAction.CallbackContext context) { }

    public override void OnConfirm(InputAction.CallbackContext context) { }

    public override void OnCancel(InputAction.CallbackContext context) { }

    #endregion

    private IEnumerator Execute()
    {
        _executeSecondTurn = true;
        _battleEnded = false;

        yield return _battle.StartCoroutine(ExecuteFirstTurn());

        if (_battle.SuccessfulRun)
            yield break;

        yield return _battle.StartCoroutine(CheckForPlayerFaint());
        if (_battleEnded)
            yield break;

        yield return _battle.StartCoroutine(CheckForOpponentFaint());
        if (_battleEnded)
            yield break;

        if (_executeSecondTurn)
        {
            yield return _battle.StartCoroutine(ExecuteSecondTurn());

            yield return _battle.StartCoroutine(CheckForPlayerFaint());
            if (_battleEnded)
                yield break;

            yield return _battle.StartCoroutine(CheckForOpponentFaint());
            if (_battleEnded)
                yield break;
        }

        if (_battle.PlayerSide.LockedIntoAction)
            _battle.SwitchState(_battle.TurnOrderState);
        else
            _battle.SwitchState(_battle.InputState);
    }

    private IEnumerator ExecuteTurn(BattleSide thisSide, BattleSide otherSide)
    {
        switch(thisSide.Action)
        {
            default:
                yield break;
            case BattleAction.UseMove:
                yield return _battle.StartCoroutine(AttemptMoveExecution(thisSide, otherSide));
                yield break;
            case BattleAction.SwitchPokemon:
                if (thisSide == _battle.PlayerSide)
                    yield return _battle.StartCoroutine(_battle.SwapPlayerPokemon());
                else
                    yield return _battle.StartCoroutine(_battle.SwapOpponentPokemon());

                yield break;
            case BattleAction.UseItem:

                yield break;
            case BattleAction.RunFromBattle:
                if (thisSide == _battle.OpponentSide)
                    yield break;

                yield return _battle.StartCoroutine(AttemptEscape(thisSide.ActivePokemon, otherSide.ActivePokemon));
                yield break;
        }
    }

    private IEnumerator AttemptMoveExecution(BattleSide thisSide, BattleSide otherSide)
    {
        BattlePokemon user = thisSide.ActivePokemon;

        // The user is unable to move if it flinches
        if (user.Flinched)
        {
            yield return _battle.StartCoroutine(OnFlinched());
            user.Flinched = false;
            yield break;
        }

        // Reduce the disable counter if the user is disabled
        if (user.Disabled)
            user.ReduceDisableCounter();

        BaseMove move = thisSide.Move;
        /* If the user is STILL disabled, check to see if the move being used is disabled
         * If so, the user does NOT use the move */
        if (user.Disabled && move == user.Moves[user.DisableIndex])
        {
            yield return _battle.StartCoroutine(OnMoveDisabled(move));
            yield break;
        }

        BattlePokemon opponent = otherSide.ActivePokemon;
        Debug.Log($"{user.Name} used {move.Name}!");
        yield return _battle.StartCoroutine(thisSide.Move.Execute(user, opponent));
    }

    private IEnumerator ExecuteFirstTurn()
    {
        yield return _battle.StartCoroutine(ExecuteTurn(_battle.FirstSide, _battle.SecondSide));
    }

    private IEnumerator ExecuteSecondTurn()
    {
        yield return _battle.StartCoroutine(ExecuteTurn(_battle.SecondSide, _battle.FirstSide));
    }

    private IEnumerator CheckForPlayerFaint()
    {
        BattlePokemon playerPokemon = _battle.PlayerSide.ActivePokemon;

        if (playerPokemon.Status != StatusEffect.FNT)
            yield break;

        _battle.PlayerSide.LockedIntoAction = false;
        _battle.PlayerSide.LockedIntoMove = false;
        // PLAYER FAINTED
        yield return _battle.StartCoroutine(OnFainted(playerPokemon));

        // MORE POKEMON AVAILABLE
        if (_battle.PlayerSide.IsAbleToFight())
        {
            // WILD BATTLE
            if (_battle.BattleType == BattleType.WILD_BATTLE)
            {
                yield return _battle.StartCoroutine(OnUseNext());

                AnswerBox.Open(true, _answerBoxPosition);
                while (!AnswerBox.Continue)
                    yield return null;
                
                // If NO, attempt to run
                if (!AnswerBox.Answer)
                {
                    yield return _battle.StartCoroutine(AttemptEscape(playerPokemon, _battle.OpponentSide.ActivePokemon));
                    if (_battle.SuccessfulRun)
                    {
                        // battle.EndBattle();
                        yield return _battle.StartCoroutine(_battle.CloseBattle());
                        _battleEnded = true;
                        yield break;
                    }
                }
            }
            // RUN FAILURE
            // CHOSE YES
            // OR TRAINER BATTLE
            _battle.ForcedSwap = true;
            _battle.BattleUI.SetActive(false);
            PokemonMenu.ClosedFromBattle += OnClosedPokemonMenu;
            BattleStateManager.OpenPokemonMenu(_battle);
            MessageBox.Close();
            while (_battle.ForcedSwap)
                yield return null;

            // Don't execute the next move if the player was supposed to go next
            if (_battle.SecondSide == _battle.PlayerSide)
                _executeSecondTurn = false;
            else
                _executeSecondTurn = true;
        }
        // NO MORE POKEMON
        else
        {
            yield return _battle.StartCoroutine(OnNoPokemon());
            yield return _battle.StartCoroutine(OnBlackedOut());

            // End battle as black out
            // battle.EndBattle();
            yield return _battle.StartCoroutine(_battle.CloseBattle());
            _battleEnded = true;
            yield break;
        }
    }

    private IEnumerator CheckForOpponentFaint()
    {
        BattlePokemon opponentPokemon = _battle.OpponentSide.ActivePokemon;
        if (opponentPokemon.Status != StatusEffect.FNT)
            yield break;

        _battle.OpponentSide.LockedIntoAction = false;
        _battle.OpponentSide.LockedIntoMove = false;
        // ENEMY FAINTED
        yield return _battle.StartCoroutine(OnFainted(opponentPokemon));
        yield return _battle.StartCoroutine(_battle.ApplyExperience(opponentPokemon.ReferencePokemon));
        _battle.Participants.Clear();
        // WILD BATTLE
        if (_battle.BattleType == BattleType.WILD_BATTLE)
        {
            _battleEnded = true;
            // battle.EndBattle();
            yield return _battle.StartCoroutine(_battle.CloseBattle());
            yield break;
        }
        // TRAINER BATTLE
        //      MORE POKEMON AVAILABLE
        //      set text over time "{trainer} "
        //      wait for input
        //      set text over time "Will {player}\nchange POKeMON?"
    }

    private IEnumerator AttemptEscape(BattlePokemon playerPokemon, BattlePokemon opponentPokemon)
    {
        int playerSpeed = playerPokemon.BattleStats.Speed;
        int opponentSpeed = opponentPokemon.BattleStats.Speed / 4 % 256;
        int threshold = 0;
        int runCheck = 0;
        _battle.RunCounter++;

        if(opponentSpeed != 0)
        {
            runCheck = playerSpeed * 32 / opponentSpeed + 30 * _battle.RunCounter;
            threshold = Random.Range(0, 256);
            if(runCheck < threshold)
            {
                yield return _battle.StartCoroutine(OnCannotEscape());
                yield break;
            }
        }

        if (opponentSpeed == 0 || runCheck >= threshold)
        {
            _battle.SuccessfulRun = true;
            yield return _battle.StartCoroutine(OnGotAway());

            // battle.EndBattle();
            yield return _battle.StartCoroutine(_battle.CloseBattle());
            yield break;
        }
    }

    private void OnClosedPokemonMenu()
    {
        _battle.BattleUI.SetActive(true);
        MessageBox.Open();
        MessageBox.Clear();

        PokemonMenu.ClosedFromBattle -= OnClosedPokemonMenu;
    }

    private IEnumerator OnMoveDisabled(BaseMove move)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.MOVE_DISABLED, move: move));
    }

    private IEnumerator OnCannotEscape()
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.CANNOT_ESCAPE));
    }

    private IEnumerator OnGotAway()
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.GOT_AWAY_SAFELY));
    }

    private IEnumerator OnFlinched()
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_FLINCHED));
    }

    private IEnumerator OnFainted(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.POKEMON_FAINTED, bPokemon: pokemon));
    }

    private IEnumerator OnUseNext()
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.USE_NEXT_POKEMON, waitForInput: false));
    }

    private IEnumerator OnNoPokemon()
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.OUT_OF_POKEMON));
    }

    private IEnumerator OnBlackedOut()
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.BLACKED_OUT));
    }
}