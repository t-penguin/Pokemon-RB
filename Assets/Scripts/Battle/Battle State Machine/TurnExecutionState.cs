using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnExecutionState : BattleBaseState
{
    private const string COULDNT_ESCAPE = "Couldn= escape!";
    private const string GOT_AWAY_SAFELY = "Got away safely!";
    private const string USE_NEXT_POKEMON = "Use next\nPOKÈMON?";

    private bool _executeSecondTurn;
    private bool _battleEnded;
    private bool _checkMultiTurn;

    private Vector2 _answerBoxPosition = new Vector2(104, -72);

    #region Battle State Callbacks

    public override void EnterState(BattleStateManager battle)
    {
        battle.SelectionBox.SetActive(false);
        battle.StartCoroutine(Execute(battle));
    }

    public override void UpdateState(BattleStateManager battle) { }

    public override void ExitState(BattleStateManager battle) { }

    public override void OnNavigate(BattleStateManager battle, InputAction.CallbackContext context) { }

    public override void OnConfirm(BattleStateManager battle, InputAction.CallbackContext context) { }

    public override void OnCancel(BattleStateManager battle, InputAction.CallbackContext context) { }

    #endregion

    private IEnumerator Execute(BattleStateManager battle)
    {
        _executeSecondTurn = true;
        _battleEnded = false;
        _checkMultiTurn = true;

        yield return battle.StartCoroutine(ExecuteFirstTurn(battle));

        if (battle.SuccessfulRun)
            yield break;

        yield return battle.StartCoroutine(CheckForPlayerFaint(battle));
        if (_battleEnded)
            yield break;

        yield return battle.StartCoroutine(CheckForOpponentFaint(battle));
        if (_battleEnded)
            yield break;

        if (_executeSecondTurn)
        {
            yield return battle.StartCoroutine(ExecuteSecondTurn(battle));

            yield return battle.StartCoroutine(CheckForPlayerFaint(battle));
            if (_battleEnded)
                yield break;

            yield return battle.StartCoroutine(CheckForOpponentFaint(battle));
            if (_battleEnded)
                yield break;
        }

        if (IsMultiTurnMoveActive(battle.PlayerSide.Move))
            battle.SwitchState(battle.TurnOrderState);
        else
            battle.SwitchState(battle.InputState);
        }

    private IEnumerator ExecuteTurn(BattleStateManager battle, BattleSide thisSide, BattleSide otherSide)
    {
        switch(thisSide.Action)
        {
            default:
                yield break;
            case BattleAction.UseMove:
                BattlePokemon user = thisSide.ActivePokemon;
                BattlePokemon opponent = otherSide.ActivePokemon;
                Debug.Log($"{user.Name} used {thisSide.Move.Name}!");
                yield return battle.StartCoroutine(thisSide.Move.Execute(user, opponent));
                yield break;
            case BattleAction.SwitchPokemon:
                if (thisSide == battle.PlayerSide)
                    yield return battle.StartCoroutine(battle.SwapPlayerPokemon());
                else
                    yield return battle.StartCoroutine(battle.SwapOpponentPokemon());

                yield break;
            case BattleAction.UseItem:

                yield break;
            case BattleAction.RunFromBattle:
                if (thisSide == battle.OpponentSide)
                    yield break;

                yield return battle.StartCoroutine(AttemptEscape(battle, thisSide.ActivePokemon, otherSide.ActivePokemon));
                yield break;
        }
    }

    private IEnumerator ExecuteFirstTurn(BattleStateManager battle)
    {
        yield return battle.StartCoroutine(ExecuteTurn(battle, battle.FirstSide, battle.SecondSide));
    }

    private IEnumerator ExecuteSecondTurn(BattleStateManager battle)
    {
        yield return battle.StartCoroutine(ExecuteTurn(battle, battle.SecondSide, battle.FirstSide));
    }

    private IEnumerator CheckForPlayerFaint(BattleStateManager battle)
    {
        BattlePokemon playerPokemon = battle.PlayerSide.ActivePokemon;

        if (playerPokemon.Status != StatusEffect.FNT)
            yield break;

        _checkMultiTurn = false;
        // PLAYER FAINTED
        // MORE POKEMON AVAILABLE
        if (battle.PlayerSide.IsAbleToFight())
        {
            // WILD BATTLE
            if (battle.BattleType == BattleType.WILD_BATTLE)
            {
                yield return battle.StartCoroutine(battle.DisplayMessage(USE_NEXT_POKEMON, false));

                AnswerBox.Open(true, _answerBoxPosition);
                while (!AnswerBox.Continue)
                    yield return null;
                
                // If NO, attempt to run
                if (!AnswerBox.Answer)
                {
                    yield return battle.StartCoroutine(AttemptEscape(battle, playerPokemon, battle.OpponentSide.ActivePokemon));
                    if (battle.SuccessfulRun)
                    {
                        // battle.EndBattle();
                        yield return battle.StartCoroutine(battle.CloseBattle());
                        _battleEnded = true;
                        yield break;
                    }
                }
            }
            // RUN FAILURE
            // CHOSE YES
            // OR TRAINER BATTLE
            battle.ForcedSwap = true;
            battle.BattleUI.SetActive(false);
            PokemonMenu.ClosedFromBattle += OnClosedPokemonMenu;
            BattleStateManager.OpenPokemonMenu(battle);
            MessageBox.Close();
            while (battle.ForcedSwap)
                yield return null;

            // Don't execute the next move if the player was supposed to go next
            if (battle.SecondSide == battle.PlayerSide)
                _executeSecondTurn = false;
            else
                _executeSecondTurn = true;
        }
        // NO MORE POKEMON
        else
        {
            string text = $"{PlayerData.Name} is out of\nuseable POKÈMON!";
            yield return battle.StartCoroutine(battle.DisplayMessage(text, true));

            text = $"{PlayerData.Name} blacked\nout!";
            yield return battle.StartCoroutine(battle.DisplayMessage(text, true));

            // End battle as black out
            // battle.EndBattle();
            yield return battle.StartCoroutine(battle.CloseBattle());
            _battleEnded = true;
            yield break;
        }
    }

    private IEnumerator CheckForOpponentFaint(BattleStateManager battle)
    {
        BattlePokemon opponentPokemon = battle.OpponentSide.ActivePokemon;
        if (opponentPokemon.Status != StatusEffect.FNT)
            yield break;

        // ENEMY FAINTED
        yield return battle.ApplyExperience(opponentPokemon.ReferencePokemon);
        battle.Participants.Clear();
        // WILD BATTLE
        if (battle.BattleType == BattleType.WILD_BATTLE)
        {
            _battleEnded = true;
            // battle.EndBattle();
            yield return battle.StartCoroutine(battle.CloseBattle());
            yield break;
        }
        // TRAINER BATTLE
        //      MORE POKEMON AVAILABLE
        //      set text over time "{trainer} "
        //      wait for input
        //      set text over time "Will {player}\nchange POKeMON?"
    }

    private IEnumerator AttemptEscape(BattleStateManager battle, BattlePokemon playerPokemon, BattlePokemon opponentPokemon)
    {
        int playerSpeed = playerPokemon.BattleStats.Speed;
        int opponentSpeed = opponentPokemon.BattleStats.Speed / 4 % 256;
        int threshold = 0;
        int runCheck = 0;
        battle.RunCounter++;

        if(opponentSpeed != 0)
        {
            runCheck = playerSpeed * 32 / opponentSpeed + 30 * battle.RunCounter;
            threshold = Random.Range(0, 256);
            if(runCheck < threshold)
            {
                yield return battle.StartCoroutine(battle.DisplayMessage(COULDNT_ESCAPE, false));
                yield return new WaitForSeconds(60 / 60f);
                yield break;
            }
        }

        if (opponentSpeed == 0 || runCheck >= threshold)
        {
            battle.SuccessfulRun = true;
            yield return battle.StartCoroutine(battle.DisplayMessage(GOT_AWAY_SAFELY, true));

            // battle.EndBattle();
            yield return battle.StartCoroutine(battle.CloseBattle());
            yield break;
        }
    }

    private void OnClosedPokemonMenu(BattleStateManager battle)
    {
        battle.BattleUI.SetActive(true);
        MessageBox.Open();
        MessageBox.Clear();

        PokemonMenu.ClosedFromBattle -= OnClosedPokemonMenu;
    }

    private bool IsMultiTurnMoveActive(BaseMove move)
    {
        if (!_checkMultiTurn)
            return false;

        if (move.GetType().BaseType != typeof(MultiTurnAttackMove))
            return false;

        MultiTurnAttackMove multiMove = (MultiTurnAttackMove)move;
        if (multiMove.TurnsLeft == 0)
            return false;

        return true;
    }
}