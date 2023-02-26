using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public abstract class UI : MonoBehaviour
{
    public RectTransform mainArrow;
    protected Sprite emptyArrow;
    protected Sprite filledArrow;
    protected PlayerInput playerInput;
    protected float blinkDelay;
    public bool canInput;

    private void Awake()
    {
        emptyArrow = Resources.Load<Sprite>("UI/Empty Arrow");
        filledArrow = Resources.Load<Sprite>("UI/Filled Arrow");
        playerInput = GetComponent<PlayerInput>();
    }

    public abstract IEnumerator CloseUI();

    public abstract IEnumerator OpenUI();

    // Flashes a GameObject on and off
    protected void BlinkObject(GameObject gObject, float maxDelay)
    {
        if (blinkDelay <= 0)
            gObject.SetActive(!gObject.activeSelf);

        blinkDelay = blinkDelay <= 0 ? maxDelay : blinkDelay - Time.deltaTime;
    }

    // Flashes a List of GameObjects on and off in sync
    protected void BlinkObjects(List<GameObject> gObjects, float maxDelay)
    {
        if (blinkDelay <= 0)
        {
            foreach (GameObject o in gObjects)
                o.SetActive(!o.activeSelf);
        }

        blinkDelay = blinkDelay <= 0 ? maxDelay : blinkDelay - Time.deltaTime;
    }
}