using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class TrainerCardMenu : Menu
{
    private const int MENU_ID = 4;

    [SerializeField] GameObject _background;
    [SerializeField] GameObject _trainerCardInfo;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _money;
    [SerializeField] TextMeshProUGUI _time;
    [SerializeField] List<Image> _icons;

    private Sprite[] _badgeIcons;

    #region Events

    public static event Action<int> OpenedTrainerCard;
    public static event Action ClosedTrainerCard;

    #endregion

    #region Input Callbacks

    protected override void OnNavigate(InputAction.CallbackContext context) { }

    protected override void OnConfirm(InputAction.CallbackContext context) => StartCoroutine(Close());

    protected override void OnCancel(InputAction.CallbackContext context) => StartCoroutine(Close());

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

    #region Monobehaviour Callbacks

    private void Start()
    {
        _badgeIcons = Resources.LoadAll<Sprite>("UI/TrainerCardIcons");
        EventManager.current.OnObtainBadge(1);
        EventManager.current.OnObtainBadge(2);
    }

    private void OnEnable()
    {
        EventManager.current.ObtainBadge += AddBadge;

        MainMenu.Opened += ListenForOpen;
        MainMenu.Closed += StopListeningForOpen;
    }

    private void OnDisable()
    {
        EventManager.current.ObtainBadge -= AddBadge;

        MainMenu.Opened -= ListenForOpen;
        MainMenu.Closed -= StopListeningForOpen;
    }

    #endregion

    private void OpenTrainerCard(int menuID)
    {
        if (menuID != MENU_ID)
            return;

        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        OpenedTrainerCard?.Invoke(MENU_ID);
        Debug.Log("Opening the Trainer Card");

        _background.SetActive(true);
        SetInfo();

        yield return new WaitForSeconds(48 / 60f);

        _trainerCardInfo.SetActive(true);
        ListenForInput();
    }

    private IEnumerator Close()
    {
        StopListeningForInput();
        _trainerCardInfo.SetActive(false);

        yield return new WaitForSeconds(30 / 60f);

        _background.SetActive(false);
        ClosedTrainerCard?.Invoke();
    }

    private void SetInfo()
    {
        _name.text = $"NAME/{PlayerData.Name}";
        _money.text = $"MONEY/${PlayerData.Balance}";
        _time.text = $"TIME/{PlayerData.FormatTime()}";
    }

    private void AddBadge(int badgeNum) => _icons[badgeNum - 1].sprite = _badgeIcons[badgeNum - 1];

    private void ListenForOpen() => MainMenu.OpenedSubMenu += OpenTrainerCard;
    private void StopListeningForOpen() => MainMenu.OpenedSubMenu -= OpenTrainerCard;
}