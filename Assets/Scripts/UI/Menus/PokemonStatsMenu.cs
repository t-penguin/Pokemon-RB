using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PokemonMenu))]
public class PokemonStatsMenu : Menu
{
    [SerializeField] GameObject _screen1;
    [SerializeField] Image _pokemonSprite;
    [SerializeField] TextMeshProUGUI _pokedexNumber;
    [SerializeField] TextMeshProUGUI _mainInfo;
    [SerializeField] RectTransform _healthBar;
    [SerializeField] TextMeshProUGUI _typeInfo;
    [SerializeField] TextMeshProUGUI _statsInfo;
    [SerializeField] GameObject _screen2;
    [SerializeField] TextMeshProUGUI _expInfo;
    [SerializeField] TextMeshProUGUI _moveInfo;

    private PokemonMenu _pokemonMenu;
    private Pokemon _pokemon;

    #region Input Callbacks

    protected override void OnNavigate(InputAction.CallbackContext context) { }

    protected override void OnConfirm(InputAction.CallbackContext context) => StartCoroutine(AdvanceOrCloseStats());

    protected override void OnCancel(InputAction.CallbackContext context) => StartCoroutine(AdvanceOrCloseStats());

    #endregion

    protected override void ListenForInput()
    {
        TestInputManager.ConfirmAction.started += OnConfirm;
        TestInputManager.CancelAction.started += OnCancel;
    }

    protected override void StopListeningForInput()
    {
        TestInputManager.ConfirmAction.started -= OnConfirm;
        TestInputManager.CancelAction.started -= OnCancel;
    }

    protected override void Awake()
    {
        _pokemonMenu = GetComponent<PokemonMenu>();
    }

    public IEnumerator Open(Pokemon pokemon)
    {
        // Hide the previous screen and set the stat screen info
        _pokemonMenu.HideTeam();
        MessageBox.Close();
        _pokemon = pokemon;
        SetStatsScreen();

        // Display the first screen after a delay
        yield return new WaitForSeconds(16 / 60f);
        _screen1.SetActive(true);
        _pokedexNumber.gameObject.SetActive(true);

        // Display the selected Pokemon's sprite after a delay
        yield return new WaitForSeconds(48 / 60f);
        _pokemonSprite.gameObject.SetActive(true);

        // Allow input after a short delay
        yield return new WaitForSeconds(30 / 60f);

        ListenForInput();
    }

    private void SetStatsScreen()
    {
        _pokemonSprite.sprite = PokemonData.Sprites[_pokemon.PokedexNumber][2];
        _pokedexNumber.text = $"Ń {_pokemon.PokedexNumber:D3}";
        // Main Info in the top right of the first screen
        _mainInfo.text = $"{_pokemon.Nickname}\n     @{_pokemon.Level}\n\n" +
                        $"   {_pokemon.CurrentHP,3}/{_pokemon.Stats.HP,3}\n\n" +
                        $"STATUS/{_pokemon.Status}";
        _healthBar.localScale = new Vector3((float)_pokemon.CurrentHP / _pokemon.Stats.HP, 1, 1);
        // Exp Info in the top right of the second screen
        string nextLevelText = "";
        if (_pokemon.Level < 99)
            nextLevelText = $"LEVEL UP\n{_pokemon.ExpToNextLevel(),5}~ @{_pokemon.Level + 1}";
        else if (_pokemon.Level == 99)
            nextLevelText = $"LEVEL UP\n{_pokemon.ExpToNextLevel(),5}~@{_pokemon.Level + 1}";
        _expInfo.text = $"{PokemonData.Names[_pokemon.PokedexNumber]}\n\n" +
                        $"EXP POINTS\n{_pokemon.TotalExperience,10}\n" +
                        $"{nextLevelText}";
        // Stat Info in the box in the bottom left of the first screen
        _statsInfo.text = $"ATTACK\n{_pokemon.Stats.Attack,8}\n" +
                            $"DEFENSE\n{_pokemon.Stats.Defense,8}\n" +
                            $"SPEED\n{_pokemon.Stats.Speed,8}\n" +
                            $"SPECIAL\n{_pokemon.Stats.Special,8}";
        // Type Info in the bottom right of the first screen
        string type2Text = _pokemon.SecondaryType == Type.NONE ? "\n" : $"TYPE2/\n {_pokemon.SecondaryType}";
        _typeInfo.text = $"TYPE1/\n {_pokemon.PrimaryType}\n" +
                        $"{type2Text}\nĪŃ/\n  {_pokemon.TrainerID:D5}\n" +
                        $"OT/\n  {_pokemon.OriginalTrainer}";
        // Move Info in the bottom box of the second screen
        string moveText = "";
        for (int i = 0; i < 4; i++)
        {
            if (_pokemon.MoveIndexes[i] == 0)
                moveText += "-\n         --\n";
            else
                moveText += $"{MoveData.Names[_pokemon.MoveIndexes[i]]}\n" +
                            $"         PP {_pokemon.MovePPs[i],2}/{_pokemon.MoveMaxPPs[i],2}\n";
        }
        _moveInfo.text = moveText;
    }

    private IEnumerator AdvanceOrCloseStats()
    {
        StopListeningForInput();

        // Advance Stats
        if(_screen1.activeSelf)
        {
            _screen1.SetActive(false);
            _screen2.SetActive(true);
            yield return new WaitForSeconds(4 / 60f);
            ListenForInput();
            yield break;
        }

        // Close Stats
        _pokedexNumber.gameObject.SetActive(false);
        _screen1.SetActive(false);
        _screen2.SetActive(false);
        _pokemonSprite.gameObject.SetActive(false);

        yield return new WaitForSeconds(20 / 60f);

        StartCoroutine(_pokemonMenu.Refocus());
    }
}