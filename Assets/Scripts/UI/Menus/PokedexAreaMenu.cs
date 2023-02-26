using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PokedexOptionsMenu))]
public class PokedexAreaMenu : Menu
{
    [SerializeField] GameObject _areaScreen;
    [SerializeField] MapUI _map;
    [SerializeField] GameObject[] _locationMarkers;
    List<GameObject> _activeMarkers;

    private PokedexOptionsMenu _optionsMenu;
    private PokedexMenu _pokedexMenu;

    #region Input Callbacks

    protected override void OnNavigate(InputAction.CallbackContext context) { }

    protected override void OnCancel(InputAction.CallbackContext context) => StartCoroutine(Close());

    protected override void OnConfirm(InputAction.CallbackContext context) => StartCoroutine(Close());

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
        _activeMarkers = new List<GameObject>();
    }

    private void Update()
    {
        // Flash location markers
        if (_activeMarkers.Count > 0)
            BlinkObjects(_activeMarkers, 26 / 60f);
    }

    public IEnumerator Open(int pokedexNumber)
    {
        _pokedexMenu.PokedexScreen.SetActive(false);
        yield return new WaitForSeconds(10 / 60f);
        _areaScreen.SetActive(true);
        yield return new WaitForSeconds(10 / 60f);

        // Set Map header and display the Map
        MapUI.OpenMap($"{PokemonData.Names[pokedexNumber]}< NEST");

        // Clear the active markers list
        _activeMarkers.Clear();
        // Get the selected Pokemon's locations based on the current game version
        int[] locations = PlayerData.Version == GameVersion.Red ? PokemonData.RedLocations[pokedexNumber]
                                                                : PokemonData.BlueLocations[pokedexNumber];
        /* Add active markers for each location the Pokemon can be found
            * If the Pokemon cannot be found by walking or surfing, display the Area Unknown box */
        foreach (int locationIndex in locations)
        {
            if (locationIndex == 0)
                _locationMarkers[0].SetActive(true);
            else
                _activeMarkers.Add(_locationMarkers[locationIndex]);
        }

        ListenForInput();
    }

    private IEnumerator Close()
    {
        StopListeningForInput();
        MapUI.CloseMap();
        // Deactivate all markers
        foreach (GameObject g in _locationMarkers)
            g.SetActive(false);

        _areaScreen.SetActive(false);
        _activeMarkers.Clear();

        yield return new WaitForSeconds(40 / 60f);

        _pokedexMenu.PokedexScreen.SetActive(true);
        _optionsMenu.Close();
    }
}