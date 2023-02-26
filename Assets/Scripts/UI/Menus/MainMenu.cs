using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenu : Menu
{
    public static bool CanOpen;

    [SerializeField] GameObject _menuBox;
    [SerializeField] RectTransform _arrow;
    [SerializeField] TextMeshProUGUI _menuText;
    private int _numMenus;
    private int _currentSelection;
    private Direction _previousNav;

    #region Events

    public static event Action Opened;
    public static event Action Closed;
    public static event Action<int> OpenedSubMenu;

    #endregion

    #region Input Callbacks

    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (!CanOpen)
            return;

        Movement.CanMove = false;
        StartCoroutine(Open());
    }
    protected override void OnNavigate(InputAction.CallbackContext context) => Navigate(context.ReadValue<Vector2>());
    protected override void OnConfirm(InputAction.CallbackContext context) => OpenSubMenu(_currentSelection);
    protected override void OnCancel(InputAction.CallbackContext context) => Close();

    #endregion

    #region Monobehaviour Callbacks

    private void Start()
    {
        _currentSelection = 1;
        SetMenu(PlayerData.Obtained_Pokedex);
    }

    // Subscribe to some events on enable
    private void OnEnable()
    {
        // Obtained Pokedex += Set Menu
        TestInputManager.StartAction.started += OpenMenu;
        BattleStateManager.BattleStarted += StopListeningForOpen;
        BattleStateManager.BattleEnded += ListenForOpen;
    }

    // Unsubscribe from ALL events on disable
    private void OnDisable()
    {
        // Obtained Pokedex -= Set Menu
        TestInputManager.StartAction.started -= OpenMenu;
        BattleStateManager.BattleStarted += StopListeningForOpen;
        BattleStateManager.BattleEnded += ListenForOpen;

        StopListeningForInput();
        StopListeningForSubMenuOpen();
        StopListeningForSubMenuClose();
    }

    #endregion

    // Sets the size of the menu box and the text of the menu text
    private void SetMenu(bool gotPokedex)
    {
        // Reset menu values
        _menuText.text = "";
        _numMenus = 0;

        int extraHeight = 0;
        if(gotPokedex)
        {
            _menuText.text = "POKÈDEX\n\n";
            _numMenus = 1;
            extraHeight = 16;
        }

        _menuBox.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 112 + extraHeight);
        _menuText.text += $"POKÈMON\n\nITEM\n\n{PlayerData.Name}\n\nSAVE\n\nOPTION\n\nEXIT";
        _numMenus += 6;
    }

    // Opens the menu
    private IEnumerator Open()
    {
        // Unsubscribe the OpenMenu function from the start action
        TestInputManager.StartAction.started -= OpenMenu;

        // Delay the display of the text and arrow
        _menuText.gameObject.SetActive(false);
        _arrow.gameObject.SetActive(false);
        _menuBox.SetActive(true);

        yield return new WaitForSeconds(10 / 60f);

        _menuText.gameObject.SetActive(true);
        _arrow.gameObject.SetActive(true);
        _previousNav = Direction.None;

        // Subscribe to events
        ListenForInput();
        ListenForSubMenuOpen();

        Opened?.Invoke();
    }

    // Closes the main menu
    private void Close()
    {
        _menuText.gameObject.SetActive(false);
        _arrow.gameObject.SetActive(false);
        _menuBox.SetActive(false);

        // Unsubscribe from user input on close
        StopListeningForInput();

        // Resubscribe the OpenMenu function to the start action
        TestInputManager.StartAction.started += OpenMenu;

        Movement.CanMove = true;
        Closed?.Invoke();
    }

    // Navigates the menu based on user input
    private void Navigate(Vector2 input)
    {
        // Return early if there is no vertical input
        if (input.y == 0)
        {
            _previousNav = Direction.None;
            return;
        }

        Direction navDirection;
        if (input.y > 0)
            navDirection = Direction.Up;
        else
            navDirection = Direction.Down;

        // Prevents horizontal input from navigating the menu while vertical input is held down
        if (navDirection == _previousNav)
            return;

        // Going down the menu, increase current selection by 1 or loop to top
        if (navDirection == Direction.Down)
            _currentSelection = _currentSelection >= _numMenus ? 1 : _currentSelection + 1;
        // Going up the menu, decrease the current selection by 1 or loop to bottom
        else if (navDirection == Direction.Up)
            _currentSelection = _currentSelection <= 1 ? _numMenus : _currentSelection - 1;

        _previousNav = navDirection;
        _arrow.anchoredPosition = new Vector2(8f, -16f * _currentSelection);
    }

    // Opens one of the listed menus
    private void OpenSubMenu(int selection)
    {
        // Exit if the selection is out of bounds
        if (selection < 1 && selection > _numMenus) return;

        // Close Main Menu if the selection is the last one
        if (selection == _numMenus)
        {
            Close();
            return;
        }

        // Offset the selection if the pokedex has been obtained
        if (_numMenus == 6) selection++;

        Debug.Log($"Attempting to open menu with ID {selection}...");

        // Opens menu with the given ID number if there is one listening
        OpenedSubMenu?.Invoke(selection);
    }

    private void OnSubMenuOpened(int menuID)
    {
        // Listen for the given menu's close event
        switch (menuID)
        {
            default:
                Debug.Log($"Unknown menu ID {menuID}. Unsubscription failed.");
                return;
            case 1:
                PokedexMenu.ClosedPokedex += OnSubMenuClosed;
                break;
            case 2:
                PokemonMenu.Closed += OnSubMenuClosed;
                break;
            case 3:
                ItemMenu.ClosedItemMenu += OnSubMenuClosed;
                break;
            case 4:
                TrainerCardMenu.ClosedTrainerCard += OnSubMenuClosed;
                break;
        }

        StopListeningForSubMenuOpen();
        StopListeningForInput();
    }

    private void OnSubMenuClosed()
    {
        ListenForSubMenuOpen();
        ListenForInput();
    }

    // Subscribes functions to user input
    protected override void ListenForInput()
    {
        base.ListenForInput();
        TestInputManager.StartAction.started += OnCancel;
    }

    // Unsubscribes functions from user input
    protected override void StopListeningForInput()
    {
        base.StopListeningForInput();
        TestInputManager.StartAction.started -= OnCancel;
    }

    // Subscribe to the opening of sub menus
    private void ListenForSubMenuOpen()
    {
        PokedexMenu.OpenedPokedex           += OnSubMenuOpened;
        PokemonMenu.Opened       += OnSubMenuOpened;
        ItemMenu.OpenedItemMenu             += OnSubMenuOpened;
        TrainerCardMenu.OpenedTrainerCard   += OnSubMenuOpened;
    }

    private void StopListeningForSubMenuOpen()
    {
        PokedexMenu.OpenedPokedex           -= OnSubMenuOpened;
        PokemonMenu.Opened       -= OnSubMenuOpened;
        ItemMenu.OpenedItemMenu             -= OnSubMenuOpened;
        TrainerCardMenu.OpenedTrainerCard   -= OnSubMenuOpened;
    }

    private void StopListeningForSubMenuClose()
    {
        PokedexMenu.ClosedPokedex           -= OnSubMenuClosed;
        PokemonMenu.Closed       -= OnSubMenuClosed;
        ItemMenu.ClosedItemMenu             -= OnSubMenuClosed;
        TrainerCardMenu.ClosedTrainerCard   -= OnSubMenuClosed;
    }

    private void ListenForOpen() => TestInputManager.StartAction.started += OpenMenu;
    private void StopListeningForOpen() => TestInputManager.StartAction.started -= OpenMenu;
}