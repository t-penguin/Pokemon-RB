using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleMessages
{
    // Placeholders
    public const string POKEMON = "[POKEMON]";
    public const string MOVE = "[MOVE]";
    public const string HITS = "[HITS]";
    public const string STAT = "[STAT]";
    public const string PLAYER = "[PLAYER]";

    // Move Messages
    public const string MOVE_USED = "[POKEMON]\nused [MOVE]!";
    public const string MOVE_MISSED = "[POKEMON]'s\nattack missed!";
    public const string MOVE_FAILED = "But it failed!";
    public const string MOVE_DOES_NOT_AFFECT = "It doesn't affect\n[POKEMON]!";
    public const string MOVE_SUPER_EFFECTIVE = "It's super\neffective!";
    public const string MOVE_NOT_EFFECTIVE = "It's not very\neffective...";
    public const string MOVE_CRITICAL_HIT = "Critical hit!";
    public const string MOVE_MULTI_HIT = "Hit the enemy\n[HITS] times!";
    public const string NO_MOVES_LEFT = "[POKEMON]\nhas no\nmoves left!";

    // Multi-turn attack messages
    public const string ATTACK_UNLEASHED_ENERGY = "[POKEMON]\nunleashed energy!";
    public const string ATTACK_CONTINUES = "[POKEMON]'s\nattack continues!";
    public const string ATTACK_THRASHING = "[POKEMON]'s\nthrashing about!";
    public const string CHARGING_DIG = "[POKEMON]\ndug a hole!";
    public const string CHARGING_FLY = "[POKEMON]\nflew up high!";
    public const string CHARGING_RAZOR_WIND = "[POKEMON]\nmade a whirlwind!";
    public const string CHARGING_SKULL_BASH = "[POKEMON]\nlowered its head!";
    public const string CHARGING_SKY_ATTACK = "[POKEMON]\nis glowing!";
    public const string CHARGING_SOLARBEAM = "[POKEMON]\ntook in sunlight!";
    public const string USER_FATIGUED = "[POKEMON]\nbecame confused\ndue to fatigue!";
    public const string USER_RECHARGING = "[POKEMON]\nmust recharge!";

    // Stat change messages
    public const string TARGET_STAT_LOWERED = "[POKEMON]'s\n[STAT] fell!";
    public const string TARGET_STAT_GREATLY_LOWERED = "[POKEMON]'s\n[STAT]\ngreatly fell!";
    public const string USER_STAT_RAISED = "[POKEMON]'s\n[STAT] rose!";
    public const string USER_STAT_GREATLY_RAISED = "[POKEMON]'s\n[STAT]\ngreatly rose!";
    public const string MOVE_NOTHING_HAPPENED = "Nothing happened!";

    // Status condition messages
    public const string TARGET_PARALYZED = "[POKEMON]'s\nparalyzed! It may\nnot attack!";
    public const string TARGET_SLEPT = "[POKEMON]\nfell asleep!";
    public const string TARGET_BURNED = "[POKEMON]\nwas burned!";
    public const string TARGET_FROZEN = "[POKEMON]\nwas frozen solid!";
    public const string TARGET_POISONED = "[POKEMON]\nwas poisoned!";
    public const string TARGET_BADLY_POISONED = "[POKEMON]'s\nbadly poisoned!";
    public const string TARGET_CONFUSED = "[POKEMON]\nbecame confused!";
    public const string TARGET_SEEDED = "[POKEMON]\nwas seeded!";
    public const string TARGET_DISABLED = "[POKEMON]'s\n[MOVE] was\ndisabled!";
    public const string USER_FOCUSED = "[POKEMON]'s\ngetting pumped!";
    public const string USER_RESTED = "[POKEMON]\nstarted sleeping!";

    // Move effect messages
    public const string EFFECT_HEALTH_SAPPED = "Sucked health from\n[POKEMON]!";
    public const string EFFECT_HEALTH_REGAINED = "[POKEMON]\nregained health!";
    public const string EFFECT_DREAM_EATER = "[POKEMON]'s\ndream was eaten!";
    public const string EFFECT_REFLECT = "[POKEMON]\ngained armor!";
    public const string EFFECT_LIGHT_SCREEN = "[POKEMON]'s\nprotected against\nspecial attacks!";
    public const string EFFECT_MIST = "[POKEMON]'s\nshrouded in mist!";
    public const string EFFECT_TELEPORT = "[POKEMON]\nran from battle!";
    public const string EFFECT_CONVERSION = "Converted type to\n[POKEMON]'s!";
    public const string EFFECT_HAZE = "All STATUS changes\nare eliminated!";
    public const string EFFECT_SPLASH = "No effect!";
    public const string EFFECT_RECOIL = "[POKEMON]'s\nhit with recoil!";
    public const string EFFECT_CRASH = "[POKEMON]\nkept going and\ncrashed!";
    public const string EFFECT_ROAR = "[POKEMON]\nran away scared!";
    public const string EFFECT_WHIRLWIND = "[POKEMON]\nwas blown away!";

    // Recurrent damage messages
    public const string RECURRENT_SAP = "LEECH SEED saps\n[POKEMON]!";
    public const string RECURRENT_BURN = "[POKEMON]'s\nhurt by the burn!";
    public const string RECURRENT_POISON = "[POKEMON]'s\nhurt by poison!";


    public const string MOVE_DISABLED = "[MOVE] is\ndisabled!";
    public const string CANNOT_ESCAPE = "Couldn't escape!";
    public const string GOT_AWAY_SAFELY = "Got away safely!";
    public const string USE_NEXT_POKEMON = "Use next\nPOK�MON?";
    public const string USER_FLINCHED = "[POKEMON]\nflinched!";
    public const string POKEMON_FAINTED = "[POKEMON]\nfainted!";
    public const string OUT_OF_POKEMON = "[PLAYER] is out of\nuseable POK�MON!";
    public const string BLACKED_OUT = "[PLAYER] blacked\nout!";

    public static IEnumerator Display(string text, BattlePokemon pokemon = null, BaseMove move = null,
        int numHits = 0, StatType stat = StatType.NONE, bool waitForInput = true)
    {
        if (pokemon != null)
        {
            string pokemonName = pokemon.TrainerIsPlayer ? pokemon.Name : $"Enemy {pokemon.Name}";
            text = text.Replace(POKEMON, pokemonName);
        }

        if (move != null)
            text = text.Replace(MOVE, move.Name);

        if (numHits > 0)
            text = text.Replace(HITS, numHits.ToString());

        if (stat != StatType.NONE)
        {
            string statName = stat.ToString();
            text = text.Replace(STAT, statName);
        }

        text = text.Replace(PLAYER, PlayerData.Name);

        MessageBox.Blink = waitForInput;
        MessageBox.SetOverTime(text);
        while (!MessageBox.FinishedMessage)
            yield return null;

        // Exit if not waiting for input
        if (!waitForInput)
            yield break;

        while (!MessageBox.Continue)
            yield return null;
        MessageBox.Blink = false;

        yield return new WaitForSeconds(10 / 60);
    }
}