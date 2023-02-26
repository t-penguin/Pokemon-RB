using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : Interaction
{
    [SerializeField][TextArea(3, 5)] string _message;
    string _formattedMessage;

    private void Awake()
    {
        _formattedMessage = _message.FontFormat();
    }

    public override void StartInteraction(InteractionManager manager)
    {
        base.StartInteraction(manager);
        MessageBox.Open();
        MessageBox.SetOverTime(_formattedMessage);
        MessageBox.CloseMessageBox += EndInteraction;
    }

    public override void OnConfirm(InputAction.CallbackContext context)
    {
        base.OnConfirm(context);
    }

    public override void OnCancel(InputAction.CallbackContext context)
    {
        base.OnCancel(context);
    }

    public override void OnNavigate(InputAction.CallbackContext context) { }

    public override void EndInteraction()
    {
        MessageBox.CloseMessageBox -= EndInteraction;
        _manager.ExitState();
    }
}
