using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartState : BattleBaseState
{
    #region Battle State Callbacks

    public override void EnterState(BattleStateManager battle)
    {
        Movement.CanMove = false;
        
        battle.PlayerSide.SetTeam(battle.Player.Team);
        battle.PlayerSide.SetBag(battle.Player.Bag);
        BattleStateManager.StartBattle();

        MessageBox.Blink = false;
        battle.RunCounter = 0;
        battle.SuccessfulRun = false;
        battle.MoveSelection = 0;
        Vector3 pos = battle.Player.GetComponent<PlayerMovement>().TargetPosition;
        Vector2Int position = new Vector2Int((int)pos.x, (int)pos.y);
        battle.StartCoroutine(StartBattle(battle, position));
    }

    public override void UpdateState(BattleStateManager battle) { }

    public override void ExitState(BattleStateManager battle) { }

    public override void OnNavigate(BattleStateManager battle, InputAction.CallbackContext context) { }

    public override void OnConfirm(BattleStateManager battle, InputAction.CallbackContext context) { }

    public override void OnCancel(BattleStateManager battle, InputAction.CallbackContext context) { }

    #endregion

    private IEnumerator StartBattle(BattleStateManager battle, Vector2Int position)
    {
        // Black screen animation before entering the battle
        yield return battle.StartCoroutine(BattleTransition.StartBattleTransition(battle.TransitionID, battle.Tilemap, position, battle));
        
        // Show empty text box and blank battle screen
        // Player slides in from right to left while opponent slides in from left to right
        yield return battle.StartCoroutine(BattleStartTransition(battle));

        // Show Player team icons
        battle.SetIcons(battle.PlayerTeamIcons, battle.Player.Team);
        battle.PlayerInfoBackground.SetActive(true);
        battle.PlayerIconsFrame.SetActive(true);

        yield return null;

        battle.SetOpponentActivePokemon(battle.Opponent.GetFirstPokemon());
        string opponentName = battle.Opponent.Name;
        string text = string.Empty;

        if(battle.BattleType == BattleType.TRAINER_BATTLE)
        {
            battle.SetIcons(battle.OpponentTeamIcons, battle.Opponent.Team);
            //battle.OpponentIconsFrame.SetActive(true);

            yield return new WaitForSeconds(2 / 60f);

            text = $"{opponentName} wants\nto fight!";
            yield return battle.StartCoroutine(battle.DisplayMessage(text, true));

            yield return new WaitForSeconds(10 / 60f);
            battle.PlayerIconsFrame.SetActive(false);
            battle.OpponentIconsFrame.SetActive(false);
            MessageBox.Clear();

            // Slide Opponent Icon to the right
            yield return new WaitForSeconds(10 / 60f);

            text = $"{opponentName} sent\nout {battle.OpponentSide.ActivePokemon.Name}!";
            yield return battle.StartCoroutine(battle.DisplayMessage(text, false));

            yield return new WaitForSeconds(6 / 60f);
            // Pokemon sent out animation

            yield return new WaitForSeconds(6 / 60f);
        }
        else
        {
            text = $"{opponentName}\nappeared!";
            yield return battle.StartCoroutine(battle.DisplayMessage(text, true));

            yield return new WaitForSeconds(10 / 60f);
            battle.PlayerIconsFrame.SetActive(false);
            MessageBox.Clear();
        }

        // Display Opponent info
        battle.SetOpponentInfo(battle.OpponentSide.ActivePokemon);
        battle.OpponentInfoFrame.SetActive(true);

        yield return new WaitForSeconds(36 / 60f);

        RectTransform playerTransform = battle.PlayerImage.rectTransform;
        Vector3 playerPosition = playerTransform.localPosition;
        // Slide Player to the left
        for(int i = 0; i < 48; i++)
        {
            playerPosition.x -= 2;
            playerTransform.localPosition = playerPosition;
            yield return new WaitForSeconds(1 / 60f);
        }

        yield return battle.StartCoroutine(battle.SendOutPokemon(battle.Player.GetFirstPokemon()));

        // Progress to Input State
        battle.SwitchState(battle.InputState);
    }

    private IEnumerator BattleStartTransition(BattleStateManager battle)
    {
        battle.Background.SetActive(true);
        if (battle.BattleType == BattleType.OLD_MAN_BATTLE)
        {

        }
        else
            battle.PlayerImage.sprite = battle.Player.Icon;

        battle.PlayerImage.gameObject.SetActive(true);
        battle.OpponentImage.sprite = battle.Opponent.Icon;
        battle.OpponentImage.gameObject.SetActive(true);
        MessageBox.Open();

        yield return null;

        RectTransform playerTransform = battle.PlayerImage.rectTransform;
        Vector3 playerPosition = new Vector3(182, -104);
        Vector2 playerSize = new Vector2(64, 64);

        playerTransform.sizeDelta = playerSize;
        playerTransform.localPosition = playerPosition;

        RectTransform opponentTransform = battle.OpponentImage.rectTransform;
        Vector3 opponentPosition = new Vector3(-74, -56);
        int opponentTargetX = 68;

        // Offset for flipped sprite for wild Pokemon
        if (battle.BattleType == BattleType.WILD_BATTLE)
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
}