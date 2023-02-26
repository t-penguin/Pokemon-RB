using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TeleportSquare : MonoBehaviour
{
    [Tooltip("Set to true if the player should teleport as soon as they step on this square.\n\n" +
        "Set to false if the player should teleport when trying to move off this square in a certain direction.")]
    [SerializeField] bool _teleportOnWalkOn;
    [Tooltip("The direction in which attempting to walk towards will trigger the teleport.\n\n" +
        "Only used if Teleport On Walk On is disabled.")]
    [SerializeField] Direction _triggerDirection;
    [SerializeField] Vector2 _targetPosition;
    [SerializeField] GameMap _targetMap;
    [SerializeField] Direction _stepOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement pMovement = collision.GetComponent<PlayerMovement>();

            if (!_teleportOnWalkOn)
                pMovement.SetTeleport(_targetPosition, _targetMap, _stepOff, false, _triggerDirection);
            else if (!PlayerMovement.Teleporting)
                pMovement.SetTeleport(_targetPosition, _targetMap, _stepOff);
        }
    }
}