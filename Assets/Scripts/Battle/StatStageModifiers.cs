using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StatStageModifiers
{
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Special { get; private set; }
    public int Speed { get; private set; }
    public int Accuracy { get; private set; }
    public int Evasion { get; private set; }

    public StatStageModifiers() { }

    public StatStageModifiers(int atk, int def, int spc, int spd, int acc, int eva)
    {
        Attack = atk;
        Defense = def;
        Special = spc;
        Speed = spd;
        Accuracy = acc;
        Evasion = eva;
    }

    // Returns whether the given stat can have its stage raised
    public bool CanGoHigher(StatType stat) => GetStage(stat) < 6;

    // Returns whether the given stat can have its stage lowered
    public bool CanGoLower(StatType stat) => GetStage(stat) > -6;

    // Modifies the given stat's stage by the given amount. Keeps it between -6 and 6.
    public void ModifyStatStage(StatType stat, int change)
    {
        int stage = GetStage(stat);
        stage = Mathf.Clamp(stage + change, -6, 6);
        SetStage(stat, stage);
    }

    // Returns the multiplier for a stat given its stage
    public float GetStageMultiplier(StatType type)
    {
        float stage = GetStage(type);

        // Negate the evasion stage for intended effects
        if (type == StatType.Evasion) stage = -stage;

        return (stage >= 0) ? (2 + stage) / 2 : 2 / (2 - stage);
    }

    // Resets all the stages to zero
    public void ResetStages()
    {
        Attack = 0;
        Defense = 0;
        Special = 0;
        Speed = 0;
        Accuracy = 0;
        Evasion = 0;
    }

    // Returns the stat stage of the given stat [-6, 6]
    private int GetStage(StatType stat)
    {
        switch (stat)
        {
            default: return 0;
            case StatType.Attack:   return Attack;
            case StatType.Defense:  return Defense;
            case StatType.Special:  return Special;
            case StatType.Speed:    return Speed;
            case StatType.Accuracy: return Accuracy;
            case StatType.Evasion:  return Evasion;
        }
    }

    // Sets the stat stage of the given stat to the provided value
    private void SetStage(StatType stat, int value)
    {
        value = Mathf.Clamp(value, -6, 6);

        switch (stat)
        {
            case StatType.Attack:
                Attack = value;
                break;
            case StatType.Defense:
                Defense = value;
                break;
            case StatType.Special:
                Special = value;
                break;
            case StatType.Speed:
                Speed = value;
                break;
            case StatType.Accuracy:
                Accuracy = value;
                break;
            case StatType.Evasion:
                Evasion = value;
                break;
        }
    }
}

public enum StatType
{
    Attack,
    Defense,
    Special,
    Speed,
    Accuracy,
    Evasion
}