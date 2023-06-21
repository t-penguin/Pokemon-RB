using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattlePokemon
{
    #region Properties

    // General Info
    [field: SerializeField] public bool TrainerIsPlayer { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int PokedexNumber { get; private set; }
    [field: SerializeField] public Type Primary { get; set; }
    [field: SerializeField] public Type Secondary { get; set; }
    [field: SerializeField] public StatusEffect Status { get; private set; }
    [field: SerializeReference] public BaseMove[] Moves { get; private set; }
    public Pokemon ReferencePokemon { get; private set; }

    // Stat Info
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public Stats Stats { get; private set; }
    [field: SerializeField] public StatStageModifiers StatModifiers { get; private set; }
    [field: SerializeField] public Stats BattleStats { get; private set; }
    [field: SerializeField] public int CurrentHP { get; private set; }
    [field: SerializeField] public float Accuracy { get; private set; }
    [field: SerializeField] public float Evasion { get; private set; }

    // Battle Info
    [field: SerializeField] public bool IsBideActive { get; set; }
    [field: SerializeField] public int BideDamage { get; set; }
    [field: SerializeField] public int LastDamageRecieved { get; set; }
    [field: SerializeField] public bool HasSubstitute { get; private set; }
    [field: SerializeField] public int SubstituteHP { get; private set; }
    [field: SerializeField] public bool IsMistActive { get; set; }
    [field: SerializeField] public bool IsReflectActive { get; set; }
    [field: SerializeField] public bool IsLightScreenActive { get; set; }
    [field: SerializeField] public bool IsSemiInvulnerable { get; set; }
    [field: SerializeField] public bool BadlyPoisoned { get; set; }
    [field: SerializeField] public int ToxicCounter { get; private set; }
    [field: SerializeField] public int SleepCounter { get; set; }
    [field: SerializeReference] public BaseMove LastMoveUsed { get; private set; }
    [field: SerializeReference] public TransitiveMove MirrorMove { get; private set; }

    // Non-Volatile Status Conditions
    public bool AfflictedByStatus { get { return Alive && Status != StatusEffect.OK; } }
    public bool Alive { get { return Status != StatusEffect.FNT; } }
    public bool Asleep { get { return Status == StatusEffect.SLP; } }
    public bool Burned { get { return Status == StatusEffect.BRN; } }
    public bool Frozen { get { return Status == StatusEffect.FRZ; } }
    public bool Paralyzed { get { return Status == StatusEffect.PAR; } }
    public bool Poisoned { get { return Status == StatusEffect.PSN; } }


    // Volatile Status Conditions
    [field: SerializeField] public bool Focused { get; set; }
    [field: SerializeField] public bool Bound { get; set; }
    [field: SerializeField] public bool Confused { get; set; }
    [field: SerializeField] public int ConfusionTimer { get; private set; }
    [field: SerializeField] public bool Flinched { get; set; }
    [field: SerializeField] public bool Seeded { get; set; }
    [field: SerializeField] public bool Disabled { get; private set; }
    [field: SerializeField] public int DisableIndex { get; private set; }
    [field: SerializeField] public int DisableCounter { get; private set; }
    [field: SerializeField] public bool Recharging { get; set; }

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
        for (int i = 0; i < 4; i++)
        {
            Moves[i] = MoveCreator.CreateMove(battle, pokemon.MoveIndexes[i]);
            if(Moves[i] != null)
                Moves[i].SetPP(pokemon.MovePPs[i], pokemon.MoveMaxPPs[i]);
        }

        Level = pokemon.Level;
        Stats = pokemon.Stats;
        StatModifiers = new StatStageModifiers();
        BattleStats = new Stats(Stats);
        CurrentHP = pokemon.CurrentHP;
        Accuracy = 1f;
        Evasion = 1f;

        LastMoveUsed = null;
        MirrorMove = null;

        SleepCounter = pokemon.SleepCounter;
    }

    #endregion

    public bool IsType(Type type) => Primary == type || Secondary == type;

    public void SetLastMoveUsed(BaseMove move) => LastMoveUsed = move;
    public void SetMirrorMove(TransitiveMove move) => MirrorMove = move;
    public void ClearMirrorMove() => MirrorMove = null;

    public IEnumerator RecieveDamge(int damage, Type type = Type.NONE)
    {
        if (damage > CurrentHP)
            damage = CurrentHP;

        ReferencePokemon.CurrentHP -= damage;

        float damageDone = 0;
        float remainingHP = CurrentHP;
        Vector3 scale = TrainerIsPlayer ? _battle.PlayerHPBar.localScale : _battle.OpponentHPBar.localScale;

        while (damageDone < damage)
        {
            damageDone += 0.25f;
            remainingHP -= 0.25f;
            CurrentHP = (int)remainingHP;
            scale.x = remainingHP / Stats.HP;
            if (TrainerIsPlayer)
            {
                _battle.PlayerHPBar.localScale = scale;
                _battle.PlayerHPText.text = $"{CurrentHP,3}/{Stats.HP,3}";
            }
            else
            {
                scale.x *= -1;
                _battle.OpponentHPBar.localScale = scale;
            }
            yield return new WaitForSeconds(2 / 60f);
        }

        if (CurrentHP <= 0)
        {
            yield return _battle.StartCoroutine(Faint());
            yield break;
        }

        // Defrost the pokemon if it is frozen and a fire type move is used on it
        if(type == Type.FIRE && Frozen)
        {
            yield return _battle.StartCoroutine(BattleMessages.Display(BattleMessages.POKEMON_DEFROSTED, bPokemon: this));
            ClearNonVolatileStatus();
        }
    }

    public IEnumerator RestoreHealth(int health)
    {
        if(CurrentHP >= Stats.HP)
        {
            CurrentHP = Stats.HP;
            yield break;
        }

        int missingHealth = Stats.HP - CurrentHP;
        if (health > missingHealth)
            health = missingHealth;

        ReferencePokemon.CurrentHP += health;

        float healthRestored = 0;
        float remainingHP = CurrentHP;
        Vector3 scale = TrainerIsPlayer ? _battle.PlayerHPBar.localScale : _battle.OpponentHPBar.localScale;

        while (healthRestored < health)
        {
            healthRestored += 0.25f;
            remainingHP += 0.25f;
            CurrentHP = (int)remainingHP;
            scale.x = remainingHP / Stats.HP;
            if(TrainerIsPlayer)
            {
                _battle.PlayerHPBar.localScale = scale;
                _battle.PlayerHPText.text = $"{CurrentHP,3}/{Stats.HP,3}";
            }
            else
            {
                scale.x *= -1;
                _battle.OpponentHPBar.localScale = scale;
            }
            yield return new WaitForSeconds(2 / 60f);
        }
    }

    public IEnumerator Explode()
    {
        CurrentHP = 0;
        ReferencePokemon.CurrentHP = 0;
        RectTransform healthBar = TrainerIsPlayer ? _battle.PlayerHPBar : _battle.OpponentHPBar;
        Vector3 scale = healthBar.localScale;
        scale.x = 0;
        healthBar.localScale = scale;

        if(TrainerIsPlayer)
        {
            _battle.PlayerHPText.text = $"{CurrentHP,3}/{Stats.HP,3}";
        }

        yield return new WaitForSeconds(10 / 60f);

        yield return _battle.StartCoroutine(Faint());
    }

    public List<int> GetMovesWithPP()
    {
        List<int> indexes = new List<int>(4);
        for (int i = 0; i < 4; i++)
        {
            if (Moves[i] != null && Moves[i].CurrentPP > 0)
                indexes.Add(i);
        }

        return indexes;
    }

    public bool HasUsableMove()
    {
        List<int> indexesWithPP = GetMovesWithPP();
        bool noMovesWithPP = indexesWithPP.Count == 0;
        bool lastMoveIsDisabled = indexesWithPP.Count == 1 && Disabled && indexesWithPP[0] == DisableIndex;

        return !noMovesWithPP && !lastMoveIsDisabled;
    }

    public void SetReferencePP()
    {
        for (int i = 0; i < 4; i++)
        {
            if(Moves[i] == null)
            {
                ReferencePokemon.MovePPs[i] = 0;
                ReferencePokemon.MoveMaxPPs[i] = 0;
            }
            else
            {
                ReferencePokemon.MovePPs[i] = Moves[i].CurrentPP;
                ReferencePokemon.MoveMaxPPs[i] = Moves[i].CurrentMaxPP;
            }
        }
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
        UpdateBattleStat(stat);
    }

    // Modifies a stat as a move's secondary effect
    public void ModifyStatAsSecondary(StatType stat, int change)
    {
        if (change < 0 && HasSubstitute)
            return;

        StatModifiers.ModifyStatStage(stat, change);
        UpdateBattleStat(stat);
    }

    private void UpdateBattleStat(StatType stat)
    {
        // Update modified stat
        switch (stat)
        {
            default:
                return;
            case StatType.ATTACK:
                BattleStats.Attack = Mathf.Clamp((int)(Stats.Attack * StatModifiers.GetStageMultiplier(StatType.ATTACK)), 1, 999);
                break;
            case StatType.DEFENSE:
                BattleStats.Defense = Mathf.Clamp((int)(Stats.Defense * StatModifiers.GetStageMultiplier(StatType.DEFENSE)), 1, 999);
                break;
            case StatType.SPECIAL:
                BattleStats.Special = Mathf.Clamp((int)(Stats.Attack * StatModifiers.GetStageMultiplier(StatType.SPECIAL)), 1, 999);
                break;
            case StatType.SPEED:
                BattleStats.Speed = Mathf.Clamp((int)(Stats.Speed * StatModifiers.GetStageMultiplier(StatType.SPEED)), 1, 999);
                break;
            case StatType.ACCURACY:
                Accuracy = StatModifiers.GetStageMultiplier(StatType.ACCURACY);
                break;
            case StatType.EVASION:
                Evasion = StatModifiers.GetStageMultiplier(StatType.EVASION);
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

        yield return _battle.StartCoroutine(_battle.FaintAnimation(TrainerIsPlayer));
        yield return new WaitForSeconds(10 / 60f);
    }

    public void Flinch() => Flinched = true;
    public void Confuse()
    {
        Confused = true;
        ConfusionTimer = Random.Range(1, 5);
    }

    public void DisableMove(int index)
    {
        Disabled = true;
        DisableIndex = index;
        DisableCounter = Random.Range(1, 9);
    }

    public void ReduceDisableCounter()
    {
        DisableCounter--;
        if (DisableCounter == 0)
            ClearDisable();
    }

    public void ClearDisable()
    {
        Disabled = false;
        DisableIndex = -1;
        DisableCounter = 0;
    }

    public void Freeze()
    {
        Status = StatusEffect.FRZ;
        ReferencePokemon.Status = StatusEffect.FRZ;
        _battle.OnPokemonStatusChanged(this);
    }

    public void Burn()
    {
        Status = StatusEffect.BRN;
        ReferencePokemon.Status = StatusEffect.BRN;
        _battle.OnPokemonStatusChanged(this);
    }

    public void Paralyze()
    {
        Status = StatusEffect.PAR;
        ReferencePokemon.Status = StatusEffect.PAR;
        _battle.OnPokemonStatusChanged(this);
    }

    public void Sleep(int turns)
    {
        SleepCounter = turns;
        Status = StatusEffect.SLP;
        ReferencePokemon.Status = StatusEffect.SLP;
        _battle.OnPokemonStatusChanged(this);
    }
    public void DecreaseSleepCounter()
    {
        if (SleepCounter > 0)
        {
            SleepCounter--;
            ReferencePokemon.SleepCounter--;
        }
    }

    public void Poison()
    {
        Status = StatusEffect.PSN;
        ReferencePokemon.Status = StatusEffect.PSN;
        _battle.OnPokemonStatusChanged(this);
    }

    public void BadlyPoison()
    {
        Poison();
        BadlyPoisoned = true;
        ToxicCounter = 1;
    }
    public void IncreaseToxicCounter()
    {
        if (ToxicCounter < 15)
            ToxicCounter++;
    }

    public void ClearNonVolatileStatus()
    {
        if (AfflictedByStatus)
        {
            Status = StatusEffect.OK;
            ReferencePokemon.Status = StatusEffect.OK;
            _battle.OnPokemonStatusChanged(this);
        }
    }

    #endregion
}