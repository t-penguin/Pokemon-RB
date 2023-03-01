using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class BattlePokemon
{
    #region Properties

    // General Info
    public bool TrainerIsPlayer;
    public Pokemon ReferencePokemon { get; }
    [field: SerializeField] public string Name { get; }
    [field: SerializeField] public int PokedexNumber { get; }
    [field: SerializeField] public Type Primary { get; }
    [field: SerializeField] public Type Secondary { get; }
    [field: SerializeField] public StatusEffect Status { get; private set; }
    [field: SerializeField] public BaseMove[] Moves { get; private set; }

    // Stat Info
    [field: SerializeField] public int Level { get; }
    [field: SerializeField] public Stats Stats { get; }
    [field: SerializeField] public StatStageModifiers StatModifiers { get; }
    [field: SerializeField] public Stats BattleStats { get; private set; }
    [field: SerializeField] public int CurrentHP { get; private set; }
    [field: SerializeField] public float Accuracy { get; private set; }
    [field: SerializeField] public float Evasion { get; private set; }

    // Battle Info
    [field: SerializeField] public BaseMove LastMoveUsed { get; private set; }
    [field: SerializeField] public TransitiveMove MirrorMove { get; private set; }
    [field: SerializeField] public bool HasSubstitute { get; private set; }
    [field: SerializeField] public bool IsMistActive { get; private set; }

    #endregion

    private BattleStateManager _battle;

    #region Constructors

    public BattlePokemon(BattleStateManager battle, Pokemon pokemon, bool isPlayerPokemon)
    {
        _battle = battle;

        TrainerIsPlayer = isPlayerPokemon;
        ReferencePokemon = pokemon;
        Name = pokemon.Nickname;
        PokedexNumber = pokemon.PokedexNumber;
        Primary = pokemon.PrimaryType;
        Secondary = pokemon.SecondaryType;
        Status = pokemon.Status;

        Moves = new BaseMove[4];
        for(int i = 0; i < 4; i++)
            Moves[i] = MoveCreator.CreateMove(battle, pokemon.MoveIndexes[i]);

        Level = pokemon.Level;
        Stats = pokemon.Stats;
        StatModifiers = new StatStageModifiers();
        BattleStats = new Stats(Stats);
        CurrentHP = pokemon.CurrentHP;
        Accuracy = 1f;
        Evasion = 1f;

        LastMoveUsed = null;
        MirrorMove = null;
    }

    #endregion

    public void SetLastMoveUsed(BaseMove move) => LastMoveUsed = move;
    public void SetMirrorMove(TransitiveMove move) => MirrorMove = move;

    public IEnumerator RecieveDamge(int damage, Type type)
    {
        if (damage > CurrentHP)
            damage = CurrentHP;

        ReferencePokemon.CurrentHP -= damage;

        if(TrainerIsPlayer)
        {
            float damageDone = 0;
            float remainingHP = CurrentHP;
            Vector3 scale = _battle.PlayerHPBar.localScale;
            while (damageDone < damage)
            {
                damageDone += 0.25f;
                remainingHP -= 0.25f;
                CurrentHP = (int)remainingHP;
                scale.x = remainingHP / Stats.HP;
                _battle.PlayerHPBar.localScale = scale;
                _battle.PlayerHPText.text = $"{CurrentHP,3}/{Stats.HP,3}";
                yield return new WaitForSeconds(2 / 60f);
            }
        }
        else
        {
            float damageDone = 0;
            float remainingHP = CurrentHP;
            Vector3 scale = _battle.OpponentHPBar.localScale;
            while (damageDone < damage)
            {
                damageDone += 0.25f;
                remainingHP -= 0.25f;
                CurrentHP = (int)remainingHP;
                scale.x = -remainingHP / Stats.HP;
                _battle.OpponentHPBar.localScale = scale;
                yield return new WaitForSeconds(2 / 60f);
            }
        }

        if (CurrentHP <= 0)
        {
            yield return _battle.StartCoroutine(Faint());
            yield break;
        }

        // If damage type is FIRE and pokemon is FROZEN, defrost
        yield return null;
    }

    public IEnumerator RestoreHealth(int health)
    {
        yield return null;
    }

    #region Stat Methods

    public bool CanStatBeLowered(StatType stat) => StatModifiers.CanGoLower(stat);
    public bool CanStatBeRaised(StatType stat) => StatModifiers.CanGoHigher(stat);
    public void ResetStatModifiers() => StatModifiers.ResetStages();

    // Modifies a stat as a move's primary effect
    public void ModifyStatAsPrimary(StatType stat, int change)
    {
        if (change < 0 && (HasSubstitute || IsMistActive))
            return;

        StatModifiers.ModifyStatStage(stat, change);
    }

    // Modifies a stat as a move's secondary effect
    public void ModifyStatAsSecondary(StatType stat, int change)
    {
        if (change < 0 && HasSubstitute)
            return;

        StatModifiers.ModifyStatStage(stat, change);
    }



    private void UpdateBattleStat(StatType stat)
    {
        // Update modified stat
        switch (stat)
        {
            default:
                return;
            case StatType.Attack:
                BattleStats.Attack = Mathf.Clamp((int)(Stats.Attack * StatModifiers.GetStageMultiplier(StatType.Attack)), 1, 999);
                break;
            case StatType.Defense:
                BattleStats.Defense = Mathf.Clamp((int)(Stats.Defense * StatModifiers.GetStageMultiplier(StatType.Defense)), 1, 999);
                break;
            case StatType.Special:
                BattleStats.Special = Mathf.Clamp((int)(Stats.Attack * StatModifiers.GetStageMultiplier(StatType.Special)), 1, 999);
                break;
            case StatType.Speed:
                BattleStats.Speed = Mathf.Clamp((int)(Stats.Speed * StatModifiers.GetStageMultiplier(StatType.Speed)), 1, 999);
                break;
        }

        ApplyBadgeBoosts();
    }

    private void ApplyBadgeBoosts()
    {
        if (!TrainerIsPlayer) return;

        // Boulder Badge Boost
        if (PlayerData.BadgesObtained[0])
            BattleStats.Attack = Mathf.Clamp(BattleStats.Attack * 9 / 8, 1, 999);
        // Thunder Badge Boost
        if (PlayerData.BadgesObtained[2])
            BattleStats.Defense = Mathf.Clamp(BattleStats.Defense * 9 / 8, 1, 999);
        // Soul Badge Boost
        if (PlayerData.BadgesObtained[4])
            BattleStats.Speed = Mathf.Clamp(BattleStats.Speed * 9 / 8, 1, 999);
        // Volcano Badge Boost
        if (PlayerData.BadgesObtained[6])
            BattleStats.Special = Mathf.Clamp(BattleStats.Special * 9 / 8, 1, 999);
    }

    #endregion

    #region Status Methods

    public IEnumerator Faint()
    {
        yield return new WaitForSeconds(10 / 60f);
        Status = StatusEffect.FNT;
        ReferencePokemon.Status = Status;
        string text;
        text = TrainerIsPlayer ? $"{Name}\nfainted!" : $"Enemy {Name}\nfainted!";

        yield return _battle.StartCoroutine(_battle.FaintAnimation(TrainerIsPlayer));
        yield return new WaitForSeconds(10 / 60f);
        yield return _battle.StartCoroutine(_battle.DisplayMessage(text, true));
    }
    public void Burn()
    {
        if (HasSubstitute)
            return;


    }

    #endregion
}
