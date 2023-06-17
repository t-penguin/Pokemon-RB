using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartState : BattleBaseState
{
    #region Battle State Callbacks

    public override void EnterState(BattleStateManager battle)
    {
        if (_battle == null)
            _battle = battle;

        Movement.CanMove = false;
        
        _battle.PlayerSide.SetTeam(_battle.Player.Team);
        _battle.PlayerSide.SetBag(_battle.Player.Bag);
        BattleStateManager.StartBattle();

        MessageBox.Blink = false;
        _battle.RunCounter = 0;
        _battle.SuccessfulRun = false;
        _battle.MoveSelection = 0;
        Vector3 pos = _battle.Player.GetComponent<PlayerMovement>().TargetPosition;
        Vector2Int position = new Vector2Int((int)pos.x, (int)pos.y);
        _battle.StartCoroutine(StartBattle(position));
    }

    public override void ExitState() { }

    #endregion

    private IEnumerator StartBattle(Vector2Int position)
    {
        // Black screen animation before entering the battle
        yield return _battle.StartCoroutine(BattleTransition.StartBattleTransition
            (_battle.TransitionID, _battle.Tilemap, position, _battle));
        
        // Show empty text box and blank battle screen
        // Player slides in from right to left while opponent slides in from left to right
        yield return _battle.StartCoroutine(BattleStartTransition());

        // Show Player team icons
        _battle.SetIcons(_battle.PlayerTeamIcons, _battle.Player.Team);
        _battle.PlayerInfoBackground.SetActive(true);
        _battle.PlayerIconsFrame.SetActive(true);

        yield return null;

        Trainer opponent = _battle.Opponent;
        _battle.SetOpponentActivePokemon(opponent.GetFirstPokemon());
        BattlePokemon opponentPokemon = _battle.OpponentSide.ActivePokemon;

        if(_battle.BattleType == BattleType.TRAINER_BATTLE)
        {
            _battle.SetIcons(_battle.OpponentTeamIcons, opponent.Team);
            //battle.OpponentIconsFrame.SetActive(true);

            yield return new WaitForSeconds(2 / 60f);

            yield return _battle.StartCoroutine(OnTrainerBattle(opponent));

            _battle.PlayerIconsFrame.SetActive(false);
            _battle.OpponentIconsFrame.SetActive(false);
            MessageBox.Clear();

            // Slide Opponent Icon to the right
            yield return new WaitForSeconds(10 / 60f);

            yield return _battle.StartCoroutine(OnTrainerSentOutPokemon(opponent, opponentPokemon));

            yield return new WaitForSeconds(6 / 60f);
            // Pokemon sent out animation

            yield return new WaitForSeconds(6 / 60f);
        }
        else
        {
            yield return _battle.StartCoroutine(OnWildBattle(opponentPokemon));

            yield return new WaitForSeconds(10 / 60f);
            _battle.PlayerIconsFrame.SetActive(false);
            MessageBox.Clear();
        }

        // Display Opponent info
        _battle.SetOpponentInfo(_battle.OpponentSide.ActivePokemon);
        _battle.OpponentInfoFrame.SetActive(true);

        yield return new WaitForSeconds(36 / 60f);

        RectTransform playerTransform = _battle.PlayerImage.rectTransform;
        Vector3 playerPosition = playerTransform.localPosition;
        // Slide Player to the left
        for(int i = 0; i < 48; i++)
        {
            playerPosition.x -= 2;
            playerTransform.localPosition = playerPosition;
            yield return new WaitForSeconds(1 / 60f);
        }

        yield return _battle.StartCoroutine(_battle.SendOutPokemon(_battle.Player.GetFirstPokemon()));

        // Progress to Input State
        _battle.SwitchState(_battle.InputState);
    }

    private IEnumerator BattleStartTransition()
    {
        _battle.Background.SetActive(true);
        if (_battle.BattleType == BattleType.OLD_MAN_BATTLE)
        {

        }
        else
            _battle.PlayerImage.sprite = _battle.Player.Icon;

        _battle.PlayerImage.gameObject.SetActive(true);
        _battle.OpponentImage.sprite = _battle.Opponent.Icon;
        _battle.OpponentImage.gameObject.SetActive(true);
        MessageBox.Open();

        yield return null;

        RectTransform playerTransform = _battle.PlayerImage.rectTransform;
        Vector3 playerPosition = new Vector3(182, -104);
        Vector2 playerSize = new Vector2(64, 64);

        playerTransform.sizeDelta = playerSize;
        playerTransform.localPosition = playerPosition;

        RectTransform opponentTransform = _battle.OpponentImage.rectTransform;
        Vector3 opponentPosition = new Vector3(-74, -56);
        int opponentTargetX = 68;

        // Offset for flipped sprite for wild Pokemon
        if (_battle.BattleType == BattleType.WILD_BATTLE)
        {
            opponentPosition.x += 56;
            opponentTargetX += 56;
        }

        // Slide in player and opponent
        for(int i = 0; i < 72; i++)
        {
            playerPosition.x = playerPosition.x <= 42 ? 40 : playerPosition.x - 2;
            opponentPosition.x = opponentPosition.x >= opponentTargetX - 2 ? opponentTargetX : opponentPosition.x + 2;

            playerTransform.localPosition = playerPosition;
            opponentTransform.localPosition = opponentPosition;

            yield return new WaitForSeconds(1 / 60f);
        }

        yield return new WaitForSeconds(30 / 60f);
    }

    private IEnumerator OnTrainerBattle(Trainer trainer)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.BATTLE_START_TRAINER, trainer: trainer));
    }

    private IEnumerator OnTrainerSentOutPokemon(Trainer trainer, BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(
            BattleMessages.TRAINER_SENT_OUT_POKEMON, trainer: trainer, bPokemon: pokemon, replaceName: false, waitForInput: false));
    }

    private IEnumerator OnWildBattle(BattlePokemon pokemon)
    {
        yield return _battle.StartCoroutine(BattleMessages.Display(
            BattleMessages.BATTLE_START_WILD, bPokemon: pokemon, replaceName: false));
    }
}