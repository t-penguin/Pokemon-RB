using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapUI : MonoBehaviour
{
    public static event Action<string> OpenedMap;
    public static event Action ClosedMap;

    public static void OpenMap(string header) => OpenedMap?.Invoke(header);
    public static void CloseMap() => ClosedMap?.Invoke();

    [SerializeField] GameObject _map;
    [SerializeField] GameObject _background;
    [SerializeField] TextMeshProUGUI _mapHeader;

    private void OnEnable()
    {
        OpenedMap += ShowMap;
        ClosedMap += HideMap;
    }

    private void OnDisable()
    {
        OpenedMap -= ShowMap;
        ClosedMap -= HideMap;
    }

    public void ShowMap(string header)
    {
        _background.SetActive(true);
        _mapHeader.text = header;
        _mapHeader.gameObject.SetActive(true);
        // Show player location
        _map.SetActive(true);
    }

    public void HideMap()
    {
        _background.SetActive(false);
        _mapHeader.gameObject.SetActive(false);
        _map.SetActive(false);
    }
}
