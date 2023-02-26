using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(PokedexMenu))]
[RequireComponent(typeof(PokedexDataMenu))]
[RequireComponent(typeof(PokedexAreaMenu))]
public class PokedexOptionsMenu : Menu
{
    [SerializeField] RectTransform _arrow;

    private PokedexMenu _pokedexMenu;
    private PokedexDataMenu _dataMenu;
    private PokedexAreaMenu _areaMenu;

    private int _selection;
    private int _pokedexNumber;
    private bool _initialPress;
    private float _navDelay;
    private Direction _navDirection;

    #region Input Callbacks

    protected override void OnNavigate(InputAction.CallbackContext context) => SetNavigation(context.ReadValue<Vector2>());
    protected override void OnConfirm(InputAction.CallbackContext context) => SelectOption(_selection);
    protected override void OnCancel(InputAction.CallbackContext context) => Close();

    #endregion

    #region Monobehaviour Callbacks

    protected override void Awake()
    {
        base.Awake();
        _pokedexMenu = GetComponent<PokedexMenu>();
        _dataMenu = GetComponent<PokedexDataMenu>();
        _areaMenu = GetComponent<PokedexAreaMenu>();
    }

    private void Update()
    {
        HandleNavigation();
    }

    #endregion

    public void Open(int dexNumber)
    {
        _selection = 0;
        _pokedexNumber = dexNumber;
        _arrow.anchoredPosition = Vector2.zero;
        _arrow.gameObject.SetActive(true);
        ListenForInput();
    }

    public void Close()
    {
        StopListeningForInput();
        _navDirection = Direction.None;
        _arrow.gameObject.SetActive(false);
        _pokedexMenu.ResumeFocus();
    }

    private void SetNavigation(Vector2 input)
    {
        Direction direction = Direction.None;
        _initialPress = true;

        // This menu's navigation is affected by both horizontal and vertical input
        if (input == Vector2.zero) _navDelay = 0f;
        else if (input.y > 0) direction = Direction.Up;
        else if (input.y < 0) direction = Direction.Down;

        _navDirection = direction;
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

        if(_navDirection == Direction.Up)
            _selection = _selection > 0 ? _selection - 1 : 0;
        else if(_navDirection == Direction.Down)
            _selection = _selection < 3 ? _selection + 1 : 3;

        // Set the arrow position
        _arrow.anchoredPosition = new Vector2(0, -16f * _selection);
        // Set the navigation delay
        _navDelay = _initialPress ? 0.5f : 0.11f;
        _initialPress = false;
    }

    private void SelectOption(int selection)
    {
        switch(selection)
        {
            default:
            // Data
            case 0:
                StopListeningForInput();
                _navDirection = Direction.None;
                StartCoroutine(_dataMenu.Open(_pokedexNumber));
                break;
            // Cry
            case 1:
                AudioManager.PlaySoundFX(PokemonData.Cries[_pokedexNumber]);
                break;
            // Area
            case 2:
                StopListeningForInput();
                _navDirection = Direction.None;
                StartCoroutine(_areaMenu.Open(_pokedexNumber));
                break;
            // Quit
            case 3:
                StopListeningForInput();
                _navDirection = Direction.None;
                _arrow.gameObject.SetActive(false);
                StartCoroutine(_pokedexMenu.Close());
                break;
        }
    }
}