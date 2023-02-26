public abstract class BaseGameState
{
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    void UpdateStates() { }

    void SwitchState() { }

    void SetSuperState() { }

    void SetSubState() { }
}