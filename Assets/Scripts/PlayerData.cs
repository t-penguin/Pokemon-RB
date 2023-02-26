using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static int MaxBalance = 999999;

    public static GameVersion Version { get; private set; }
    public static string Name { get; private set; }
    public static int ID { get; private set; }
    public static bool[] PokemonSeen { get; private set; }
    public static int NumPokemonSeen { get; private set; }
    public static bool[] PokemonCaught { get; private set; }
    public static int NumPokemonCaught { get; private set; }
    public static bool[] BadgesObtained { get; private set; }
    public static int Balance { get; private set; }
    public static float TimePlayed { get; private set; }


    public static bool Obtained_First_Pokemon { get; private set; }
    public static bool Obtained_Oaks_Parcel { get; private set; }
    public static bool Obtained_Pokedex { get; private set; }

    static PlayerData()
    {
        Name = "";
        PokemonSeen = new bool[152];
        PokemonCaught = new bool[152];
        BadgesObtained = new bool[8];

        //FOR TESTING, REMOVE LATER
        Version = GameVersion.Red;
        Obtained_Pokedex = true;
        for(int i = 1; i < 152; i++)
        {
            SetPokemonSeen(i);
            SetPokemonCaught(i);
        }
        AddToBalance(500000);
        AddTimePlayed(4324);
        SetPlayerName("JOSEPH");
        GenerateID();
        RivalData.SetRivalName("GARY");
    }

    /// <summary>Sets the player's name. Should only be called at the start of the game</summary>
    public static void SetPlayerName(string name)
    {
        Name = name;
        GameData.FontSubstitutions.Add("[player]", name);
    }

    /// <summary>Sets the badge value for a player to true</summary>
    public static void SetBadge(int badgeNum, bool obtained = true) => BadgesObtained[badgeNum] = obtained;

    /// <summary>Sets whether a Pokemon in the Pokedex is registered as 'seen'</summary>
    public static void SetPokemonSeen(int dexNumber, bool seen = true)
    {
        PokemonSeen[dexNumber] = seen;
        NumPokemonSeen += seen ? 1 : -1;
    }

    /// <summary>Reverse searches the Pokedex for the last Pokemon registered as seen</summary>
    /// <returns>The Pokemon's Pokedex number</returns>
    public static int GetLastSeen()
    {
        for(int i = PokemonSeen.Length - 1; i > 0; i--)
        {
            if (PokemonSeen[i]) return i;
        }

        return 0;
    }

    /// <summary>Sets whether a Pokemon in the Pokedex is registered as 'caught'</summary>
    public static void SetPokemonCaught(int dexNumber, bool caught = true)
    {
        PokemonCaught[dexNumber] = caught;
        NumPokemonCaught += caught ? 1 : -1;
    }

    public static void AddToBalance(int money)
    {
        Balance += money;
        if (Balance > MaxBalance) Balance = MaxBalance;
    }

    public static void SubtractFromBalance(int money)
    {
        Balance -= money;
        if (Balance < 0) Balance = 0;
    }

    public static void AddTimePlayed(float time)
    {
        TimePlayed += time;
        // Cap the time at 999:59:59
        if (TimePlayed > 3599999) TimePlayed = 3599999;
    }

    public static string FormatTime()
    {
        int totalMinutes = Mathf.FloorToInt(TimePlayed / 60);
        int hours = Mathf.FloorToInt(totalMinutes / 60);
        int minutes = totalMinutes % 60;

        return $"{hours,3}:{minutes}";
    }

    public static void GenerateID() => ID = Random.Range(1, 100000);
}

public enum GameVersion
{
    Red,
    Blue
}