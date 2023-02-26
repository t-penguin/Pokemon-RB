using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildArea : MonoBehaviour
{
    [SerializeField] int _encounterRate;
    [SerializeField] int _index;
    [SerializeField] bool _isRoute21Grass;

    public int GetEncounterRate() => _encounterRate;
    public int GetIndex() => _index;
    public bool IsRoute21Grass() => _isRoute21Grass;
}