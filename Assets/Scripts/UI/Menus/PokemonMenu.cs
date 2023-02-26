using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PokemonOptionsMenu))]
public class PokemonMenu : Menu
{
    private const int MENU_ID = 2;
    private const string CHOOSE_A_POKEMON = "Choose a POKÈMON.";
    private const string MOVE_POKEMON_WHERE = "Move POKÈMON\nwhere?";
    private const string BRING_OUT_POKEMON = "Bring out which\nPOKÈMON?";
    private const string NO_WILL_TO_FIGHT = "There< no will\nto fight!";

    [SerializeField] RectTransform _arrow;
    [SerializeField] RectTransform _secondArrow;
    [SerializeField] GameObject _background;
    [SerializeField] GameObject[] _pokemonInfo = new GameObject[6];
    [SerializeField] Image[] _icons = new Image[6];
    [SerializeField] RectTransform[] _healthBars = new RectTransform[6];
    [SerializeField] TextMeshProUGUI[] _names = new TextMeshProUGUI[6];
    [SerializeField] TextMeshProUGUI[] _levels = new TextMeshProUGUI[6];
    [SerializeField] TextMeshProUGUI[] _statuses = new TextMeshProUGUI[6];
    [SerializeField] TextMeshProUGUI[] _healthText = new TextMeshProUGUI[6];

    private PokemonOptionsMenu _optionsMenu;
    public BattleStateManager battle;

    private int _currentSelection;
    private int _switchSelection;
    private int _teamSize;
    private float _iconDelay;
    private Sprite _icon1;
    private Sprite _icon2;
    private Player _player;
    private Direction _previousNav;
    private bool _switching;
    private bool _animateIcons;

    private string _messageText;

    public bool InBattle { get; private set; }

    #region Events

    public static event Action<int> Opened;
    public static event Action Closed;
    public static event Action<BattleStateManager> ClosedFromBattle;

    #endregion

    #region Input Callbacks

    protected override void OnNavigate(InputAction.CallbackContext context) => Navigate(context.ReadValue<Vector2>());

    protected override void OnConfirm(InputAction.CallbackContext context)
    {
        if(InBattle && battle.ForcedSwap)
        {
            StartCoroutine(SwapPokemon(true));
            return;
        }

        if (_switching)
            StartCoroutine(SwitchTeamOrder());
        else
        {
            _secondArrow.localPosition = _arrow.localPosition;
            _secondArrow.gameObject.SetActive(true);
            _arrow.gameObject.SetActive(false);
            _optionsMenu.Open(_player.Team[_currentSelection]);
            _animateIcons = false;
            StopListeningForInput();
        }
    }

    protected override void OnCancel(InputAction.CallbackContext context)
    {
        if(InBattle)
        {
            StartCoroutine(CloseFromBattle());
            return;
        }

        if (_switching)
            CancelSwitch();
        else
            StartCoroutine(Close());
    }

    #endregion

    private void OnEnable()
    {
        MainMenu.Opened += ListenFromMain;
        MainMenu.Closed += StopListeningFromMain;
        BattleStateManager.BattleStarted += ListenFromBattle;
        BattleStateManager.BattleEnded += StopListeningFromBattle;
    }

    private void OnDisable()
    {
        MainMenu.Opened -= ListenFromMain;
        MainMenu.Closed -= StopListeningFromMain;
        BattleStateManager.BattleStarted -= ListenFromBattle;
        BattleStateManager.BattleEnded -= StopListeningFromBattle;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _optionsMenu = GetComponent<PokemonOptionsMenu>();
        _currentSelection = 0;
    }

    private void Update()
    {
        if(_animateIcons)
            AnimateIcons();
    }

    public void Quit() => StartCoroutine(Close());

    public IEnumerator SwapPokemon(bool forced)
    {
        if(!forced)
        {
            battle.Swap = true;
            yield return StartCoroutine(CloseFromBattle());
            BattleStateManager.SwapPokemon(battle, _player.Team[_currentSelection]);
            yield break;
        }

        Pokemon pokemon = _player.Team[_currentSelection];
        bool ableToFight = pokemon.CurrentHP > 0;

        if (ableToFight)
        {
            yield return StartCoroutine(CloseFromBattle());
            yield return StartCoroutine(battle.SendOutPokemon(pokemon));
            yield break;
        }

        StopListeningForInput();

        _secondArrow.localPosition = _arrow.localPosition;
        _arrow.gameObject.SetActive(false);
        _secondArrow.gameObject.SetActive(true);
        _animateIcons = false;
        yield return StartCoroutine(battle.DisplayMessage(NO_WILL_TO_FIGHT, true));

        yield return StartCoroutine(Refocus());
    }

    public IEnumerator Refocus()
    {
        _secondArrow.gameObject.SetActive(false);
        ShowTeam();
        MessageBox.Close();

        yield return new WaitForSeconds(6 / 60f);

        _arrow.gameObject.SetActive(true);
        _animateIcons = true;
        MessageBox.Open();
        if (battle.ForcedSwap)
            MessageBox.Set(BRING_OUT_POKEMON);
        else
            MessageBox.Set(CHOOSE_A_POKEMON);

        ListenForInput();
    }

    public IEnumerator AskForSwitch()
    {
        yield return new WaitForSeconds(4 / 60);
        MessageBox.Set(MOVE_POKEMON_WHERE);
        _switchSelection = _currentSelection;
        _arrow.gameObject.SetActive(true);
        _switching = true;
        ListenForInput();
    }

    public void HideTeam()
    {
        foreach (GameObject o in _pokemonInfo)
            o.SetActive(false);

        _arrow.gameObject.SetActive(false);
        _secondArrow.gameObject.SetActive(false);
    }

    private void OpenFromMain(int menuID)
    {
        // Exit if the IDs don't match
        if (menuID != MENU_ID) return;

        // Open the Pokemon Menu
        InBattle = false;
        _messageText = CHOOSE_A_POKEMON;
        StartCoroutine(Open());
    }

    private void OpenFromBattle(BattleStateManager battle)
    {
        InBattle = true;
        this.battle = battle;
        _messageText = battle.ForcedSwap ? BRING_OUT_POKEMON : CHOOSE_A_POKEMON;
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        Opened?.Invoke(MENU_ID);
        Debug.Log("Opening the Pokemon Menu");

        HideTeam();
        _background.SetActive(true);
        _teamSize = _player.Team.Count;
        SetTeam();

        yield return new WaitForSeconds(16 / 60f);
        ShowTeam();
        _animateIcons = true;
        MessageBox.Open();
        MessageBox.Set(CHOOSE_A_POKEMON);
        ListenForInput();
    }

    private IEnumerator Close()
    {
        StopListeningForInput();
        HideTeam();
        _animateIcons = false;
        _arrow.gameObject.SetActive(false);
        MessageBox.Close();
        MessageBox.Clear();

        yield return new WaitForSeconds(30 / 60f);

        _background.SetActive(false);
        Closed?.Invoke();
    }

    private IEnumerator CloseFromBattle()
    {
        yield return StartCoroutine(Close());
        Debug.Log("Closed the pokemon menu from battle");
        ClosedFromBattle?.Invoke(battle);
    }

    private void Navigate(Vector2 input)
    {
        if (input.y == 0)
        {
            _previousNav = Direction.None;
            return;
        }

        Direction direction;
        if (input.y > 0) direction = Direction.Up;
        else direction = Direction.Down;

        if (direction == _previousNav)
            return;

        // Set the icon before moving to the next selection
        _icons[_currentSelection].sprite = _icon1;

        // Going down the menu, increase current selection by 1 or loop to top
        if (direction == Direction.Down)
            _currentSelection = _currentSelection >= _teamSize - 1 ? 0 : _currentSelection + 1;
        // Going up the menu, decrease the current selection by 1 or loop to bottom
        else if (direction == Direction.Up)
            _currentSelection = _currentSelection <= 0 ? _teamSize - 1 : _currentSelection - 1;

        // Set the position of the menu arrow based on the current selection
        _arrow.anchoredPosition = new Vector2(0f, -8f - 16f * _currentSelection);

        // Set icon1 and icon2 to the new selections icons
        _icon1 = _icons[_currentSelection].sprite;
        _icon2 = PokemonData.Sprites[_player.Team[_currentSelection].PokedexNumber][1];

        _previousNav = direction;
    }

    private void ShowTeam()
    {
        for (int i = 0; i < _teamSize; i++)
            _pokemonInfo[i].SetActive(true);

        _arrow.gameObject.SetActive(true);
    }

    private void SetTeam()
    {
        // Loop through each Pokemon in the player's team
        for (int i = 0; i < _teamSize; i++)
        {
            Pokemon p = _player.Team[i];

            // Set each part of the Pokemon's information
            _icons[i].sprite = PokemonData.Sprites[p.PokedexNumber][0];
            _healthBars[i].localScale = new Vector3((float)p.CurrentHP / p.Stats.HP, 1f, 1f);
            _names[i].text = p.Nickname;
            _levels[i].text = $"@{p.Level}";
            _statuses[i].text = p.Status == StatusEffect.OK ? "" : p.Status.ToString();
            _healthText[i].text = $"{p.CurrentHP,3}/{p.Stats.HP,3}";
        }

        // Set references to the currently selected Pokemon's mini sprites for animation
        _icon1 = _icons[_currentSelection].sprite;
        _icon2 = PokemonData.Sprites[_player.Team[_currentSelection].PokedexNumber][1];
    }

    private IEnumerator SwitchTeamOrder()
    {
        StopListeningForInput();
        _switching = false;

        // Disable the first selection and move the empty arrow
        _pokemonInfo[_switchSelection].SetActive(false);
        _secondArrow.localPosition = _arrow.localPosition;
        _arrow.gameObject.SetActive(false);

        // Delay, then disable the second selection
        yield return new WaitForSeconds(20 / 60f);
        _pokemonInfo[_currentSelection].SetActive(false);
        _secondArrow.gameObject.SetActive(false);

        // Swap Pokemon in the player's team and reset the Pokemon Info
        _player.SwitchPokemon(_switchSelection, _currentSelection);
        SetTeam();

        // Delay, then Re-enable both selections
        yield return new WaitForSeconds(10 / 60f);
        _pokemonInfo[_switchSelection].SetActive(true);
        _pokemonInfo[_currentSelection].SetActive(true);
        _arrow.gameObject.SetActive(true);

        // Revert Text
        MessageBox.Set(CHOOSE_A_POKEMON);

        ListenForInput();
    }

    private void CancelSwitch()
    {
        _secondArrow.gameObject.SetActive(false);
        MessageBox.Set(CHOOSE_A_POKEMON);
    }

    private void AnimateIcons()
    {
        if (_iconDelay > 0) _iconDelay -= Time.deltaTime;

        // Animate the Pokemon icon
        if (_iconDelay <= 0)
        {
            _icons[_currentSelection].sprite = _icons[_currentSelection].sprite == _icon1 ? _icon2 : _icon1;
            _iconDelay = 7 / 60f;
        }
    }

    private void ListenFromMain()
    {
        MainMenu.OpenedSubMenu += OpenFromMain;
        BattleStateManager.BattleStarted -= ListenFromBattle;
    }

    private void ListenFromBattle()
    {
        _currentSelection = 0;
        BattleStateManager.OpenedPokemon += OpenFromBattle;
        MainMenu.Opened -= ListenFromMain;
    }

    private void StopListeningFromMain() => MainMenu.OpenedSubMenu -= OpenFromMain;
    private void StopListeningFromBattle()
    {
        _currentSelection = 0;
        BattleStateManager.OpenedPokemon -= OpenFromBattle;
        MainMenu.Opened += ListenFromMain;
    }
}