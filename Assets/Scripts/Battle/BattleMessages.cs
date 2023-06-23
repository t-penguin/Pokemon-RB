using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleMessages
{
    // Placeholders
    public const string POKEMON = "[POKEMON]";
    public const string MOVE = "[MOVE]";
    public const string VALUE = "[VALUE]";
    public const string STAT = "[STAT]";
    public const string PLAYER = "[PLAYER]";
    public const string TRAINER = "[TRAINER]";

    // Move Messages
    public const string MOVE_USED = "[POKEMON]\nused [MOVE]!";
    public const string MOVE_MISSED = "[POKEMON]'s\nattack missed!";
    public const string MOVE_FAILED = "But, it failed!";
    public const string MOVE_DOES_NOT_AFFECT = "It doesn't affect\n[POKEMON]!";
    public const string MOVE_SUPER_EFFECTIVE = "It's super\neffective!";
    public const string MOVE_NOT_EFFECTIVE = "It's not very\neffective...";
    public const string MOVE_CRITICAL_HIT = "Critical hit!";
    public const string MOVE_MULTI_HIT = "Hit the enemy\n[VALUE] times!";
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
    public const string STAT_CHANGE_FAILED = "Nothing happened!";

    // Status condition messages
    public const string TARGET_PARALYZED = "[POKEMON]'s\nparalyzed! It may\nnot attack!";
    public const string USER_FULLY_PARALYZED = "[POKEMON]'s\nfully paralyzed!";
    public const string TARGET_SLEPT = "[POKEMON]\nfell asleep!";
    public const string TARGET_ALREADY_SLEEPING = "[POKEMON]'s\nalready asleep!";
    public const string USER_SLEEPING = "[POKEMON]\nis fast asleep!";
    public const string USER_WOKE_UP = "[POKEMON]\nwoke up!";
    public const string TARGET_BURNED = "[POKEMON]\nwas burned!";
    public const string TARGET_FROZEN = "[POKEMON]\nwas frozen solid!";
    public const string USER_FROZEN = "[POKEMON]\nis frozen solid!";
    public const string POKEMON_DEFROSTED = "Fire defrosted\n[POKEMON]!";
    public const string TARGET_POISONED = "[POKEMON]\nwas poisoned!";
    public const string TARGET_BADLY_POISONED = "[POKEMON]'s\nbadly poisoned!";
    public const string TARGET_CONFUSED = "[POKEMON]\nbecame confused!";
    public const string TARGET_SEEDED = "[POKEMON]\nwas seeded!";
    public const string TARGET_DISABLED = "[POKEMON]'s\n[MOVE] was\ndisabled!";
    public const string USER_NO_LONGER_DISABLED = "[POKEMON]'s\ndisabled no more!";
    public const string USER_FOCUSED = "[POKEMON]'s\ngetting pumped!";
    public const string USER_RESTED = "[POKEMON]\nstarted sleeping!";
    public const string TARGET_EVADED = "[POKEMON]\nevaded attack!";
    public const string STATUS_DID_NOT_AFFECT = "It didn't affect\n[POKEMON]!";

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

    public const string PLAYER_SEND_OUT_1 = "GO!\n[POKEMON]!";
    public const string PLAYER_RETURN = "[POKEMON] enough!\nCome back!";
    public const string GAINED_EXP = "[POKEMON] gained\n[VALUE] EXP. Points!";

    public const string BATTLE_START_WILD = "Wild [POKEMON]\nappeared!";
    public const string BATTLE_START_TRAINER = "[TRAINER] wants\nto fight!";
    public const string TRAINER_SENT_OUT_POKEMON = "[TRAINER] sent\nout [POKEMON]!";

    public const string NO_RUNNING = "No! There's no\nrunning from a\ntrainer battle!";
    public const string NO_PP = "No PP left for\nthis move!";
    public const string NO_WILL_TO_FIGHT = "There's no will\nto fight!";
    public const string ALREADY_OUT = "[POKEMON] is\nalready out!";

    public const string MOVE_DISABLED = "[MOVE] is\ndisabled!";
    public const string CANNOT_ESCAPE = "Couldn't escape!";
    public const string GOT_AWAY_SAFELY = "Got away safely!";
    public const string USE_NEXT_POKEMON = "Use next\nPOKéMON?";
    public const string USER_FLINCHED = "[POKEMON]\nflinched!";
    public const string POKEMON_FAINTED = "[POKEMON]\nfainted!";
    public const string OUT_OF_POKEMON = "[PLAYER] is out of\nuseable POKéMON!";
    public const string BLACKED_OUT = "[PLAYER] blacked\nout!";
    public const string POKEMON_LEVEL_UP = "[POKEMON] grew\nto level [VALUE]!";

    public static IEnumerator Display(string text, Trainer trainer = null, BattlePokemon bPokemon = null, Pokemon pokemon = null,
        BaseMove move = null, int value = 0, StatType stat = StatType.NONE, bool replaceName = true, bool waitForInput = true)
    {
        if (bPokemon != null)
        {
            string pokemonName = bPokemon.Name; 
            if(replaceName && !bPokemon.TrainerIsPlayer)
                pokemonName = $"Enemy {bPokemon.Name}";

            text = text.Replace(POKEMON, pokemonName);
        }

        if (pokemon != null)
            text = text.Replace(POKEMON, pokemon.Nickname);

        if (move != null)
            text = text.Replace(MOVE, move.Name);

        if (value > 0)
            text = text.Replace(VALUE, value.ToString());

        if (stat != StatType.NONE)
        {
            string statName = stat.ToString();
            text = text.Replace(STAT, statName);
        }

        if (trainer != null)
            text = text.Replace(TRAINER, trainer.Name);

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