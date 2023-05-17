using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PokemonMenu))]
[RequireComponent(typeof(PokemonStatsMenu))]
public class PokemonOptionsMenu : Menu
{
    [SerializeField] GameObject _optionsBox;
    [SerializeField] RectTransform _arrow;
    [SerializeField] TextMeshProUGUI[] _optionsText = new TextMeshProUGUI[6];

    private PokemonMenu _pokemonMenu;
    private PokemonStatsMenu _statsMenu;

    [SerializeField] private int _currentOption;
    private int _numOptions;
    private List<int> _hmIndexes;
    private Pokemon _pokemon;
    private Direction _previousNav;

    private const string SWITCH = "SWITCH";
    private const string STATS = "STATS";
    private const string NO_WILL_TO_FIGHT = "There< no will\nto fight!";

    #region Input Callbacks

    protected override void OnNavigate(InputAction.CallbackContext context) => Navigate(context.ReadValue<Vector2>());

    protected override void OnConfirm(InputAction.CallbackContext context) => SelectOption(_currentOption);

    protected override void OnCancel(InputAction.CallbackContext context)
    {
        HideOptions();
        StartCoroutine(_pokemonMenu.Refocus());
    }

    #endregion

    protected override void Awake()
    {
        _hmIndexes = new List<int>();
        _pokemonMenu = GetComponent<PokemonMenu>();
        _statsMenu = GetComponent<PokemonStatsMenu>();
    }

    public void Open(Pokemon pokemon)
    {
        _pokemon = pokemon;

        if (_pokemonMenu.InBattle)
        {
            _hmIndexes.Clear();
            _numOptions = 3;
            _optionsText[4].text = SWITCH;
            _optionsText[5].text = STATS;
        }
        else
        {
            _hmIndexes = pokemon.GetHMsKnown();
            _numOptions = 3 + _hmIndexes.Count;
            _optionsText[4].text = STATS;
            _optionsText[5].text = SWITCH;
        }

        Vector2 optionsSize = new Vector2(72, 56);
        if (_numOptions > 3)
            optionsSize.y = (_numOptions + 1) * 16;

        _optionsBox.GetComponent<RectTransform>().sizeDelta = optionsSize;

        for (int i = 3; i >= 0; i--)
            _optionsText[i].text = 3 - i >= _hmIndexes.Count ? string.Empty : MoveData.Names[_hmIndexes[3 - i]];

        // Set the current option to the top one and set the arrow position
        _currentOption = 7 - _numOptions;
        _arrow.anchoredPosition = new Vector2(8f, (7 - _currentOption) * 16f);
        _optionsBox.SetActive(true);
        ListenForInput();
    }

    private void Navigate(Vector2 input)
    {
        if(input.y == 0)
        {
            _previousNav = Direction.None;
            return;
        }

        Direction direction;
        if (input.y > 0)
            direction = Direction.Up;
        else
            direction = Direction.Down;

        if (direction == _previousNav)
            return;

        int topOption = 7 - _numOptions;
        if (direction == Direction.Up)
            _currentOption = _currentOption > topOption ? _currentOption - 1 : topOption;
        else
            _currentOption = _currentOption < 6 ? _currentOption + 1 : 6;

        _previousNav = direction;
        _arrow.anchoredPosition = new Vector2(8f, 16f * (7 - _currentOption));
    }

    private void SelectOption(int option)
    {
        switch(option)
        {
            default:
            case 6:
                HideOptions();
                _pokemonMenu.Quit();
                break;
            // STATS in battle - SWITCH out of battle
            case 5:
                HideOptions();
                if (_pokemonMenu.InBattle)
                    StartCoroutine(_statsMenu.Open(_pokemon));
                else
                    StartCoroutine(_pokemonMenu.AskForSwitch());
                break;
            // SWITCH in battle - STATS out of battle
            case 4:
                if (_pokemonMenu.InBattle)
                    StartCoroutine(AttemptSwap());
                else
                {
                    HideOptions();
                    StartCoroutine(_statsMenu.Open(_pokemon));
                }
                break;
            case 3:
            case 2:
            case 1:
            case 0:
                Debug.Log("HM use not yet added!");
                break;

        }
    }

    private void HideOptions()
    {
        _optionsBox.SetActive(false);
        StopListeningForInput();
    }

    private IEnumerator AttemptSwap()
    {
        bool ableToFight = _pokemon.CurrentHP > 0;
        bool alreadyOut = _pokemon == _pokemonMenu.battle.PlayerSide.ActivePokemon.ReferencePokemon;

        if(ableToFight && !alreadyOut)
        {
            HideOptions();
            StartCoroutine(_pokemonMenu.SwapPokemon(false));
            yield break;
        }

        string text = "";
        if (alreadyOut)
            text = BattleMessages.ALREADY_OUT;
        if (!ableToFight)
            text = BattleMessages.NO_WILL_TO_FIGHT;

        StopListeningForInput();

        MessageBox.BringToFront();
        yield return StartCoroutine(BattleMessages.Display(text));
        MessageBox.ResetSortOrder();

        HideOptions();
        StartCoroutine(_pokemonMenu.Refocus());
    }
}