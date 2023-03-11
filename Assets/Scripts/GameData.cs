using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Dictionary<string, string> FontSubstitutions = new Dictionary<string, string>
    {
        { "'d", "#" },
        { "'l", "%" },
        { "'s", "<" },
        { "'t", "=" },
        { "'v", ">" },
        { "'r", "_" },
        { "'m", "`" },
        { "PKMN", "{}" },
        { ":L", "@" },
    };
}