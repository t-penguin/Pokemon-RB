using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMove
{
    public BattleStateManager Battle { get; }
    public string Name { get; }
    public Type Type { get; }
    public Category Category { get; }
    public int Priority { get; }
    public int BasePP { get; }
    public int CurrentPP { get; protected set; }
    public int CurrentMaxPP { get; protected set; }
    public int MaxPP { get; }

    private const string FAILED = "But it failed!";
    private const string NO_EFFECT = "It had no\neffect!";
    private const string NOTHING_HAPPENED = "Nothing happened!";

    /// <summary>
    /// Base class for a Pokemon's move.
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="category">The category of this move. (Physical, Special, Status)</param>
    /// <param name="priority">This move's priority. Higher priority goes first.</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="battle">A reference to the current battle this move is in.</param>
    protected BaseMove(string name, Type type, Category category, int priority, int basePP, BattleStateManager battle)
    {
        Battle = battle;

        Name = name;
        Type = type;
        Category = category;
        Priority = priority;
        BasePP = basePP;
        CurrentPP = BasePP;
        CurrentMaxPP = BasePP;
        MaxPP = (int)(BasePP * 1.6);
    }

    // Executes the user's move, to be overriden by each move
    public abstract IEnumerator Execute(BattlePokemon user, BattlePokemon opponent);

    // Sets the user's last used move to be this one
    protected void SetLastMoveUsed(BattlePokemon user) => user.SetLastMoveUsed(this);

    // Displays a message after certain battle events
    protected IEnumerator OnUsed(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{user.Name}\nused {Name}!", false));
        yield return new WaitForSeconds(6 / 60f);
    }
    protected IEnumerator OnMissed(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{user.Name}<\nattack missed!", true));
        yield return new WaitForSeconds(6 / 60f);
    }
    protected IEnumerator OnFailed()
    {
        yield return Battle.StartCoroutine(Battle.DisplayMessage(FAILED, true));
        yield return new WaitForSeconds(6 / 60f);
    }
    protected IEnumerator OnNoEffect()
    {
        yield return Battle.StartCoroutine(Battle.DisplayMessage(NO_EFFECT, true));
        yield return new WaitForSeconds(6 / 60f);
    }

    protected IEnumerator OnNothingHappened()
    {
        yield return Battle.StartCoroutine(Battle.DisplayMessage(NOTHING_HAPPENED, true));
        yield return new WaitForSeconds(6 / 60f);
    }

    protected IEnumerator OnLoweredStat(BattlePokemon target, StatType stat, bool greatlyLowered = false)
    {
        string targetName = target == Battle.PlayerSide.ActivePokemon ? target.Name : $"Enemy {target.Name}";
        string statName = stat.ToString().ToUpper();
        string greatlyText = greatlyLowered ? "\ngreatly" : string.Empty;
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{targetName}<\n{statName}{greatlyText} fell!", true));
        yield return new WaitForSeconds(6 / 60f);
    }

    protected IEnumerator OnRaisedStat(BattlePokemon target, StatType stat, bool greatlyRaised = false)
    {
        string targetName = target == Battle.PlayerSide.ActivePokemon ? target.Name : $"Enemy {target.Name}";
        string statName = stat.ToString().ToUpper();
        string greatlyText = greatlyRaised ? "\ngreatly" : string.Empty;
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{targetName}<\n{statName}{greatlyText} rose!", true));
        yield return new WaitForSeconds(6 / 60f);
    }
}