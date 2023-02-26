using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Singleton reference
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    // Events
    public event Action<Interaction> InteractionStart;
    public void OnInteractionStart(Interaction interaction) => InteractionStart?.Invoke(interaction);

    public event Action<Interaction> InteractionEnd;
    public void OnInteractionEnd(Interaction interaction) => InteractionEnd?.Invoke(interaction);

    public event Action OpenMessageBox;
    public void OnOpenMessageBox() => OpenMessageBox?.Invoke();

    public event Action<string, bool, bool> ChangeMessageText;
    public void OnChangeMessageText(string text, bool instantText, bool listenForInput) => ChangeMessageText?.Invoke(text, instantText, listenForInput);

    public event Action CloseMessageBox;
    public void OnCloseMessageBox() => CloseMessageBox?.Invoke();

    public event Action ObtainFirstPokemon;
    public void OnObtainFirstPokemon() => ObtainFirstPokemon?.Invoke();

    public event Action ObtainPokedex;
    public void OnObtainPokedex() => ObtainPokedex?.Invoke();

    public event Action<int> ObtainBadge;
    public void OnObtainBadge(int badgeID) => ObtainBadge?.Invoke(badgeID);


}