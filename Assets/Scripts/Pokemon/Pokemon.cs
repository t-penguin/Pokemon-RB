using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Pokemon
{
    #region Properties

    //General info for a Pokemon
    [field: SerializeField] public int PokedexNumber { get; private set; }
	[field: SerializeField] public string Nickname { get; private set; }
	[field: SerializeField] public Type PrimaryType { get; private set; }
	[field: SerializeField] public Type SecondaryType { get; private set; }
	[field: SerializeField] public string OriginalTrainer { get; private set; }
	[field: SerializeField] public int TrainerID { get; private set; }

	//Stat info for a Pokemon
	[field: SerializeField] public int Level { get; private set; }
	[field: SerializeField] public int TotalExperience { get; private set; }
	[field: SerializeField] public Stats IVs { get; private set; }
	[field: SerializeField] public Stats EVs { get; private set; }
	[field: SerializeField] public Stats Stats { get; private set; }
	[field: SerializeField] public int CurrentHP { get; set; }
	[field: SerializeField] public StatusEffect Status { get; set; }

	//Move info for a Pokemon
	[field: SerializeField] public int[] MoveIndexes { get; private set; }
	[field: SerializeField] public int[] MovePPs { get; private set; }
	public int[] MoveMaxPPs { get; private set; }

	#endregion

	#region Constructors

	//Creates an "empty" Pokemon
	public Pokemon()
    {
		PokedexNumber = 0;
		Level = 1;
    }

	//Creates a Pokemon with the specified Pokedex Number at the specified level
	public Pokemon(int dexNum, int level)
    {
		PokedexNumber = dexNum;
		MoveIndexes = new int[4];
		MovePPs = new int[4];
		MoveMaxPPs = new int[4];

		//Level up the Pokemon one by one, checking for possible moves to learn
		//If there is a move to learn, attempt to learn it
		//If all four slots are taken, randomly decide whether to replace a move and which one
		Level = 0;
		for(int i = 1; i <= level; i++)
        {
			Level++;

			if (PokemonLearnset.CanLearnMoveAtLevel(PokedexNumber, Level))
				LearnLevelUpMoves();
        }

		//Set the PP arrays to each moves Max PP
		for (int i = 0; i < 4; i++)
			MoveMaxPPs[i] = MoveData.MaxPPs[MoveIndexes[i]];
		MoveMaxPPs.CopyTo(MovePPs, 0);

		TotalExperience = ExpAtLevel(Level);
		SetInfo();
		SetStats();
	}

    #endregion

	#region Data Methods

	//Sets the general info for a Pokemon using it's Pokedex number
	private void SetInfo()
    {
		//A Pokemon's Nickname is set to it's species name unless later changed by the trainer
		Nickname = PokemonData.Names[PokedexNumber];
		PrimaryType = PokemonData.Types[PokedexNumber][0];
		SecondaryType = PokemonData.Types[PokedexNumber][1];

		//Original Trainer & Trainer ID should be set when a Pokemon is caught or given to the player
		OriginalTrainer = "NONE";
		TrainerID = 0;
    }

	private void SetStats()
    {
		//IVs are set randomly
		IVs = CreateRandomIVs();
		//EVs are set to 0 and are added to upon defeating a Pokemon
		EVs = new Stats(0, 0, 0, 0, 0);
		//Stats are set based on IVs, EVs, and base stats (which are located in the PokemonData class)
		Stats = CreateStats();
		CurrentHP = Stats.HP;
    }

	//Creates randomized IVs for the current Pokemon
	//IVs range from 0 - 15
	private Stats CreateRandomIVs()
    {
		//Attack, Defense, Speed, and Special IVs are random from 0 - 15
		int atk = Random.Range(0, 16);
		int def = Random.Range(0, 16);
		int spd = Random.Range(0, 16);
		int spcl = Random.Range(0, 16);

		//The HP IV is determined using the last binary digit of the other IVs
		//(Each IV can be represented by a 4 digit binary XXXX)
		int hp = 0;
		if (atk % 2 == 1)
			hp += 8;		//1XXX
		if (def % 2 == 1)
			hp += 4;        //X1XX
		if (spd % 2 == 1)
			hp += 2;        //XX1X
		if (spcl % 2 == 1)
			hp += 1;        //XXX1

		return new Stats(hp, atk, def, spd, spcl);
	}

	//Calculates a single stat's value given a base stat, the stat's IV and EV,
	//the Pokemon's level, and whether or not it's the HP stat
	private int CalculateStat(int baseStat, int IV, int EV, int level, bool isHPStat = false)
	{
		//Stat points are determined by taking the square root of the stat experience (EV)
		//and dividing by 4. This should be between 0 and 255
		int statPoint = Mathf.Clamp((int)Mathf.Sqrt(EV) / 4, 0, 255);

		//The HP stat is calculated slightly differently
		if (isHPStat)
			return ((baseStat + IV) * 2 + statPoint) * level / 100 + 5;

		return ((baseStat + IV) * 2 + statPoint) * level / 100 + level + 10;
	}

	//Returns the actual calculated stats for this Pokemon
	private Stats CreateStats()
    {
		//Grab the base stats for this Pokemon from the PokemonData class
		Stats BaseStats = PokemonData.BaseStats[PokedexNumber];
		
		return new Stats(
			CalculateStat(BaseStats.HP, IVs.HP, EVs.HP, Level, true),
			CalculateStat(BaseStats.Attack, IVs.Attack, EVs.Attack, Level),
			CalculateStat(BaseStats.Defense, IVs.Defense, EVs.Defense, Level),
			CalculateStat(BaseStats.Speed, IVs.Speed, EVs.Speed, Level),
			CalculateStat(BaseStats.Special, IVs.Special, EVs.Special, Level)
			);
    }

	//Returns the total experience this Pokemon would have at the current level
	//Experience is dependent on which group this Pokemon belongs to
	private int ExpAtLevel(int level)
    {
		if(level <= 1)
			return 0;
		
		switch (PokemonData.ExpGroups[PokedexNumber])
        {
			case ExperienceGroup.Fast:			// 0.8L^3
				return 4 * level * level * level / 5;
			case ExperienceGroup.MediumFast:	//L^3
				return level * level * level;
			case ExperienceGroup.MediumSlow:	// 1.2L^3 - 15L^2 + 100L - 140
				return (level * level * level * 6 / 5) - (15 * level * level) + (100 * level) - 140;
			case ExperienceGroup.Slow:			//1.25L^3
				return 5 * level * level * level / 4;
			default:
				return 0;
        }
    }

	public int ExpToNextLevel() => ExpAtLevel(Level + 1) - TotalExperience;

	public void GainExperience(int experience)
    {
		if (experience >= ExpToNextLevel())
			LevelUp();

		TotalExperience += experience;
    }

	public void LevelUp()
    {
		Level++;
		Debug.Log($"{Nickname} leveled up to level {Level}!");
    }

    #endregion

    #region Move Methods
	
	//Checks if the Pokemon already knows the specified move
	private bool IsMoveKnown(int moveIndex)
    {
		foreach(int index in MoveIndexes)
        {
			if (index == moveIndex)
				return true;
        }

		return false;
    }

	//Returns the number of moves this Pokemon knows
	public int GetNumberOfMoves()
    {
		int moves = 0;
		//Loop through the MoveIndexes array and increment the counter if the move index is not an empty move (index 0)
		foreach(int m in MoveIndexes)
        {
			if (m != 0)
				moves++;
        }

		return moves;
    }

	//Returns the index of a random move this Pokemon knows
	//Returns 0 if this Pokemon doesn't know any moves yet
	public int SelectRandomMove()
    {
		int m = GetNumberOfMoves();
		if (m == 0)
		{
			Debug.LogError($"{Nickname} doesn't know any moves! Something must've gone wrong...");
			return 0;
		}

		int r = Random.Range(0, m);
		return MoveIndexes[r];
    }

	//Attempt to teach this Pokemon any moves it can learn at the current level
	private void LearnLevelUpMoves()
    {
		//Get a list of moves this Pokemon can learn at thsi level
		List<int> learnableMoves = PokemonLearnset.GetAllMovesAtLevel(PokedexNumber, Level);

		//Loop through each move in the list and attempt to learn it
		foreach(int index in learnableMoves)
        {
			//Only attempt to learn the move if this Pokemon doesn't already know the move
			if(!IsMoveKnown(index))
            {
				//Find the first move slot that doesn't have a move set (indicated by a value of 0)
				//Replace a move if all move slots are filled
				switch (GetNumberOfMoves())
                {
					case 0:
						MoveIndexes[0] = index;
						break;
					case 1:
						MoveIndexes[1] = index;
						break;
					case 2:
						MoveIndexes[2] = index;
						break;
					case 3:
						MoveIndexes[3] = index;
						break;
					case 4: //All four moves slots are already filled
						//85% chance to replace a random move
						if(Random.Range(0, 100) < 85)
                        {
							//Pick a random slot and replace the move at that slot
							int slot = Random.Range(1, 5);
							MoveIndexes[slot] = index;
                        }
						break;

				}
            }
        }
    }

	//Return a list of HM moves this Pokemon knows
	public List<int> GetHMsKnown()
    {
		List<int> moves = new List<int>();
		//Indexes for Cut, Fly, Surf, Strength, and Flash
		int[] HMs = new int[] { 26, 48, 141, 133, 47 };
		foreach (int mIndex in MoveIndexes)
        {
			if (HMs.Contains(mIndex))
				moves.Add(mIndex);
        }

		return moves;
    }

	//TEMP - CHANGE LATER
	public void TeachMove(int moveIndex, int moveSlot)
    {
		MoveIndexes[moveSlot] = moveIndex;
		MoveMaxPPs[moveSlot] = MoveData.MaxPPs[moveSlot];
		MovePPs[moveSlot] = MoveMaxPPs[moveSlot];
    }

    #endregion
}

//Class to hold a set of stats
public class Stats
{
	public int HP;
	public int Attack;
	public int Defense;
	public int Speed;
	public int Special;

	public Stats()
    {
		HP = 0;
		Attack = 0;
		Defense = 0;
		Speed = 0;
		Special = 0;
    }

	public Stats(int hp, int atk, int def, int spd, int spcl)
    {
		HP = hp;
		Attack = atk;
		Defense = def;
		Speed = spd;
		Special = spcl;
    }

	public Stats(Stats stats)
    {
		HP = stats.HP;
		Attack = stats.Attack;
		Defense = stats.Defense;
		Speed = stats.Speed;
		Special = stats.Special;
	}
}

//Types used for Pokemon and moves
public enum Type
{
	NONE,
	BUG,
	DRAGON,
	ELECTRIC,
	FIGHTING,
	FIRE,
	FLYING,
	GHOST,
	GRASS,
	GROUND,
	ICE,
	NORMAL,
	POISON,
	PSYCHIC,
	ROCK,
	WATER
}

//Possible status effects a Pokemon could be afflicted with
public enum StatusEffect
{
	OK,
	BRN,
	FNT,
	FRZ,
	PAR,
	PSN,
	SLP
}

//Possible experience groups a Pokemon could belongs to
public enum ExperienceGroup
{
	Fast,
	MediumFast,
	MediumSlow,
	Slow
}