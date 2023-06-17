using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class BattleStateManager : StateManager, IGameState
{
    #region Events

    public static event Action<WildArea> WildBattleStart;
    public static event Action<Trainer> TrainerBattleStart;
    public static event Action<Trainer> LinkBattleStart;
    public static event Action BattleStarted;
    public static event Action BattleEnded;

    public static event Action<BattleStateManager> OpenedPokemon;
    public static event Action<Pokemon> SwappedPokemon;
    public static event Action<BattleStateManager, Bag> OpenedBag;

    public static void StartWildBattle(WildArea area) => WildBattleStart?.Invoke(area);
    public static void StartTrainerBattle(Trainer trainer) => TrainerBattleStart?.Invoke(trainer);
    public static void StartLinkBattle(Trainer opponent) => LinkBattleStart?.Invoke(opponent);
    public static void StartBattle() => BattleStarted?.Invoke();

    public static void OpenPokemonMenu(BattleStateManager battle) => OpenedPokemon?.Invoke(battle);
    public static void SwapPokemon(Pokemon pokemon) => SwappedPokemon?.Invoke(pokemon);
    public static void OpenItemMenu(BattleStateManager battle, Bag bag) => OpenedBag?.Invoke(battle, bag);

    #endregion

    BattleBaseState _currentState;
    public StartState StartState = new StartState();
    public InputState InputState = new InputState();
    public TurnOrderState TurnOrderState = new TurnOrderState();
    public TurnExecutionState ExecutionState = new TurnExecutionState();

    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public Trainer Opponent { get; private set; }
    public BattleType BattleType { get; private set; }

    [field: SerializeField] public Tilemap Tilemap { get; private set; }
    [field: SerializeField] public Image BattleFlash { get; private set; }

    

    [Space(10)]
    [Header("Battle Properties")]
    public int TransitionID;
    [field: SerializeField] public GameObject BattleUI { get; private set; }
    [field: SerializeField] public GameObject Background { get; private set; }
    [field: SerializeField] public Image PlayerImage { get; private set; }
    [field: SerializeField] public Image[] PlayerTeamIcons { get; private set; }
    [field: SerializeField] public GameObject PlayerIconsFrame { get; private set; }
    [field: SerializeField] public GameObject PlayerInfoFrame { get; private set; }
    [field: SerializeField] public GameObject PlayerInfoBackground { get; private set; }
    [field: SerializeField] public TextMeshProUGUI PlayerName { get; private set; }
    [field: SerializeField] public TextMeshProUGUI PlayerLevel { get; private set; }
    [field: SerializeField] public RectTransform PlayerHPBar { get; private set; }
    [field: SerializeField] public TextMeshProUGUI PlayerHPText { get; private set; }

    [field: SerializeField] public Image OpponentImage { get; private set; }
    [field: SerializeField] public Image[] OpponentTeamIcons { get; private set; }
    [field: SerializeField] public GameObject OpponentIconsFrame { get; private set; }
    [field: SerializeField] public GameObject OpponentInfoFrame { get; private set; }
    [field: SerializeField] public TextMeshProUGUI OpponentName { get; private set; }
    [field: SerializeField] public TextMeshProUGUI OpponentLevel { get; private set; }
    [field: SerializeField] public RectTransform OpponentHPBar { get; private set; }

    [field: SerializeField] public GameObject SelectionBox { get; private set; }
    [field: SerializeField] public RectTransform SelectionArrow { get; private set; }

    [field: SerializeField] public GameObject MoveInfoBox { get; private set; }
    [field: SerializeField] public TextMeshProUGUI MoveInfoText { get; private set; }
    [field: SerializeField] public GameObject MovesBox { get; private set; }
    [field: SerializeField] public RectTransform MoveArrow { get; private set; }
    [field: SerializeField] public TextMeshProUGUI[] MoveNames { get; private set; }
    [field: SerializeField] public int MoveSelection { get; set; }

    [field: SerializeField] public BattleSide PlayerSide { get; private set; }
    [field: SerializeField] public BattleSide OpponentSide { get; private set; }
    [field: SerializeField] public List<Pokemon> Participants { get; private set; }
    public BattleSide FirstSide { get; set; }
    public BattleSide SecondSide { get; set; }
    public int RunCounter { get; set; }
    public bool SuccessfulRun { get; set; }
    public bool Swap { get; set; }
    public bool ForcedSwap { get; set; }

    [Space(10)]
    [Header("Pokeball Icons")]
    public Sprite EmptyPokeball;
    public Sprite NormalPokeball;
    public Sprite StatusPokeball;
    public Sprite FaintedPokeball;

    #region Monobehaviour Callbacks

    // Start is called before the first frame update
    void Start()
    {
        PlayerSide = new BattleSide();
        PlayerSide.IsPlayerSide = true;

        OpponentSide = new BattleSide();
        OpponentSide.IsPlayerSide = false;

        Participants = new List<Pokemon>();
    }

    private void OnEnable()
    {
        WildBattleStart += OnWildBattleStart;
    }

    private void OnDisable()
    {
        WildBattleStart -= OnWildBattleStart;
    }

    #endregion

    public void SwitchState(BattleBaseState state)
    {
        if (_currentState != null)
            _currentState.ExitState();

        _currentState = state;
        _currentState.EnterState(this);
    }

    public void EndBattle()
    {
        _currentState = null;
        ExitState();
        BattleEnded?.Invoke();
    }

    // REMOVE
    public IEnumerator CloseBattle()
    {
        yield return new WaitForSeconds(2 / 60f);

        // Set move PP to persist after battle
        PlayerSide.ActivePokemon.SetReferencePP();

        Background.SetActive(false);
        PlayerInfoBackground.SetActive(false);
        PlayerImage.gameObject.SetActive(false);
        OpponentImage.gameObject.SetActive(false);
        PlayerIconsFrame.SetActive(false);
        OpponentInfoFrame.SetActive(false);
        //OpponentIconsFrame.SetActive(false);
        PlayerInfoFrame.SetActive(false);

        SelectionBox.SetActive(false);
        MessageBox.Close();

        EndBattle();
        StopAllCoroutines();
        MainMenu.CanOpen = true;
        GameStateManager.SwitchState(GameStateManager.GameManager);
    }

    public void SetOpponentInfo(BattlePokemon pokemon)
    {
        OpponentName.text = pokemon.Name;
        OpponentLevel.text = $"@{pokemon.Level}";
        float healthPercent = (float)pokemon.CurrentHP / pokemon.Stats.HP;
        OpponentHPBar.localScale = new Vector3(-healthPercent, 1, 1);
    }

    public void SetOpponentActivePokemon(Pokemon pokemon)
    {
        if (pokemon.CurrentHP > 0)
            OpponentSide.ActivePokemon = new BattlePokemon(this, pokemon, false);
        else
            Debug.LogError($"Could not set {pokemon.Nickname} as the opponents active Pokemon because it has no HP left!");
    }

    public void SetPlayerInfo(BattlePokemon pokemon)
    {
        PlayerName.text = pokemon.Name;
        PlayerLevel.text = $"@{pokemon.Level}";
        float healthPercent = (float)pokemon.CurrentHP / pokemon.Stats.HP;
        PlayerHPBar.localScale = new Vector3(healthPercent, 1, 1);
        PlayerHPText.text = $"{pokemon.CurrentHP, 3}/{pokemon.Stats.HP, 3}";
    }

    public void SetPlayerActivePokemon(Pokemon pokemon)
    {
        if (pokemon.CurrentHP > 0)
            PlayerSide.ActivePokemon = new BattlePokemon(this, pokemon, true);
        else
            Debug.LogError($"Could not set {pokemon.Nickname} as your active Pokemon because it has no HP left!");
    }

    public void SetIcons(Image[] icons, List<Pokemon> team)
    {
        for(int i = 0; i < 6; i++)
        {
            if (i < team.Count)
            {
                StatusEffect status = team[i].Status;
                if (status == StatusEffect.OK)
                    icons[i].sprite = NormalPokeball;
                else if (status == StatusEffect.FNT)
                    icons[i].sprite = FaintedPokeball;
                else
                    icons[i].sprite = StatusPokeball;
            }
            else
                icons[i].sprite = EmptyPokeball;
        }
    }

    public void SetMoveNames(BattlePokemon pokemon)
    {
        for (int i = 0; i < 4; i++)
        {
            if (pokemon.Moves[i] == null)
                MoveNames[i].text = "-";
            else
                MoveNames[i].text = pokemon.Moves[i].Name;
        }
    }

    public void SetMoveInfo(BaseMove move, bool disabled)
    {
        if (disabled)
            MoveInfoText.text = "\ndisabled!";
        else
        {
            string moveType = move.Type.ToString();
            string movePP = $"{move.CurrentPP,2}/{move.CurrentMaxPP,2}";
            MoveInfoText.text = $"TYPE/\n {moveType}\n    {movePP}";
        }
    }

    public IEnumerator SendOutPokemon(Pokemon pokemon)
    {
        if (!Participants.Contains(pokemon))
            Participants.Add(pokemon);

        MoveSelection = 0;
        SetPlayerActivePokemon(pokemon);

        yield return StartCoroutine(OnPlayerSentOutPokemon(PlayerSide.ActivePokemon));

        // Display player pokemon info
        yield return new WaitForSeconds(4 / 60f);
        SetPlayerInfo(PlayerSide.ActivePokemon);
        PlayerInfoFrame.SetActive(true);
        yield return new WaitForSeconds(54 / 60f);

        // IF BATTLE ANIMATIONS ON: Pokeball smoke animation


        // Pokemon sent out animation
        yield return StartCoroutine(SendOutAnimation(true));
        yield return new WaitForSeconds(44 / 60f);
        MessageBox.Clear();
        yield return new WaitForSeconds(8 / 60f);

        ForcedSwap = false;
    }

    public IEnumerator SendOutAnimation(bool playerSide)
    {
        int scaleMirror = playerSide ? 1 : -1;
        Vector3 scale = new Vector3(0, 0, 1);
        Vector3 position = playerSide ? new Vector3(40, -96) : new Vector3(124, -56);
        Image image = playerSide ? PlayerImage : OpponentImage;
        int dexNumber = playerSide ? PlayerSide.ActivePokemon.PokedexNumber : OpponentSide.ActivePokemon.PokedexNumber;
        int spriteIndex = playerSide ? 3 : 2;
        
        image.rectTransform.sizeDelta = new Vector2(56, 56);
        image.rectTransform.localScale = scale;
        image.rectTransform.localPosition = position;
        image.sprite = PokemonData.Sprites[dexNumber][spriteIndex];

        for(int i = 0; i < 12; i++)
        {
            scale.x = i / 11f * scaleMirror;
            scale.y = i / 11f;
            image.rectTransform.localScale = scale;

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    public IEnumerator ReturnAnimation(bool playerSide)
    {
        int scaleMirror = playerSide ? 1 : -1;
        Image image = playerSide ? PlayerImage : OpponentImage;
        Vector3 scale = image.rectTransform.localScale;

        for (int i = 11; i >= 0; i--)
        {
            scale.x = i / 11f * scaleMirror;
            scale.y = i / 11f;
            image.rectTransform.localScale = scale;

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    public IEnumerator FaintAnimation(bool playerSide)
    {
        PlayerInfoFrame.SetActive(!playerSide);
        RectTransform imageTransform = playerSide ? PlayerImage.rectTransform : OpponentImage.rectTransform;
        Vector3 position = imageTransform.localPosition;

        for (int i = 0; i <= 56; i += 4)
        {
            position.y -= 4;
            imageTransform.localPosition = position;
            yield return new WaitForSeconds(1 / 60f);
        }

        OpponentInfoFrame.SetActive(playerSide);
    }

    public IEnumerator SwapPlayerPokemon()
    {
        // Set move PP to persist between switches
        PlayerSide.ActivePokemon.SetReferencePP();
        
        yield return StartCoroutine(OnPlayerReturnedPokemon(PlayerSide.ActivePokemon));
        yield return new WaitForSeconds(60 / 60f);
        yield return StartCoroutine(ReturnAnimation(true));
        yield return StartCoroutine(SendOutPokemon(PlayerSide.SwitchTarget));
    }

    public IEnumerator SwapOpponentPokemon()
    {

        yield break;
    }

    public void OnPokemonStatusChanged(BattlePokemon pokemon)
    {
        TextMeshProUGUI text = pokemon.TrainerIsPlayer ? PlayerLevel : OpponentLevel;
        StatusEffect status = pokemon.ReferencePokemon.Status;

        if (status == StatusEffect.OK)
            text.text = $"@{pokemon.Level}";
        else
            text.text = $" {status}";
    }

    public IEnumerator ApplyExperience(Pokemon faintedPokemon)
    {
        /* If only one Pokemon participates it recieves full exp
         * If multiple Pokemon participate, they recieve an even split
         * If the EXP All is in the bag, participants recieve half of what
         *  they would and the rest gain the same amount divided by the total 
         *  number of Pokemon in the party. This results in experience loss
         *  if there are multiple participants or if the player has Pokemon that have fainted. */

        /* b * L / 7 * 1/s * a * t
         * b = exp yield of fainted pokemon
         * L = level of the fainted pokemon
         * s = non-fainted participants (x2 if EXP All is in bag)
         * a = 1.5 if the fainted pokemon is owned by a trainer, 1 if it's wild
         * t = 1 if the player is the original trainer, 1.5 otherwise (traded) */
        int yield = PokemonData.ExpYield[faintedPokemon.PokedexNumber];
        int level = faintedPokemon.Level;
        int participants = 0;
        foreach(Pokemon p in Participants)
        {
            if (p.Status != StatusEffect.FNT)
                participants++;
        }

        float trainerMultiplier = BattleType == BattleType.WILD_BATTLE ? 1 : 1.5f;
        int expGained = yield * level / 7;
        expGained = Mathf.FloorToInt(expGained / participants);
        expGained = Mathf.FloorToInt(expGained * trainerMultiplier);

        foreach (Pokemon p in Participants)
        {
            if(p.Status != StatusEffect.FNT)
            {
                float tradedMultiplier = PlayerData.ID == p.TrainerID ? 1 : 1.5f;
                expGained = Mathf.FloorToInt(expGained * tradedMultiplier);

                yield return StartCoroutine(OnGainedEXP(p, expGained));
                p.GainExperience(expGained);
            }
        }
    }

    // Starts a wild battle against a pokemon from the provided area
    private void OnWildBattleStart(WildArea area)
    {
        BattleType = BattleType.WILD_BATTLE;

        // Generate another random number between 0 and 256
        int encounterCheck = UnityEngine.Random.Range(0, 256);
        // Get this area's encounter slots
        int[,] encounterSlots = EncounterData.GetEncountersFromIndex(area.GetIndex(), area.IsRoute21Grass());

        // Determine which pokemon to fight
        int encounterIndex;
        if (encounterCheck < 3) encounterIndex = 9;
        else if (encounterCheck < 14) encounterIndex = 8;
        else if (encounterCheck < 27) encounterIndex = 7;
        else if (encounterCheck < 40) encounterIndex = 6;
        else if (encounterCheck < 65) encounterIndex = 5;
        else if (encounterCheck < 90) encounterIndex = 4;
        else if (encounterCheck < 115) encounterIndex = 3;
        else if (encounterCheck < 154) encounterIndex = 2;
        else if (encounterCheck < 205) encounterIndex = 1;
        else encounterIndex = 0;

        // Get the pokedex number and level of the determined Pokemon
        int dexNum = encounterSlots[encounterIndex, 0];
        int level = encounterSlots[encounterIndex, 1];

        // Reset the opponent and add the wild Pokemon to its team
        Opponent.Clear();
        Pokemon wildPokemon = new Pokemon(dexNum, level);
        Opponent.SetTrainerName($"Wild {wildPokemon.Nickname}");
        Opponent.SetTrainerIcon(PokemonData.Sprites[wildPokemon.PokedexNumber][2]);
        Opponent.AddToTeam(wildPokemon);

        OpponentImage.rectTransform.localScale = new Vector3(-1, 1, 1);

        // CHANGE HERE: Set transition ID based on criteria
        TransitionID = UnityEngine.Random.Range(0, 4);
        // Start the battle
        SwitchState(StartState);
    }


    #region State Manager Callbacks

    public override void OnNavigate(InputAction.CallbackContext context) { }

    public override void OnConfirm(InputAction.CallbackContext context) { }

    public override void OnCancel(InputAction.CallbackContext context) { }

    #endregion

    #region Messages

    private IEnumerator OnPlayerSentOutPokemon(BattlePokemon pokemon)
    {
        yield return StartCoroutine(BattleMessages.Display(BattleMessages.PLAYER_SEND_OUT_1, bPokemon: pokemon, waitForInput: false));
    }

    private IEnumerator OnPlayerReturnedPokemon(BattlePokemon pokemon)
    {
        yield return StartCoroutine(BattleMessages.Display(BattleMessages.PLAYER_RETURN, bPokemon: pokemon, waitForInput: false));
    }

    private IEnumerator OnGainedEXP(Pokemon pokemon, int exp)
    {
        yield return StartCoroutine(BattleMessages.Display(BattleMessages.GAINED_EXP, pokemon: pokemon, value: exp));
    }

    #endregion
}

[System.Serializable]
public class BattleSide
{
    public bool IsPlayerSide;
    public BattleAction Action;
    public BattlePokemon ActivePokemon;
    public Pokemon SwitchTarget;
    public BaseMove Move;
    public Item Item;
    
    public List<Pokemon> Team;
    public Bag Bag;

    public bool LockedIntoAction;
    public bool LockedIntoMove;

    public void SetMove(BaseMove move)
    {
        Action = BattleAction.UseMove;
        Move = move;
    }

    public void SetItem(Item item)
    {
        Action = BattleAction.UseItem;
        Item = item;
    }

    public void SetSwitch(Pokemon pokemon)
    {
        Action = BattleAction.SwitchPokemon;
        SwitchTarget = pokemon;
    }

    public void SetTeam(List<Pokemon> team) => Team = team;

    public void SetBag(Bag bag) => Bag = bag;

    public bool IsAbleToFight()
    {
        foreach (Pokemon pokemon in Team)
        {
            if (pokemon.Status != StatusEffect.FNT)
            {
                Debug.Log($"{pokemon.Nickname} is able to fight!");
                return true;
            }
        }

        Debug.Log("None of your Pokemon are able to fight!");
        return false;
    }
}

public enum BattleType
{
    WILD_BATTLE,
    TRAINER_BATTLE,
    LINK_BATTLE,
    OLD_MAN_BATTLE
}