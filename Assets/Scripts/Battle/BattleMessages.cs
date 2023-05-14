using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleMessages
{
    // Placeholders
    public const string USER = "[USER]";
    public const string TARGET = "[TARGET]";
    public const string MOVE = "[MOVE]";
    public const string HITS = "[HITS]";
    public const string STAT = "[STAT]";

    // Move Messages
    public const string MOVE_USED = "[USER]\nused [MOVE]!";
    public const string MOVE_MISSED = "[USER]'s\nattack missed!";
    public const string MOVE_FAILED = "But it failed!";
    public const string MOVE_DOES_NOT_AFFECT = "It doesn't affect\n[TARGET]!";
    public const string MOVE_SUPER_EFFECTIVE = "It's super\neffective!";
    public const string MOVE_NOT_EFFECTIVE = "It's not very\neffective...";
    public const string MOVE_CRITICAL_HIT = "Critical hit!";
    public const string MOVE_MULTI_HIT = "Hit the enemy\n[HITS] times!";
    public const string NO_MOVES_LEFT = "[USER]\nhas no\nmoves left!";

    // Multi-turn attack messages
    public const string ATTACK_UNLEASHED_ENERGY = "[USER]\nunleashed energy!";
    public const string ATTACK_CONTINUES = "[USER]'s\nattack continues!";
    public const string ATTACK_THRASHING = "[USER]'s\nthrashing about!";
    public const string CHARGING_DIG = "[USER]\ndug a hole!";
    public const string CHARGING_FLY = "[USER]\nflew up high!";
    public const string CHARGING_RAZOR_WIND = "[USER]\nmade a whirlwind!";
    public const string CHARGING_SKULL_BASH = "[USER]\nlowered its head!";
    public const string CHARGING_SKY_ATTACK = "[USER]\nis glowing!";
    public const string CHARGING_SOLARBEAM = "[USER]\ntook in sunlight!";
    public const string USER_FATIGUED = "[USER]\nbecame confused\ndue to fatigue!";
    public const string USER_RECHARGING = "[USER]\nmust recharge!";

    // Stat change messages
    public const string TARGET_STAT_LOWERED = "[TARGET]'s\n[STAT] fell!";
    public const string TARGET_STAT_GREATLY_LOWERED = "[TARGET]'s\n[STAT]\ngreatly fell!";
    public const string USER_STAT_RAISED = "[USER]'s\n[STAT] rose!";
    public const string USER_STAT_GREATLY_RAISED = "[USER]'s\n[STAT]\ngreatly rose!";
    public const string MOVE_NOTHING_HAPPENED = "Nothing happened!";

    // Status condition messages
    public const string TARGET_PARALYZED = "[TARGET]'s\nparalyzed! It may\nnot attack!";
    public const string TARGET_SLEPT = "[TARGET]\nfell asleep!";
    public const string TARGET_BURNED = "[TARGET]\nwas burned!";
    public const string TARGET_FROZEN = "[TARGET]\nwas frozen solid!";
    public const string TARGET_POISONED = "[TARGET]\nwas poisoned!";
    public const string TARGET_BADLY_POISONED = "[TARGET]'s\nbadly poisoned!";
    public const string TARGET_CONFUSED = "[TARGET]\nbecame confused!";
    public const string TARGET_SEEDED = "[TARGET]\nwas seeded!";
    public const string TARGET_DISABLED = "[TARGET]'s\n[MOVE] was\ndisabled!";
    public const string USER_FOCUSED = "[USER]'s\ngetting pumped!";
    public const string USER_RESTED = "[USER]\nstarted sleeping!";

    // Move effect messages
    public const string EFFECT_HEALTH_SAPPED = "Sucked health from\n[TARGET]!";
    public const string EFFECT_HEALTH_REGAINED = "[USER]\nregained health!";
    public const string EFFECT_DREAM_EATER = "[TARGET]'s\ndream was eaten!";
    public const string EFFECT_REFLECT = "[USER]\ngained armor!";
    public const string EFFECT_LIGHT_SCREEN = "[USER]'s\nprotected against\nspecial attacks!";
    public const string EFFECT_MIST = "[USER]'s\nshrouded in mist!";
    public const string EFFECT_TELEPORT = "[USER]\nran from battle!";
    public const string EFFECT_CONVERSION = "Converted type to\n[TARGET]'s!";
    public const string EFFECT_HAZE = "All STATUS changes\nare eliminated!";
    public const string EFFECT_SPLASH = "No effect!";
    public const string EFFECT_RECOIL = "[USER]'s\nhit with recoil!";
    public const string EFFECT_CRASH = "[USER]\nkept going and\ncrashed!";
    public const string EFFECT_ROAR = "[TARGET]\nran away scared!";
    public const string EFFECT_WHIRLWIND = "[TARGET]\nwas blown away!";

    // Recurrent damage messages
    public const string RECURRENT_SAP = "[MOVE] saps\n[TARGET]!";
    public const string RECURRENT_BURN = "[USER]'s\nhurt by the burn!";
    public const string RECURRENT_POISON = "[USER]'s\nhurt by poison!";

    public static IEnumerator Display(string text, BattlePokemon user = null, BattlePokemon target = null,
        BaseMove move = null, int numHits = 0, StatType stat = StatType.NONE, bool waitForInput = true)
    {
        if (user != null)
        {
            string userName = user.TrainerIsPlayer ? user.Name : $"Enemy {user.Name}";
            text = text.Replace(USER, userName);
        }

        if (target != null)
        {
            string targetName = target.TrainerIsPlayer ? target.Name : $"Enemy {target.Name}";
            text = text.Replace(TARGET, targetName);
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