using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Converts this direction to a Vector2 of length 1, or 0 if no direction.
    /// </summary>
    public static Vector2 ToVector2(this Direction direction)
    {
        switch (direction)
        {
            default:
                return Vector2.zero;
            case Direction.Up:
                return Vector2.up;
            case Direction.Down:
                return Vector2.down;
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
        }
    }

    /// <summary>
    /// Formats this string to display as intended with the Pokemon-GB font.
    /// </summary>
    public static string FontFormat(this string str)
    {
        string formatted = str;

        foreach (string key in GameData.FontSubstitutions.Keys)
            formatted = formatted.Replace(key, GameData.FontSubstitutions[key]);

        return formatted;
    }
}