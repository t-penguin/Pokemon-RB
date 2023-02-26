using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RivalData
{
    public static string Name { get; private set; }

    /// <summary>Sets the rival's name. Should only be called at the start of the game</summary>
    public static void SetRivalName(string name)
    {
        Name = name;
        GameData.FontSubstitutions.Add("[rival]", name);
    }
}