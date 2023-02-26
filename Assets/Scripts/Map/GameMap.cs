using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GameMap : MonoBehaviour
{
    [SerializeField] List<GameMap> _mapsToLoad;

    public bool IsLoaded { get; private set; }

    public void LoadMap()
    {
        if (!gameObject.activeSelf)
        {
            Debug.Log($"Loading map {gameObject.name}");
            gameObject.SetActive(true);
            IsLoaded = true;
        }
    }

    public void UnloadMap()
    {
        if (gameObject.activeSelf)
        {
            Debug.Log($"Unloading map {gameObject.name}");
            gameObject.SetActive(false);
            IsLoaded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log($"Collision with GameMap {gameObject.name}");

            if (_mapsToLoad.Count == 1)
                GameMapManager.LoadMap(this);
            else if (_mapsToLoad.Count > 1)
                GameMapManager.LoadMaps(_mapsToLoad);
        }
    }
}