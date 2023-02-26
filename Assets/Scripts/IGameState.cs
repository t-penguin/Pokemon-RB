using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    public bool IsActiveState { get; set; }

    void EnterState();

    void ExitState();
}