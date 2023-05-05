using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReflexiveStatusMove : BaseMove
{
    protected ReflexiveStatusEffect Effect = ReflexiveStatusEffect.None;
    protected bool greatlyRaiseStat = false;

    /// <summary>
    /// Creates a status move that affects only the user.
    /// </summary>
    /// <param name="name">The name of this move.</param>
    /// <param name="type">The type of this move. (Fire, Water, etc)</param>
    /// <param name="basePP">The base Power Points of this move.</param>
    /// <param name="battle">A reference to the battle this move is in.</param>
    protected ReflexiveStatusMove(string name, Type type, int basePP, BattleStateManager battle)
        : base(name, type, Category.Status, 0, basePP, battle) { }

    // Executes the user's move
    protected virtual IEnumerator Execute(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        yield return Battle.StartCoroutine(ApplyReflexiveEffect(user));

        SetLastMoveUsed(user);
        CurrentPP--;
    }

    /* Passes execution to the single parameter Execute method
     * as most reflexive moves only need a reference to the user.*/
    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        if (Effect == ReflexiveStatusEffect.Teleport)
            yield return Battle.StartCoroutine(Teleport(user, opponent));
        else
            yield return Battle.StartCoroutine(Execute(user));
    }

    public IEnumerator Teleport(BattlePokemon user, BattlePokemon opponent)
    {
        // Battle ending moves can only succeed in wild battles
        if (Battle.BattleType != BattleType.WILD_BATTLE)
        {
            yield return Battle.StartCoroutine(OnFailed());
            SetLastMoveUsed(user);
            CurrentPP--;
            yield break;
        }

        bool success = false;
        /* These moves have a chance to fail
         * if the user is at a lower level than the opponent */
        if (user.Level < opponent.Level)
        {
            int numerator = opponent.Level / 4;
            float denominator = opponent.Level + user.Level + 1;
            float failureChance = numerator / denominator;
            float check = Random.Range(0f, 1f);
            if (check < failureChance)
                yield return Battle.StartCoroutine(OnFailed());
        }
        else
        {
            success = true;
            yield return Battle.StartCoroutine(OnTeleported(opponent));
        }

        SetLastMoveUsed(user);
        CurrentPP--;

        if (success)
            yield return Battle.StartCoroutine(Battle.CloseBattle());
    }

    private IEnumerator ApplyReflexiveEffect(BattlePokemon user)
    {
        switch (Effect)
        {
            case ReflexiveStatusEffect.RaiseAttack:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Attack));
                yield break;
            case ReflexiveStatusEffect.RaiseDefense:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Defense));
                yield break;
            case ReflexiveStatusEffect.RaiseSpecial:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Special));
                yield break;
            case ReflexiveStatusEffect.RaiseSpeed:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Speed));
                yield break;
            case ReflexiveStatusEffect.RaiseEvasion:
                yield return Battle.StartCoroutine(RaiseStatAttempt(user, StatType.Evasion));
                yield break;
            case ReflexiveStatusEffect.Reflect:
                user.IsReflectActive = true;
                yield return Battle.StartCoroutine(OnReflect(user));
                yield break;
            case ReflexiveStatusEffect.LightScreen:
                user.IsLightScreenActive = true;
                yield return Battle.StartCoroutine(OnLightScreen(user));
                yield break;
            case ReflexiveStatusEffect.Rest:
                if (user.CurrentHP == user.Stats.HP)
                    yield return Battle.StartCoroutine(OnFailed());
                else
                {
                    yield return Battle.StartCoroutine(OnRest(user));
                    yield return Battle.StartCoroutine(user.RestoreHealth(user.Stats.HP));
                    user.Sleep();
                    yield return Battle.StartCoroutine(OnRegainedHealth(user));
                }
                yield break;
            case ReflexiveStatusEffect.RestoreHealth:
                if (user.CurrentHP == user.Stats.HP)
                    yield return Battle.StartCoroutine(OnFailed());
                else
                {
                    yield return Battle.StartCoroutine(user.RestoreHealth(user.Stats.HP / 2));
                    yield return Battle.StartCoroutine(OnRegainedHealth(user));
                }
                yield break;
            case ReflexiveStatusEffect.Focus:
                user.Focused = true;
                yield return Battle.StartCoroutine(OnFocused(user));
                yield break;
            case ReflexiveStatusEffect.Mist:
                if (user.IsMistActive)
                    yield return Battle.StartCoroutine(OnFailed());
                else
                {
                    user.IsMistActive = true;
                    yield return Battle.StartCoroutine(OnMist(user));
                }
                yield break;
        }
    }

    private IEnumerator RaiseStatAttempt(BattlePokemon user, StatType stat)
    {
        if(user.CanStatBeRaised(stat))
        {
            int statChange = greatlyRaiseStat ? 2 : 1;
            user.ModifyStatAsPrimary(stat, statChange);
            yield return Battle.StartCoroutine(OnRaisedStat(user, stat, greatlyRaiseStat));
        }
        else
            yield return Battle.StartCoroutine(OnFailed());
    }

    private IEnumerator OnReflect(BattlePokemon user)
    {
        string userName = user == Battle.PlayerSide.ActivePokemon ? user.Name : $"Enemy {user.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{userName}\ngained armor!", false));
        yield return new WaitForSeconds(6 / 60f);
    }

    private IEnumerator OnLightScreen(BattlePokemon user)
    {
        string userName = user == Battle.PlayerSide.ActivePokemon ? user.Name : $"Enemy {user.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{userName}<\nprotected against\nspecial attacks!", false));
        yield return new WaitForSeconds(6 / 60f);
    }

    private IEnumerator OnRegainedHealth(BattlePokemon user)
    {
        string userName = user == Battle.PlayerSide.ActivePokemon ? user.Name : $"Enemy {user.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{userName}\nregained health!", false));
        yield return new WaitForSeconds(6 / 60f);
    }

    private IEnumerator OnRest(BattlePokemon user)
    {
        string userName = user == Battle.PlayerSide.ActivePokemon ? user.Name : $"Enemy {user.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{userName}\nstarted sleeping!", false));
        yield return new WaitForSeconds(6 / 60f);
    }

    private IEnumerator OnFocused(BattlePokemon user)
    {
        string userName = user == Battle.PlayerSide.ActivePokemon ? user.Name : $"Enemy {user.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{userName}<\ngetting pumped!", false));
        yield return new WaitForSeconds(6 / 60f);
    }

    private IEnumerator OnMist(BattlePokemon user)
    {
        string userName = user == Battle.PlayerSide.ActivePokemon ? user.Name : $"Enemy {user.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{userName}<\nshrouded in mist!", false));
        yield return new WaitForSeconds(6 / 60f);
    }

    private IEnumerator OnTeleported(BattlePokemon user)
    {
        string userName = user == Battle.PlayerSide.ActivePokemon ? user.Name : $"Enemy {user.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"{userName}\nran from battle!", false));
        yield return new WaitForSeconds(6 / 60f);
    }
}

public enum ReflexiveStatusEffect
{
    None,
    RaiseAttack,
    RaiseDefense,
    RaiseSpecial,
    RaiseSpeed,
    RaiseEvasion,
    Reflect,
    LightScreen,
    Rest,
    RestoreHealth,
    Focus,
    Mist,
    Teleport
}