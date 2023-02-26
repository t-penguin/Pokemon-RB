using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PokedexOptionsMenu))]
public class PokedexDataMenu : Menu
{
    [SerializeField] GameObject _dataScreen;
    [SerializeField] Image _dataSprite;
    [SerializeField] TextMeshProUGUI _dataText;
    [SerializeField] TextMeshProUGUI _dataNumber;
    [SerializeField] TextMeshProUGUI _dataDescription;
    [SerializeField] GameObject _descriptionArrow;

    private PokedexOptionsMenu _optionsMenu;
    private PokedexMenu _pokedexMenu;
    private bool _blinkDescriptionArrow;
    private int _pokedexNumber;
    private bool _exitData;

    #region Input Callbacks

    protected override void OnNavigate(InputAction.CallbackContext context) { }

    protected override void OnConfirm(InputAction.CallbackContext context) => OnConfirmOrCancel();

    protected override void OnCancel(InputAction.CallbackContext context) => OnConfirmOrCancel();

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
        _optionsMenu = GetComponent<PokedexOptionsMenu>();
        _pokedexMenu = GetComponent<PokedexMenu>();
    }

    private void Update()
    {
        // Flash the data description arrow
        if (!_exitData && _blinkDescriptionArrow)
            BlinkObject(_descriptionArrow, 34 / 60f);
    }

    public IEnumerator Open(int pokedexNumber)
    {
        _pokedexNumber = pokedexNumber;
        _pokedexMenu.PokedexScreen.SetActive(false);

        yield return new WaitForSeconds(6 / 60f);

        _dataScreen.SetActive(true);

        // Display available information
        _dataSprite.sprite = PokemonData.Sprites[0][2];
        _dataNumber.text = _pokedexNumber.ToString("D3");
        _dataText.text = $"{PokemonData.Names[_pokedexNumber]}\n" +
                        $"{PokemonData.Categories[_pokedexNumber]}\n" +
                        $"HT  ? ??\nWT   ???lb";
        _dataDescription.text = "";
        _descriptionArrow.SetActive(false);

        // Delay sprite loading
        yield return new WaitForSeconds(32 / 60f);
        _dataSprite.sprite = PokemonData.Sprites[_pokedexNumber][2];

        // Display unknown data after delay if the Pokemon has been caught
        if (PlayerData.PokemonCaught[_pokedexNumber])
        {
            yield return new WaitForSeconds(48 / 60f);
            int heightInInches = PokemonData.Heights[_pokedexNumber];
            _dataText.text = $"{PokemonData.Names[_pokedexNumber]}\n" +
                            $"{PokemonData.Categories[_pokedexNumber]}\n" +
                            $"HT {heightInInches / 12,2} {heightInInches % 12:D2}\n" +
                            $"WT{PokemonData.Weights[_pokedexNumber],6:0.0}lb";
            _dataDescription.text = PokemonData.Descriptions[_pokedexNumber][0];
            _descriptionArrow.SetActive(true);
            _blinkDelay = 0.64f;
            _blinkDescriptionArrow = true;
        }
        else _blinkDescriptionArrow = false;

        _exitData = !PlayerData.PokemonCaught[_pokedexNumber];
        ListenForInput();
    }

    public IEnumerator Close()
    {
        StopListeningForInput();
        _dataScreen.SetActive(false);

        yield return new WaitForSeconds(18 / 60f);

        _optionsMenu.Close();
        _pokedexMenu.PokedexScreen.SetActive(true);
    }

    public IEnumerator Advance()
    {
        StopListeningForInput();
        _dataDescription.text = "";
        _exitData = true;
        _descriptionArrow.SetActive(false);

        yield return new WaitForSeconds(18 / 60f);

        _dataDescription.text = PokemonData.Descriptions[_pokedexNumber][1];
        ListenForInput();
    }

    private void OnConfirmOrCancel()
    {
        if (_exitData)
            StartCoroutine(Close());
        else
            StartCoroutine(Advance());
    }
}