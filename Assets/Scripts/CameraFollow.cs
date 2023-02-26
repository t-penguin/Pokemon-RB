using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Vector3 offset;

    private void Awake()
    {
        UpdateCameraPosition();
    }

    public void UpdateCameraPosition() => transform.position = _player.position + offset;
}