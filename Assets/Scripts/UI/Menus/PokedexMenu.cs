using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(PokedexOptionsMenu))]
public class PokedexMenu : Menu
{
    private const int MENU_ID = 1;
    private const string UNSEEN_TEXT = "----------";

    [field: SerializeField] public GameObject PokedexScreen { get; private set; }
    [SerializeField] GameObject _blankScreen;
    [SerializeField] RectTransform _arrow;
    [SerializeField] TextMeshProUGUI _registeredText;
    [SerializeField] TextMeshProUGUI[] _dexNumbers = new TextMeshProUGUI[7];
    [SerializeField] TextMeshProUGUI[] _speciesNames = new TextMeshProUGUI[7];
    [SerializeField] GameObject[] _caughtIcons = new GameObject[7];

    private PokedexOptionsMenu _optionsMenu;

    private int _index;
    private int _currentDexNum;
    private int _topDexNum;
    private int _lastSeen;

    private bool _initialPress;
    private Direction _navDirection;
    private float _navDelay;

    #region Events

    public static event Action<int> OpenedPokedex;
    public static event Action ClosedPokedex;

    #endregion

    #region Input Callbacks

    protected override void OnNavigate(InputAction.CallbackContext context) => SetNavigation(context.ReadValue<Vector2>());
    protected override void OnConfirm(InputAction.CallbackContext context) => OpenOptions(_currentDexNum);
    protected override void OnCancel(InputAction.CallbackContext context) => StartCoroutine(Close());

    #endregion

    #region Monobehaviour Callbacks

    protected override void Awake()
    {
        base.Awake();
        _optionsMenu = GetComponent<PokedexOptionsMenu>();
    }

    private void Update()
    {
        HandleNavigation();
    }

    // Subscribe to some events on enable
    private void OnEnable()
    {
        MainMenu.Opened += ListenForOpen;
        MainMenu.Closed += StopListeningForOpen;
    }

    // Unsubscribe from ALL events on disable
    private void OnDisable()
    {
        MainMenu.Opened -= ListenForOpen;
        MainMenu.Closed -= StopListeningForOpen;
        MainMenu.OpenedSubMenu -= OpenPokedex;
        StopListeningForInput();
    }

    #endregion

    public void OpenPokedex(int menuID)
    {
        // Exit if the IDs don't match
        if (menuID != MENU_ID) return;

        // Open the Pokedex
        StartCoroutine(Open());
    }

    public void ResumeFocus()
    {
        _arrow.GetComponent<Image>().sprite = _filledArrow;
        ListenForInput();
    }

    private void ListenForOpen() => MainMenu.OpenedSubMenu += OpenPokedex;
    private void StopListeningForOpen() => MainMenu.OpenedSubMenu -= OpenPokedex;

    private IEnumerator Open()
    {
        OpenedPokedex?.Invoke(MENU_ID);
        Debug.Log("Opening the Pokedex");

        // Display a blank screen for a few frames
        _blankScreen.SetActive(true);

        // (Re)set initial values
        _index = _topDexNum = _currentDexNum = 1;
        _arrow.anchoredPosition = new Vector2(0, -24f);
        _arrow.GetComponent<Image>().sprite = _filledArrow;

        // Set data based on the corresponding values stored in Player Data
        _lastSeen = PlayerData.GetLastSeen();
        _registeredText.text = $"SEEN\n{PlayerData.NumPokemonSeen,3}\n\nOWN\n{PlayerData.NumPokemonCaught,3}";
        SetPokedexInfo();

        // Delay the display of information
        yield return new WaitForSeconds(10 / 60f);
        PokedexScreen.SetActive(true);

        // Subscribe to user input on open
        ListenForInput();
    }

    // Close the Pokedex
    public IEnumerator Close()
    {
        StopListeningForInput();
        PokedexScreen.SetActive(false);

        yield return new WaitForSeconds(10 / 60f);

        _blankScreen.SetActive(false);
        // Let any subscribers know that the Pokedex has been closed
        ClosedPokedex?.Invoke();
    }

    // Sets the information seen on the Pokedex screen
    private void SetPokedexInfo()
    {
        for (int i = 0; i < 7; i++)
        {
            int currentDexNumber = _topDexNum + i;
            _dexNumbers[i].text = (currentDexNumber).ToString("D3");
            _caughtIcons[i].SetActive(PlayerData.PokemonCaught[currentDexNumber]);
            _speciesNames[i].text = PlayerData.PokemonSeen[currentDexNumber] ? PokemonData.Names[currentDexNumber] : UNSEEN_TEXT;
        }
    }

    // Sets navigation values based on input
    private void SetNavigation(Vector2 input)
    {
        _navDirection = Direction.None;
        _initialPress = true;

        // This menu's navigation is affected by both horizontal and vertical input
        if (input == Vector2.zero) _navDelay = 0f;
        else if (input.y == 1) _navDirection = Direction.Up;
        else if (input.y == -1) _navDirection = Direction.Down;
        else if (input.x == 1) _navDirection = Direction.Right;
        else if (input.x == -1) _navDirection = Direction.Left;
        // Case where there is both horizontal and vertical input
        else
        {
            // Vertical input takes priority, so we only care about that
            if (input.y > 0) _navDirection = Direction.Up;
            else if (input.y < 0) _navDirection = Direction.Down;
        }
    }

    private void HandleNavigation()
    {
        // Decrease navigation delay timer if it's greater than 0
        if (_navDelay > 0)
        {
            _navDelay -= Time.deltaTime;
            return;
        }

        if (_navDirection == Direction.None)
            return;

        switch (_navDirection)
        {
            /* Move up the Pokedex by one
                * Top number only changes if we're at the top of the list */
            case Direction.Up:
                _topDexNum = (_topDexNum > 1 && _index == 1) ? _topDexNum - 1 : _topDexNum;
                _index = _index > 1 ? _index - 1 : _index;
                break;
            /* Move down the Pokedex by one
                * Top number only changes if we're at the bottom of the list */
            case Direction.Down:
                _topDexNum = (_topDexNum < _lastSeen - 6 && _index == 7) ? _topDexNum + 1 : _topDexNum;
                _index = _index < 7 ? _index + 1 : _index;
                break;
            // Move up the Pokedex by 7 at most
            case Direction.Left:
                _topDexNum = _topDexNum > 7 ? _topDexNum - 7 : 1;
                break;
            // Move down the Pokedex by 7 at most
            case Direction.Right:
                _topDexNum = _topDexNum < _lastSeen - 13 ? _topDexNum + 7 : _lastSeen - 6;
                break;
        }

        // Set the arrow position
        _arrow.anchoredPosition = new Vector2(0, -(8f + 16f * _index));
        // Set the navigation delay
        _navDelay = _initialPress ? 0.5f : 0.11f;
        _initialPress = false;

        // Update the Pokedex Information with any changes made
        _currentDexNum = _topDexNum - 1 + _index;
        SetPokedexInfo();
    }

    private void OpenOptions(int dexNumber)
    {
        _arrow.GetComponent<Image>().sprite = _emptyArrow;
        _navDirection = Direction.None;
        StopListeningForInput();
        _optionsMenu.Open(dexNumber);
    }
}