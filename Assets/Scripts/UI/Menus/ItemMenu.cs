using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ItemMenu : Menu
{
    private const int MENU_ID = 3;
    private const string CANCEL = "CANCEL";

    [SerializeField] RectTransform _arrow;
    [SerializeField] RectTransform _secondArrow;
    [SerializeField] GameObject _itemBox;
    [SerializeField] TextMeshProUGUI[] _itemNames = new TextMeshProUGUI[4];
    [SerializeField] TextMeshProUGUI[] _itemQuantities = new TextMeshProUGUI[4];
    [SerializeField] GameObject _itemScrollArrow;

    public bool InBattle { get; private set; }
    private BattleStateManager _battle;

    private int _topSelection;
    private int _currentSelection;
    private int _arrowIndex;
    private bool _blink;
    private List<Item> _currentItemList;
    private List<int> _currentItemQuantities;
    private Player _player;

    private float _navDelay;
    private Direction _navDirection;
    private bool _initialPress;

    #region Events

    public static event Action<int> OpenedItemMenu;
    public static event Action ClosedItemMenu;

    #endregion

    #region Input Callbacks

    protected override void OnNavigate(InputAction.CallbackContext context) => SetNavigation(context.ReadValue<Vector2>());

    protected override void OnConfirm(InputAction.CallbackContext context)
    {
        if (_currentSelection == _currentItemList.Count)
            StartCoroutine(Close());
        else
            StartCoroutine(NotImplemented("Items are not\nyet implemented!"));
    }

    protected override void OnCancel(InputAction.CallbackContext context)
    {
        StartCoroutine(Close());
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        StartCoroutine(NotImplemented("Switching items\nis not yet\nimplemented!"));
    }

    #endregion

    private IEnumerator NotImplemented(string text)
    {
        StopListeningForInput();

        if(InBattle)
            MessageBox.BringToFront();
        else
            MessageBox.Open();

        MessageBox.SetOverTime(text);

        while (!MessageBox.FinishedMessage)
            yield return null;
        while (!MessageBox.Continue)
            yield return null;

        if (InBattle)
            MessageBox.ResetSortOrder();
        else
            MessageBox.Close();

        ListenForInput();
    }

    protected override void ListenForInput()
    {
        base.ListenForInput();
        TestInputManager.SelectAction.started += OnSelect;
    }

    protected override void StopListeningForInput()
    {
        base.StopListeningForInput();
        TestInputManager.SelectAction.started -= OnSelect;
    }

    #region Monobehaviour Callbacks

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
    }

    private void Update()
    {
        HandleNavigation();

        if (_blink)
            BlinkObject(_itemScrollArrow, 18 / 60f);
    }

    #endregion

    public void OpenFromMain(int menuID)
    {
        if (menuID != MENU_ID)
            return;

        InBattle = false;
        SetItemList(_player.Bag.Items, _player.Bag.Quantities);
        StartCoroutine(Open());
    }

    public void OpenFromBattle(BattleStateManager battle, Bag bag)
    {
        InBattle = true;
        _battle = battle;
        SetItemList(bag.Items, bag.Quantities);
        StartCoroutine(Open());
    }

    public IEnumerator Open()
    {
        OpenedItemMenu?.Invoke(MENU_ID);
        Debug.Log("Opening the Item Menu");

        SetItemMenu();
        _blink = false;

        yield return new WaitForSeconds(8 / 60f);
            
        _itemBox.SetActive(true);
        ListenForInput();
    }

    public IEnumerator Close()
    {
        StopListeningForInput();
        _itemBox.SetActive(false);
        _blink = false;

        yield return new WaitForSeconds(4 / 60f);

        ClosedItemMenu?.Invoke();
    }

    public void SetNavigation(Vector2 input)
    {
        _navDirection = Direction.None;
        _initialPress = true;

        if (input.y == 0) _navDelay = 0f;
        else if (input.y > 0) _navDirection = Direction.Up;
        else if (input.y < 0) _navDirection = Direction.Down;
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

        // Navigate the menu once the delay has finished and there is input to navigate
        if (_navDirection == Direction.Up)
        {
            if (_topSelection > 0 && _arrowIndex == 0) _topSelection--;
            if (_arrowIndex > 0) _arrowIndex--;
        }
        else if (_navDirection == Direction.Down)
        {
            if (_topSelection < _currentItemList.Count - 2 && _arrowIndex == 2) _topSelection++;
            if (_arrowIndex < 2) _arrowIndex++;
        }

        // Set the arrow position
        _arrow.localPosition = new Vector2(8f, -(16f + 16 * _arrowIndex));

        // Set the navigation delay
        _navDelay = _initialPress ? 0.5f : 0.11f;
        _initialPress = false;
        _blink = _topSelection + 3 < _currentItemList.Count;

        SetItemMenu();
    }

    private void SetItemMenu()
    {
        int itemIndex;
        for (int i = 0; i < _itemNames.Length; i++)
        {
            itemIndex = _topSelection + i;
            // Past the last item in the bag, quantity is blank
            if (itemIndex >= _currentItemList.Count)
                _itemQuantities[i].text = "";

            // Item index is valid, display item and quantity
            if (itemIndex < _currentItemList.Count)
            {
                _itemNames[i].text = _currentItemList[itemIndex].name;
                _itemQuantities[i].text = _currentItemList[itemIndex].isKeyItem ? string.Empty : $"*{_currentItemQuantities[itemIndex],2}";
            }
            // Cancel button is immediately after all the items in the bag
            else if (itemIndex == _currentItemList.Count) _itemNames[i].text = CANCEL;
            // Any text after CANCEL should be blank
            else _itemNames[i].text = string.Empty;
        }

        _currentSelection = _topSelection + _arrowIndex;
    }

    public void SetItemList(List<Item> itemList, List<int> itemQuantities)
    {
        _currentItemList = itemList;
        _currentItemQuantities = itemQuantities;
    }

    private void ListenFromMain()
    {
        MainMenu.OpenedSubMenu += OpenFromMain;
        BattleStateManager.BattleStarted -= ListenFromBattle;
    }

    private void ListenFromBattle()
    {
        BattleStateManager.OpenedBag += OpenFromBattle;
        MainMenu.Opened -= ListenFromMain;
    }

    private void StopListeningFromMain() => MainMenu.OpenedSubMenu -= OpenFromMain;
    private void StopListeningFromBattle() => BattleStateManager.OpenedBag -= OpenFromBattle;
}