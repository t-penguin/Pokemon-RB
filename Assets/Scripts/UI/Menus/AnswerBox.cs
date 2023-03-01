using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class AnswerBox : MonoBehaviour
{
    public const string YES = "YES";
    public const string NO = "NO";

    public static bool Continue;
    public static bool Answer;

    [SerializeField] private RectTransform _answerBox;
    [SerializeField] private RectTransform _arrow;
    [SerializeField] private TextMeshProUGUI _topAnswer;
    [SerializeField] private TextMeshProUGUI _botttomAnswer;

    private Vector2 _arrowDefaultPos = new Vector3(8, -8);
    private int _selection;
    private bool _yesIsTop;

    #region Events

    public static event Action<bool, Vector2> Opened;
    public static void Open(bool yesOnTop, Vector2 location) => Opened?.Invoke(yesOnTop, location);

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        Opened += OnOpen;
    }

    private void OnDisable()
    {
        Opened -= OnOpen;
    }

    #endregion

    #region Input Callbacks

    public void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (input.y == 0)
            return;

        Vector2 position = _arrow.localPosition;
        if (input.y > 0 && _selection == 1)
        {
            _selection = 0;
            position.y = -8;
        }
        else if (input.y < 0 && _selection == 0)
        {
            _selection = 1;
            position.y = -24;
        }
        _arrow.localPosition = position;
    }

    /* _yesIsTop == true        _yesIsTop == false
     * (0) YES                  (0) NO
     * (1) NO                   (1) YES     */
    public void OnConfirm(InputAction.CallbackContext context)
    {
        Answer = _selection == 0 ? _yesIsTop : !_yesIsTop;
        Close();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        Answer = false;
        Close();
    }

    #endregion

    public void OnOpen(bool yesOnTop, Vector2 location)
    {
        AnswerBox.Continue = false;
        _answerBox.localPosition = location;
        _yesIsTop = yesOnTop;
        _selection = 0;
        _arrow.localPosition = _arrowDefaultPos;
        _topAnswer.text = yesOnTop ? YES : NO;
        _botttomAnswer.text = yesOnTop ? NO : YES;
        _answerBox.gameObject.SetActive(true);

        TestInputManager.MoveAction.performed += OnNavigate;
        TestInputManager.ConfirmAction.started += OnConfirm;
        TestInputManager.CancelAction.started += OnCancel;
    }

    public void Close()
    {
        Continue = true;
        _answerBox.gameObject.SetActive(false);

        TestInputManager.MoveAction.performed -= OnNavigate;
        TestInputManager.ConfirmAction.started -= OnConfirm;
        TestInputManager.CancelAction.started -= OnCancel;
    }
}
