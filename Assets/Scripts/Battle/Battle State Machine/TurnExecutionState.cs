using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnExecutionState : BattleBaseState
{
    private bool _executeSecondTurn;

    private Vector2 _answerBoxPosition = new Vector2(104, -72);

    #region Battle State Callbacks

    public override void EnterState(BattleStateManager battle)
    {
        if (_battle == null)
            _battle = battle;

        _battle.SelectionBox.SetActive(false);
        _battle.StartCoroutine(Execute());
    }

    public override void ExitState() { }

    #endregion

    private IEnumerator Execute()
    {
        _executeSecondTurn = true;

        yield return _battle.StartCoroutine(ExecuteFirstTurn());

        if (_executeSecondTurn)
            yield return _battle.StartCoroutine(ExecuteSecondTurn());

        if (_battle.PlayerSide.LockedIntoAction)
            _battle.SwitchState(_battle.TurnOrderState);
        else
            _battle.SwitchState(_battle.InputState);
    }

    private IEnumerator ExecuteTurn(BattleSide thisSide, BattleSide otherSide)
    {
        BattlePokemon user = thisSide.ActivePokemon;
        BattlePokemon opponent = otherSide.ActivePokemon;

        switch(thisSide.Action)
        {
            default:
                yield break;
            case BattleAction.UseMove:
                yield return _battle.StartCoroutine(AttemptMoveExecution(thisSide, otherSide));
                break;
            case BattleAction.SwitchPokemon:
                if (thisSide == _battle.PlayerSide)
                    yield return _battle.StartCoroutine(_battle.SwapPlayerPokemon());
                else
                    yield return _battle.StartCoroutine(_battle.SwapOpponentPokemon());
                break;
            case BattleAction.UseItem:

                break;
            case BattleAction.RunFromBattle:
                if (thisSide == _battle.PlayerSide)
                    yield return _battle.StartCoroutine(AttemptEscape(user, opponent));
                break;
        }

        yield return new WaitForSeconds(30 / 60f);
        yield return _battle.StartCoroutine(CheckForFaint());

        if (user.Alive)
            yield return _battle.StartCoroutine(AttemptRecurrentDamage(user, opponent));
    }

    private IEnumerator AttemptMoveExecution(BattleSide thisSide, BattleSide otherSide)
    {
        BattlePokemon user = thisSide.ActivePokemon;

        /* The user is unable to move if it is sleeping.
         * The sleep counter is reduced BEFORE checking for the final turn of sleep.
         * The user wakes up on the final turn but is still unable to move. */
        if(user.Asleep)
        {
            user.DecreaseSleepCounter();
            if (user.SleepCounter > 0)
                yield return _battle.StartCoroutine(OnSleeping(user));
            else
            {
                yield return _battle.StartCoroutine(OnWokeUp(user));
                user.ClearNonVolatileStatus();
            }
            
            yield break;
        }

        /* The user is unable to move if it is frozen.
         * This lasts until the user is hit with a fire type move. */
        if(user.Frozen)
        {
            yield return _battle.StartCoroutine(OnStillFrozen(user));
            yield break;
        }

        // The user is unable to move if it flinches
        if (user.Flinched)
        {
            yield return _battle.StartCoroutine(OnFlinched());
            user.Flinched = false;
            yield break;
        }

        /* Reduce the disable counter if the user is disabled
         * Skip this check if the user is recharging. */
        if (user.Disabled && !user.Recharging)
        {
            user.ReduceDisableCounter();

            if (!user.Disabled)
                yield return _battle.StartCoroutine(OnNoLongerDisabled(user));
        }

        /* Check if the user will hurt itself due to confusion
         * There is a 50% chance the user hurts itself */
        if (user.Confused)
        {
            int r = Random.Range(0, 2);
            if (r == 1)
            {
                int confusionDamage = CalculateConfusionDamage(user);
                yield return _battle.StartCoroutine(user.RecieveDamge(confusionDamage));
                yield return _battle.StartCoroutine(OnHurtByConfusion(user));
                yield break;
            }
        }

        /* Check if the user is immobilized by paralysis
         * There is a 25% chance the user cannot move */
        if(user.Paralyzed)
        {
            int r = Random.Range(0, 4);
            if (r == 0)
            {
                yield return _battle.StartCoroutine(OnFullyParalyzed(user));
                yield break;
            }
        }

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

    private IEnumerator CheckForFaint()
    {
        yield return _battle.StartCoroutine(CheckForPlayerFaint());
        yield return _battle.StartCoroutine(CheckForOpponentFaint());
    }

    private IEnumerator CheckForPlayerFaint()
    {
        BattlePokemon playerPokemon = _battle.PlayerSide.ActivePokemon;

        if (playerPokemon.Alive)
            yield break;

        _battle.PlayerSide.LockedIntoAction = false;
        _battle.PlayerSide.LockedIntoMove = false;
        // PLAYER FAINTED
        yield return _battle.StartCoroutine(OnFainted(playerPokemon));

        // MORE POKEMON AVAILABLE
        if (_battle.PlayerSide.IsAbleToFight())
        {
            // WILD BATTLE
            if (_battle.BattleType == BattleType.Wild)
            {
                yield return _battle.StartCoroutine(OnUseNext());

                AnswerBox.Open(true, _answerBoxPosition);
                while (!AnswerBox.Continue)
                    yield return null;
                
                // If NO, attempt to run
                if (!AnswerBox.Answer)
                    yield return _battle.StartCoroutine(AttemptEscape(playerPokemon, _battle.OpponentSide.ActivePokemon));
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
            yield break;
        }
    }

    private IEnumerator CheckForOpponentFaint()
    {
        BattlePokemon opponentPokemon = _battle.OpponentSide.ActivePokemon;
        if (opponentPokemon.Alive)
            yield break;

        _battle.OpponentSide.LockedIntoAction = false;
        _battle.OpponentSide.LockedIntoMove = false;
        // ENEMY FAINTED
        yield return _battle.StartCoroutine(OnFainted(opponentPokemon));
        yield return _battle.StartCoroutine(_battle.ApplyExperience(opponentPokemon.ReferencePokemon));
        _battle.Participants.Clear();
        // WILD BATTLE
        if (_battle.BattleType == BattleType.Wild)
        {
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

    private IEnumerator AttemptRecurrentDamage(BattlePokemon pokemon, BattlePokemon opponent)
    {
        bool afflicted = pokemon.Burned || pokemon.Poisoned || pokemon.Seeded;
        if (!afflicted)
            yield break;

        int baseReccurentDamage = Mathf.Max(1, pokemon.Stats.HP / 16);

        if(pokemon.Burned)
        {
            yield return _battle.StartCoroutine(pokemon.RecieveDamge(baseReccurentDamage));
            yield return _battle.StartCoroutine(OnReccurentBurn(pokemon));
        }
        else if(pokemon.Poisoned)
        {
            int damage = baseReccurentDamage;
            if (pokemon.BadlyPoisoned)
            {
                damage *= pokemon.ToxicCounter;
                pokemon.IncreaseToxicCounter();
            }

            yield return _battle.StartCoroutine(pokemon.RecieveDamge(damage));
            yield return _battle.StartCoroutine(OnReccurentPoison(pokemon));
        }

        if(pokemon.Seeded)
        {
            yield return _battle.StartCoroutine(pokemon.RecieveDamge(baseReccurentDamage));
            yield return _battle.StartCoroutine(opponent.RestoreHealth(baseReccurentDamage));
            yield return _battle.StartCoroutine(OnReccurentSap(pokemon));
        }

        yield return new WaitForSeconds(30 / 60f);
        yield return _battle.StartCoroutine(CheckForFaint());
    }

    private void OnClosedPokemonMenu()
    {
        _battle.BattleUI.SetActive(true);
        MessageBox.Open();
        MessageBox.Clear();

        PokemonMenu.ClosedFromBattle -= OnClosedPokemonMenu;
    }

    private int CalculateConfusionDamage(BattlePokemon user)
    {
        int userLevel = user.Level;
        int userAttack = user.BattleStats.Attack;
        int userDefense = user.BattleStats.Defense;

        return MoveData.DamageFormula(userLevel, userAttack, userDefense, power: 40, stab: 1, typeMultiplier: 1);
    }

    #region Messages

    private IEnumerator OnReccurentBurn(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.RECURRENT_BURN, bPokemon: pokemon));
    }

    private IEnumerator OnReccurentPoison(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.RECURRENT_POISON, bPokemon: pokemon));
    }

    private IEnumerator OnReccurentSap(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.RECURRENT_SAP, bPokemon: pokemon));
    }

    private IEnumerator OnHurtByConfusion(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display("", bPokemon: pokemon));
    }

    private IEnumerator OnSleeping(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_SLEEPING, bPokemon: pokemon));
    }

    private IEnumerator OnWokeUp(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_WOKE_UP, bPokemon: pokemon));
    }

    private IEnumerator OnStillFrozen(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_FROZEN, bPokemon: pokemon));
    }

    private IEnumerator OnFullyParalyzed(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_FULLY_PARALYZED, bPokemon: pokemon));
    }

    private IEnumerator OnMoveDisabled(BaseMove move)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.MOVE_DISABLED, move: move));
    }

    private IEnumerator OnNoLongerDisabled(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.USER_NO_LONGER_DISABLED, bPokemon: pokemon));
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

    #endregion
}