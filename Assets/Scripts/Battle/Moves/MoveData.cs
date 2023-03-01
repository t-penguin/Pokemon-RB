using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveData
{
	/* An array of all move names sorted alphabetically
	 * This array gives each move its corresponding index
	 * An empty or unassigned move will have index 0 */
	public static readonly string[] Names = new string[]
	{
		"EMPTY", "ABSORB", "ACID", "ACID ARMOR", "AGILITY", "AMNESIA",				//0 - 5
		"AURORA BEAM", "BARRAGE", "BARRIER", "BIDE", "BIND",						//6 - 10
		"BITE", "BLIZZARD", "BODY SLAM", "BONE CLUB", "BONEMERANG",					//11 - 15
		"BUBBLE", "BUBBLEBEAM", "CLAMP", "COMET PUNCH", "CONFUSE RAY",				//16 - 20
		"CONFUSION", "CONSTRICT", "CONVERSION", "COUNTER", "CRABHAMMER",			//21 - 25
		"CUT", "DEFENSE CURL", "DIG", "DISABLE", "DIZZY PUNCH",						//26 - 30
		"DOUBLE KICK", "DOUBLE SLAP", "DOUBLE TEAM", "DOUBLE-EDGE", "DRAGON RAGE",	//31 - 35
		"DREAM EATER", "DRILL PECK", "EARTHQUAKE", "EGG BOMB", "EMBER",				//36 - 40
		"EXPLOSION", "FIRE BLAST", "FIRE PUNCH", "FIRE SPIN", "FISSURE",			//41 - 45
		"FLAMETHROWER", "FLASH", "FLY", "FOCUS ENERGY", "FURY ATTACK",				//46 - 50
		"FURY SWIPES", "GLARE", "GROWL", "GROWTH", "GUILLOTINE",					//51 - 55
		"GUST", "HARDEN", "HAZE", "HEADBUTT", "HI JUMP KICK",						//56 - 60
		"HORN ATTACK", "HORN DRILL", "HYDRO PUMP", "HYPER BEAM", "HYPER FANG",		//61 - 65
		"HYPNOSIS", "ICE BEAM", "ICE PUNCH", "JUMP KICK", "KARATE CHOP",			//66 - 70
		"KINESIS", "LEECH LIFE", "LEECH SEED", "LEER", "LICK",						//71 - 75
		"LIGHT SCREEN", "LOVELY KISS", "LOW KICK", "MEDITATE", "MEGA DRAIN",		//76 - 80
		"MEGA KICK", "MEGA PUNCH", "METRONOME", "MIMIC", "MINIMIZE",				//81 - 85
		"MIRROR MOVE", "MIST", "NIGHT SHADE", "PAY DAY", "PECK",					//86 - 90
		"PETAL DANCE", "PIN MISSLE", "POISON GAS", "POISON POWDER", "POISON STING",	//91 - 95
		"POUND", "PSYBEAM", "PSYCHIC", "PSYWAVE", "QUICK ATTACK",					//96 - 100
		"RAGE", "RAZOR LEAF", "RAZOR WIND", "RECOVER", "REFLECT",					//101 - 105
		"REST", "ROAR", "ROCK SLIDE", "ROCK THROW", "ROLLING KICK",					//106 - 110
		"SAND ATTACK", "SCRATCH", "SCREECH", "SEISMIC TOSS", "SELF-DESTRUCT",		//111 - 115
		"SHARPEN", "SING", "SKULL BASH", "SKY ATTACK", "SLAM",						//116 - 120
		"SLASH", "SLEEP POWDER", "SLUDGE", "SMOG", "SMOKESCREEN",					//121 - 125
		"SOFT-BOILED", "SOLAR BEAM", "SONIC BOOM", "SPIKE CANNON", "SPLASH",		//126 - 130
		"SPORE", "STOMP", "STRENGTH", "STRING SHOT", "STRUGGLE",					//131 - 135
		"STUN SPORE", "SUBMISSION", "SUBSTITUTE", "SUPER FANG", "SUPERSONIC",		//136 - 140
		"SURF", "SWIFT", "SWORDS DANCE", "TACKLE", "TAIL WHIP",						//141 - 145
		"TAKE DOWN", "TELEPORT", "THRASH", "THUNDER", "THUNDER PUNCH",				//146 - 150
		"THUNDER SHOCK", "THUNDER WAVE", "THUNDERBOLT", "TOXIC", "TRANSFORM",		//151 - 155
		"TRI ATTACK", "TWINEEDLE", "VICE GRIP", "VINE WHIP", "WATER GUN",			//156 - 160
		"WATERFALL", "WHIRLWIND", "WING ATTACK", "WITHDRAW", "WRAP"					//161 - 165
	};

	// An array of each move's type
	public static readonly Type[] Types = new Type[]
	{
		Type.NONE, Type.GRASS, Type.POISON, Type.POISON, Type.PSYCHIC, Type.PSYCHIC,	//0 - 5
		Type.ICE, Type.NORMAL, Type.PSYCHIC, Type.NORMAL, Type.NORMAL,			//6 - 10
		Type.NORMAL, Type.ICE, Type.NORMAL, Type.GROUND, Type.GROUND,			//11 - 15
		Type.WATER, Type.WATER, Type.WATER, Type.NORMAL, Type.GHOST,			//16 - 20
		Type.PSYCHIC, Type.NORMAL, Type.NORMAL, Type.FIGHTING, Type.WATER,		//21 - 25
		Type.NORMAL, Type.NORMAL, Type.GROUND, Type.NORMAL, Type.NORMAL,		//26 - 30
		Type.FIGHTING, Type.NORMAL, Type.NORMAL, Type.NORMAL, Type.DRAGON,		//31 - 35
		Type.PSYCHIC, Type.FLYING, Type.GROUND, Type.NORMAL, Type.FIRE,			//36 - 40
		Type.NORMAL, Type.FIRE, Type.FIRE, Type.FIRE, Type.GROUND,				//41 - 45
		Type.FIRE, Type.NORMAL, Type.FLYING, Type.NORMAL, Type.NORMAL,			//46 - 50
		Type.NORMAL, Type.NORMAL, Type.NORMAL, Type.NORMAL, Type.NORMAL,		//51 - 55
		Type.NORMAL, Type.NORMAL, Type.ICE, Type.NORMAL, Type.FIGHTING,			//56 - 60
		Type.NORMAL, Type.NORMAL, Type.WATER, Type.NORMAL, Type.NORMAL,			//61 - 65
		Type.PSYCHIC, Type.ICE, Type.ICE, Type.FIGHTING, Type.NORMAL,			//66 - 70
		Type.PSYCHIC, Type.BUG, Type.GRASS, Type.NORMAL, Type.GHOST,			//71 - 75
		Type.PSYCHIC, Type.NORMAL, Type.FIGHTING, Type.PSYCHIC, Type.GRASS,		//76 - 80
		Type.NORMAL, Type.NORMAL, Type.NORMAL, Type.NORMAL, Type.NORMAL,		//81 - 85
		Type.FLYING, Type.ICE, Type.GHOST, Type.NORMAL, Type.FLYING,			//86 - 90
		Type.GRASS, Type.BUG, Type.POISON, Type.POISON, Type.POISON,			//91 - 95
		Type.NORMAL, Type.PSYCHIC, Type.PSYCHIC, Type.PSYCHIC, Type.NORMAL,		//96 - 100
		Type.NORMAL, Type.GRASS, Type.NORMAL, Type.NORMAL, Type.PSYCHIC,		//101 - 105
		Type.PSYCHIC, Type.NORMAL, Type.ROCK, Type.ROCK, Type.FIGHTING,			//106 - 110
		Type.NORMAL, Type.NORMAL, Type.NORMAL, Type.FIGHTING, Type.NORMAL,		//111 - 115
		Type.NORMAL, Type.NORMAL, Type.NORMAL, Type.FLYING, Type.NORMAL,		//116 - 120
		Type.NORMAL, Type.GRASS, Type.POISON, Type.POISON, Type.NORMAL,			//121 - 125
		Type.NORMAL, Type.GRASS, Type.NORMAL, Type.NORMAL, Type.NORMAL,			//126 - 130
		Type.GRASS, Type.NORMAL, Type.NORMAL, Type.BUG, Type.NORMAL,			//131 - 135
		Type.GRASS, Type.FIGHTING, Type.NORMAL, Type.NORMAL, Type.NORMAL,		//136 - 140
		Type.WATER, Type.NORMAL, Type.NORMAL, Type.NORMAL, Type.NORMAL,			//141 - 145
		Type.NORMAL, Type.PSYCHIC, Type.NORMAL, Type.ELECTRIC, Type.ELECTRIC,	//146 - 150
		Type.ELECTRIC, Type.ELECTRIC, Type.ELECTRIC, Type.POISON, Type.NORMAL,	//151 - 155
		Type.NORMAL, Type.BUG, Type.NORMAL, Type.GRASS, Type.WATER,				//156 - 160
		Type.WATER, Type.NORMAL, Type.FLYING, Type.WATER, Type.NORMAL			//161 - 165
	};

	// An array of each move's category
	public static readonly Category[] Categories = new Category[]
	{
		Category.Status, Category.Special, Category.Physical, Category.Status, Category.Status, Category.Status,	//0 - 5
		Category.Special, Category.Physical, Category.Status, Category.Physical, Category.Physical,		//6 - 10
		Category.Physical, Category.Special, Category.Physical, Category.Physical, Category.Physical,	//11 - 15
		Category.Special, Category.Special, Category.Special, Category.Physical, Category.Status,		//16 - 20
		Category.Special, Category.Physical, Category.Status, Category.Physical, Category.Special,		//21 - 25
		Category.Physical, Category.Status, Category.Physical, Category.Status, Category.Physical,		//26 - 30
		Category.Physical, Category.Physical, Category.Status, Category.Physical, Category.Special,		//31 - 35
		Category.Special, Category.Physical, Category.Physical, Category.Physical, Category.Special,	//36 - 40
		Category.Physical, Category.Special, Category.Special, Category.Special, Category.Physical,		//41 - 45
		Category.Special, Category.Status, Category.Physical, Category.Status, Category.Physical,		//46 - 50
		Category.Physical, Category.Status, Category.Status, Category.Status, Category.Physical,		//51 - 55
		Category.Physical, Category.Status, Category.Status, Category.Physical, Category.Physical,		//56 - 60
		Category.Physical, Category.Physical, Category.Special, Category.Physical, Category.Physical,	//61 - 65
		Category.Status, Category.Special, Category.Special, Category.Physical, Category.Physical,		//66 - 70
		Category.Status, Category.Physical, Category.Status, Category.Status, Category.Physical,		//71 - 75
		Category.Status, Category.Status, Category.Physical, Category.Status, Category.Special,			//76 - 80
		Category.Physical, Category.Physical, Category.Status, Category.Status, Category.Status,		//81 - 85
		Category.Status, Category.Status, Category.Physical, Category.Physical, Category.Physical,		//86 - 90
		Category.Special, Category.Physical, Category.Status, Category.Status, Category.Physical,		//91 - 95
		Category.Physical, Category.Special, Category.Special, Category.Special, Category.Physical,		//96 - 100
		Category.Physical, Category.Special, Category.Physical, Category.Status, Category.Status,		//101 - 105
		Category.Status, Category.Status, Category.Physical, Category.Physical, Category.Physical,		//106 - 110
		Category.Status, Category.Physical, Category.Status, Category.Physical, Category.Physical,		//111 - 115
		Category.Status, Category.Status, Category.Physical, Category.Physical, Category.Physical,		//116 - 120
		Category.Physical, Category.Status, Category.Physical, Category.Physical, Category.Status,		//121 - 125
		Category.Status, Category.Special, Category.Physical, Category.Physical, Category.Status,		//126 - 130
		Category.Status, Category.Physical, Category.Physical, Category.Status, Category.Physical,		//131 - 135
		Category.Status, Category.Physical, Category.Status, Category.Physical, Category.Status,		//136 - 140
		Category.Special, Category.Physical, Category.Status, Category.Physical, Category.Status,		//141 - 145
		Category.Physical, Category.Status, Category.Physical, Category.Special, Category.Special,		//146 - 150
		Category.Special, Category.Status, Category.Special, Category.Status, Category.Status,			//151 - 155
		Category.Physical, Category.Physical, Category.Physical, Category.Special, Category.Special,	//156 - 160
		Category.Special, Category.Status, Category.Physical, Category.Status, Category.Physical		//161 - 165
	};

	// An array of each move's standard Max PP
	public static readonly int[] MaxPPs = new int[]
	{
		0, 20, 30, 40, 30, 20, 20, 20, 30, 10, 20,	//0 - 10
		25, 5, 15, 20, 10, 30, 20, 10, 15, 10,		//11 - 20
		25, 35, 30, 20, 10, 30, 40, 10, 20, 10,		//21 - 30
		30, 10, 15, 15, 10, 15, 20, 10, 10, 25,		//31 - 40
		5, 5, 15, 15, 5, 15, 20, 15, 30, 20,		//41 - 50
		15, 30, 40, 40, 5, 35, 30, 30, 15, 10,		//51 - 60
		25, 5, 5, 5, 15, 20, 10, 15, 10, 25,		//61 - 70
		15, 15, 10, 30, 30, 30, 10, 20, 40, 10,		//71 - 80
		5, 20, 10, 10, 10, 20, 30, 15, 20, 35,		//81 - 90
		20, 20, 40, 35, 35, 35, 20, 10, 15, 30,		//91 - 100
		20, 25, 10, 20, 20, 10, 20, 10, 15, 15,		//101 - 110
		15, 35, 40, 20, 5, 30, 15, 10, 5, 20,		//111 - 120
		20, 15, 20, 20, 20, 10, 10, 20, 15, 40,		//121 - 130
		15, 20, 15, 40, 10, 30, 20, 10, 10, 20,		//131 - 140
		15, 20, 20, 35, 30, 20, 20, 20, 10, 15,		//141 - 150
		30, 20, 15, 10, 10, 10, 10, 30, 10, 25,		//151 - 160
		15, 20, 35, 40, 20							//161 - 165
	};

	// An array of each move's power
	public static readonly int[] Powers = new int[]
	{
		0, 20, 40, 0, 0, 0, 65, 15, 0, 0, 15,		//0 - 10
		60, 110, 85, 65, 50, 20, 65, 35, 18, 0,		//11 - 20
		50, 10, 0, 0, 90, 50, 0, 100, 0, 70,		//21 - 30
		30, 15, 0, 100, 0, 100, 80, 100, 100, 40,	//31 - 40
		340, 110, 75, 15, 0, 90, 0, 90, 0, 15,		//41 - 50
		18, 0, 0, 0, 0, 40, 0, 0, 70, 85,			//51 - 60
		65, 0, 110, 150, 80, 0, 95, 75, 70, 50,		//61 - 70
		0, 20, 0, 0, 30, 0, 0, 50, 0, 40,			//71 - 80
		120, 80, 0, 0, 0, 0, 0, 0, 40, 35,			//81 - 90
		70, 14, 0, 0, 15, 40, 65, 90, 0, 40,		//91 - 100
		20, 55, 80, 0, 0, 0, 0, 75, 50, 60,			//101 - 110
		0, 40, 0, 0, 260, 0, 0, 130, 140, 80,		//111 - 120
		70, 0, 65, 20, 0, 0, 120, 0, 20, 0,			//121 - 130
		0, 65, 80, 0, 50, 0, 80, 0, 0, 0,			//131 - 140
		90, 60, 0, 40, 0, 90, 0, 90, 110, 75,		//141 - 150
		40, 0, 90, 0, 0, 80, 80, 55, 35, 40,		//151 - 160
		80, 0, 35, 0, 15							//161 - 165
	};

	// An array of each move's accuracy
	public static readonly int[] Accuracies = new int[]
	{
		0, 100, 100, 0, 0, 0, 100, 85, 0, 0, 75,		//0 - 10
		100, 90, 100, 85, 90, 100, 100, 85, 85, 100,	//11 - 20
		100, 100, 0, 100, 85, 95, 0, 100, 55, 100,		//21 - 30
		100, 85, 0, 100, 100, 100, 100, 100, 75, 100,	//31 - 40
		100, 85, 100, 70, 30, 100, 70, 95, 0, 85,		//41 - 50
		80, 75, 100, 0, 30, 100, 0, 0, 100, 90,			//51 - 60
		100, 30, 80, 90, 90, 60, 100, 100, 95, 100,		//61 - 70
		80, 100, 90, 100, 100, 0, 75, 90, 0, 100,		//71 - 80
		75, 85, 100, 100, 100, 100, 100, 100, 100, 100,	//81 - 90
		100, 85, 55, 75, 100, 100, 100, 100, 80, 100,	//91 - 100
		100, 95, 75, 0, 0, 0, 0, 90, 65, 85,			//101 - 110
		100, 100, 85, 100, 100, 0, 55, 100, 90, 75,		//111 - 120
		100, 75, 100, 70, 100, 0, 100, 90, 100, 0,		//121 - 130
		100, 100, 100, 95, 100, 75, 85, 100, 90, 55,	//131 - 140
		100, 100, 0, 100, 100, 85, 0, 100, 70, 100,		//141 - 150
		100, 100, 100, 85, 0, 100, 100, 100, 100, 100,	//151 - 160
		100, 0, 100, 0, 90								//161 - 165
	};

	/* An array of type matchups
	 * Access as [AttackType][DefenderType]
	 * Types are ordered the same horizontally as they are vertically */
	private static readonly float[][] TypeEffectiveness = new float[][]
	{
		//None
		new float[] {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f},
		//Bug
		new float[] {1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 0.5f, 0.5f, 0.5f, 2.0f, 1.0f, 1.0f, 1.0f, 2.0f, 2.0f, 1.0f, 1.0f},
		//Dragon
		new float[] {1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f},
		//Electric
		new float[] {1.0f, 1.0f, 0.5f, 0.5f, 0.5f, 1.0f, 2.0f, 1.0f, 0.5f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f},
		//Fighting
		new float[] {1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 0.0f, 1.0f, 1.0f, 2.0f, 2.0f, 0.5f, 0.5f, 2.0f, 1.0f},
		//Fire
		new float[] {1.0f, 2.0f, 0.5f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 2.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 0.5f, 0.5f},
		//Flying
		new float[] {1.0f, 2.0f, 1.0f, 0.5f, 2.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f},
		//Ghost
		new float[] {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f},
		//Grass
		new float[] {1.0f, 0.5f, 0.5f, 1.0f, 1.0f, 0.5f, 0.5f, 1.0f, 0.5f, 2.0f, 1.0f, 1.0f, 0.5f, 1.0f, 2.0f, 2.0f},
		//Ground
		new float[] {1.0f, 0.5f, 1.0f, 2.0f, 1.0f, 2.0f, 0.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 2.0f, 1.0f},
		//Ice
		new float[] {1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 2.0f, 2.0f, 0.5f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f},
		//Normal
		new float[] {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f},
		//Poison
		new float[] {1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 2.0f, 0.5f, 1.0f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f},
		//Psychic
		new float[] {1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 0.5f, 1.0f, 1.0f},
		//Rock
		new float[] {1.0f, 2.0f, 1.0f, 1.0f, 0.5f, 2.0f, 2.0f, 1.0f, 1.0f, 0.5f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f},
		//Water
		new float[] {1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f, 0.5f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 2.0f, 0.5f}
	};



	/// <summary>
	/// Returns the type matchup multiplier between an attacking type and a defending type.
	/// </summary>
	/// <param name="attackType">The attacking type.</param>
	/// <param name="defenseType">The defending type.</param>
	/// <returns></returns>
	public static float GetMatchupMultiplier(Type attackType, Type defenseType)
		=> TypeEffectiveness[(int)attackType][(int)defenseType];

	/// <summary>
	/// Returns the type matchup multiplier between an attacking move and a defending pokemon.
	/// </summary>
	/// <param name="move">The attacking move.</param>
	/// <param name="target">The defending Pokemon.</param>
	public static float GetMatchupMultiplier(AttackMove move, BattlePokemon target)
		=> GetMatchupMultiplier(move.Type, target.Primary) * GetMatchupMultiplier(move.Type, target.Secondary);

	/// <summary>
	/// Returns whether the given move has no effect on the target.
	/// </summary>
	/// <param name="move">The attacking move.</param>
	/// <param name="target">The defending Pokemon.</param>
	public static bool HasNoEffect(AttackMove move, BattlePokemon target) => GetMatchupMultiplier(move, target) == 0f;

	/// <summary>
	/// Calculates the amount of damage the user's move will do to the target
	/// </summary>
	/// <param name="move">The move that the user is using.</param>
	/// <param name="user">The attacking Pokemon.</param>
	/// <param name="target">The defending Pokemon.</param>
	/// <returns></returns>
	public static int CalculateDamage(AttackMove move, BattlePokemon user, BattlePokemon target)
    {
		bool isPhysicalMove = move.Category == Category.Physical;
		// Use corresponding stats depending on the move category
		int userAttack = isPhysicalMove ? user.Stats.Attack : user.Stats.Special;
		int targetDefense = isPhysicalMove ? target.Stats.Defense : target.Stats.Special;

		int userLevel = user.Level;
		float stab = STABMultiplier(move, user);
		float typeMultiplier = GetMatchupMultiplier(move, target);

		// Critical hit moves ignore stat modifications
		if(IsCrit(move, user))
			userLevel *= 2;
		// Non-critical hit moves take into account stat modifications
		else
        {
			userAttack = isPhysicalMove ? user.BattleStats.Attack : user.BattleStats.Special;
			targetDefense = isPhysicalMove ? user.BattleStats.Defense : user.BattleStats.Special;

			//ADD HERE?: Check for light screen and reflect to modify stats
        }

		return DamageFormula(userLevel, userAttack, targetDefense, move.Power, stab, typeMultiplier);
    }

	/// <summary>
	/// Determines whether or not the use of the given move resulted in a critical hit.
	/// </summary>
	/// <param name="move">The move that the user is using.</param>
	/// <param name="user">The attacking Pokemon.</param>
	/// <returns></returns>
	public static bool IsCrit(AttackMove move, BattlePokemon user)
    {
		int check = Random.Range(0, 256);
		int threshold = user.Stats.Speed / 2;

		// ADD HERE: Focus Energy or Dire Hit => t/= 4

		// Multiply the threshold by 8 if the move has a high critical hit ratio
		if (move.HasHighCritRatio)
			threshold *= 8;

		// Clamp the threshold between 1 and 255
		threshold = Mathf.Clamp(threshold, 1, 255);

		return check < threshold;
    }

	/// <summary>
	/// Returns the Same Type Attack Bonus (STAB) multiplier for the given move and user. 
	/// Returns 1.5 if there is a bonus, 1 otherwise.
	/// </summary>
	/// <param name="move">The move that the user is using.</param>
	/// <param name="user">The attacking Pokemon.</param>
	/// <returns></returns>
	public static float STABMultiplier(AttackMove move, BattlePokemon user)
		=> (user.Primary == move.Type || user.Secondary == move.Type) ? 1.5f : 1.0f;


	/// <summary>
	/// The damage formula given stats from the attacker and the target.
	/// </summary>
	/// <param name="level">The level of the attacking Pokemon.</param>
	/// <param name="attack">The attacking stat of the attacking Pokemon.</param>
	/// <param name="defense">The defending stat of the target Pokemon.</param>
	/// <param name="power">The power of the move being used.</param>
	/// <param name="stab">The STAB multiplier of the attacking move.</param>
	/// <param name="typeMultiplier">The type matchup multiplier.</param>
	/// <returns></returns>
	public static int DamageFormula(int level, int attack, int defense, int power, float stab, float typeMultiplier)
    {
		int rawDamage = (2 * level / 5 + 2) * power * (attack / defense) / 50 + 2;
		return (int)(rawDamage * stab * typeMultiplier * Random.Range(217, 256) / 255);
    }
}

// Possible move categories
public enum Category
{
	Physical,
	Special,
	Status
}