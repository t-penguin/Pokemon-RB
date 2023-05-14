using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class MessageBox : MonoBehaviour
{
    #region Events

    public static event Action OpenMessageBox;
    public static event Action CloseMessageBox;
    public static event Action Focus;
    public static event Action Unfocus;
    public static event Action<string> SetText;
    public static event Action<string> SetTextOverTime;
    public static event Action<string[]> SetTextArrayOverTime;

    public static void Open() => OpenMessageBox?.Invoke();
    public static void Close() => CloseMessageBox?.Invoke();
    public static void BringToFront() => Focus?.Invoke();
    public static void ResetSortOrder() => Unfocus?.Invoke();
    public static void Set(string text) => SetText?.Invoke(text);
    public static void Clear() => SetText?.Invoke(string.Empty);
    public static void SetOverTime(string text) => SetTextOverTime?.Invoke(text);
    public static void SetOverTime(string[] text) => SetTextArrayOverTime?.Invoke(text);

    #endregion

    [SerializeField] GameObject _arrow;
    [SerializeField] GameObject _messageBox;
    [SerializeField] TextMeshProUGUI _messageText;
    [SerializeField] Scrollbar _scrollbar;

    public const float FAST_TEXT_DELAY = 2 / 60f;
    public const float NORMAL_TEXT_DELAY = 6 / 60f;
    public const float SLOW_TEXT_DELAY = 10 / 60f;

    public static float TextDelay;
    public static bool FinishedTextDisplay;
    public static bool FinishedMessage;
    // Used when displaying messages in battle
    public static bool Continue;
    public static bool Blink;

    private Canvas _canvas;

    private float _normalTextDelay;
    private float _spedUpTextDelay;

    private Vector2 _size;
    private int _totalLines;
    private int _nextLine;
    private int _totalMessages;
    private int _nextMessage;
    private string _message;
    private string[] _messages;
    private string[] _splitMessage;

    private bool _fromInteraction;
    private bool _fromBattle;
    private bool _canBlink;
    private float _blinkDelay;

    #region Event Callbacks

    private void OnMessageBoxOpened()
    {
        _arrow.gameObject.SetActive(false);
        _messageBox.SetActive(true);
        _normalTextDelay = TextDelay;
        _messageText.text = string.Empty;

        Focus += OnFocus;
        Unfocus += OnUnfocus;
        SetText += OnSet;
        SetTextOverTime += OnSetOverTime;
        SetTextArrayOverTime += OnSetOverTime;
        OpenMessageBox -= OnMessageBoxOpened;
        CloseMessageBox += OnMessageBoxClosed;

        TestInputManager.ConfirmAction.started += OnConfirmOrCancel;
        TestInputManager.CancelAction.started += OnConfirmOrCancel;

        TestInputManager.ConfirmAction.canceled += OnConfirmOrCancelReleased;
        TestInputManager.CancelAction.canceled += OnConfirmOrCancelReleased;


        //_fromInteraction = (UnityEngine.Object)GameStateManager.currentGameState == GameStateManager.InteractionManager;
        _fromBattle = (UnityEngine.Object)GameStateManager.currentGameState == GameStateManager.BattleStateManager;

        if (_fromBattle)
        {
            Continue = false;
        }
    }

    private void OnMessageBoxClosed()
    {
        _messageBox.SetActive(false);
        TextDelay = _normalTextDelay;
        _messageText.rectTransform.localPosition = Vector2.zero;

        Focus -= OnFocus;
        Unfocus -= OnUnfocus;
        SetText -= OnSet;
        SetTextOverTime -= OnSetOverTime;
        SetTextArrayOverTime -= OnSetOverTime;
        OpenMessageBox += OnMessageBoxOpened;
        CloseMessageBox -= OnMessageBoxClosed;

        TestInputManager.ConfirmAction.started -= OnConfirmOrCancel;
        TestInputManager.CancelAction.started -= OnConfirmOrCancel;

        TestInputManager.ConfirmAction.canceled -= OnConfirmOrCancelReleased;
        TestInputManager.CancelAction.canceled -= OnConfirmOrCancelReleased;
    }

    private void OnFocus() => _canvas.sortingOrder = 7;
    private void OnUnfocus() => _canvas.sortingOrder = 3;

    private void OnSet(string text)
    {
        _size.y = 24;
        _messageText.rectTransform.sizeDelta = _size;
        _messageText.rectTransform.localPosition = Vector2.zero;
        _messageText.text = text.FontFormat();
        _canBlink = false;
        _arrow.gameObject.SetActive(false);
    }

    private void OnSetOverTime(string text)
    {
        Debug.Log($"Setting Message Text to :\n{text}");
        StartCoroutine(DisplayTextOverTime(text));
    }

    private void OnSetOverTime(string[] text) => StartCoroutine(DisplayTextOverTime(text));

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        _size = _messageText.rectTransform.sizeDelta;
        _canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        OpenMessageBox += OnMessageBoxOpened;
        TextDelay = FAST_TEXT_DELAY;
        _spedUpTextDelay = TextDelay / 2;
    }

    private void OnDisable()
    {
        OpenMessageBox -= OnMessageBoxOpened;
        CloseMessageBox -= OnMessageBoxClosed;
    }

    private void Update()
    {
        if (_canBlink)
            BlinkArrow();
    }

    #endregion

    private IEnumerator DisplayTextOverTime(string text)
    {
        _canBlink = false;
        FinishedTextDisplay = false;
        FinishedMessage = false;
        _messageText.text = "";

        _message = text.FontFormat();
        _splitMessage = _message.Split('\n');
        _totalLines = _splitMessage.Length;
        _nextLine = 0;

        _size.y = Mathf.Max(24, 8 + 16 * (_totalLines - 1));
        _messageText.rectTransform.sizeDelta = _size;

        yield return new WaitForSeconds(12 / 60f);

        // Display at most 2 lines
        for (int i = 0; i < Mathf.Min(2, _totalLines); i++)
            yield return StartCoroutine(DisplayLine(i));

        _nextMessage++;
        FinishedTextDisplay = true;
        _canBlink = true;
    }

    private IEnumerator DisplayTextOverTime(string[] text)
    {
        FinishedTextDisplay = false;
        FinishedMessage = false;
        _messages = text;
        _totalMessages = _messages.Length;
        _nextMessage = 0;
        yield return StartCoroutine(DisplayTextOverTime(_messages[0]));
    }

    private IEnumerator DisplayLine(int line)
    {
        char[] lineArray = _splitMessage[line].ToCharArray();
        for (int i = 0; i < lineArray.Length; i++)
        {
            _messageText.text += lineArray[i];
            yield return new WaitForSeconds(TextDelay);
        }

        _nextLine++;
        _messageText.text += "\n";

        FinishedMessage = _nextLine == _splitMessage.Length;
    }

    private IEnumerator ScrollToNextLine()
    {
        FinishedTextDisplay = false;
        Continue = false;
        float scrollStep = 1f / (_totalLines - 2);
        float valueAfter = _scrollbar.value - scrollStep;
        while (_scrollbar.value > valueAfter && _scrollbar.value > 0)
        {
            _scrollbar.value -= scrollStep / 4;
            yield return new WaitForSeconds(2 / 60f);
        }

        yield return StartCoroutine(DisplayLine(_nextLine));
        FinishedTextDisplay = true;
    }

    private void OnConfirmOrCancel(InputAction.CallbackContext context)
    {
        // Increase text speed
        TextDelay = _spedUpTextDelay;

        if (FinishedTextDisplay)
        {
            Continue = true;

            // Go to next line if there are more lines
            if (_nextLine < _totalLines)
                StartCoroutine(ScrollToNextLine());
            // Go to next message if there is more text
            else if (_nextMessage < _totalMessages)
            {
                StartCoroutine(DisplayTextOverTime(_messages[_nextMessage]));
                _canBlink = true;
            }
            // Close Message UI if there is no more text
            else if (!_fromBattle)
            {
                Close();
            }
        }
    }

    private void OnConfirmOrCancelReleased(InputAction.CallbackContext context)
    {
        // Decrease text speed
        TextDelay = _normalTextDelay;
        Continue = false;
    }

    private void BlinkArrow()
    {
        bool anotherLine = _nextLine < _totalLines;
        bool anotherMessage = _nextMessage < _totalMessages;
        bool waitingForInput = _fromBattle && !Continue && Blink;
        // Blink the arrow if there are more lines to display OR if there is another message to display
        if (FinishedTextDisplay && (anotherLine || anotherMessage || waitingForInput))
        {
            if (_blinkDelay <= 0)
                _arrow.SetActive(!_arrow.activeSelf);

            _blinkDelay = _blinkDelay <= 0 ? 34 / 60f : _blinkDelay - Time.deltaTime;
        }
        else if (_arrow.activeSelf)
            _arrow.SetActive(false);
    }
}