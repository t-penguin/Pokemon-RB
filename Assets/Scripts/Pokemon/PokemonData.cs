using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PokemonData
{
	//For each array below (except MiniSprites), an index of 0 refers to an empty/null Pokemon
	
    //An array of all Pokemon names by Pokedex number
    public static readonly string[] Names = new string[]
    {
        "EMPTY", "BULBASAUR", "IVYSAUR", "VENUSAUR", "CHARMANDER", "CHARMELEON",    //0 - 5
        "CHARIZARD", "SQUIRTLE", "WARTORTLE", "BLASTOISE", "CATERPIE",		//6 - 10
        "METAPOD", "BUTTERFREE", "WEEDLE", "KAKUNA", "BEEDRILL",            //11 - 15
        "PIDGEY", "PIDGEOTTO", "PIDGEOT", "RATTATA", "RATICATE",            //16 - 20
        "SPEAROW", "FEAROW", "EKANS", "ARBOK", "PIKACHU",                   //21 - 25
        "RAICHU", "SANDSHREW", "SANDSLASH", "NIDORAN|", "NIDORINA",			//26 - 30
        "NIDOQUEEN", "NIDORAN^", "NIDORINO", "NIDOKING", "CLEFAIRY",        //31 - 35
        "CLEFABLE", "VULPIX", "NINETALES", "JIGGLYPUFF", "WIGGLYTUFF",		//36 - 40
		"ZUBAT", "GOLBAT", "ODDISH", "GLOOM", "VILEPLUME",					//41 - 45
		"PARAS", "PARASECT", "VENONAT",	"VENOMOTH",	"DIGLETT",              //46 - 50
		"DUGTRIO", "MEOWTH", "PERSIAN",	"PSYDUCK", "GOLDUCK",               //51 - 55
		"MANKEY", "PRIMEAPE", "GROWLITHE", "ARCANINE", "POLIWAG",           //56 - 60
        "POLIWHIRL", "POLIWRATH", "ABRA", "KADABRA", "ALAKAZAM",			//61 - 65
		"MACHOP", "MACHOKE", "MACHAMP",	"BELLSPROUT", "WEEPINBELL",			//66 - 70
		"VICTREEBEL", "TENTACOOL", "TENTACRUEL", "GEODUDE",	"GRAVELER",		//71 - 75
		"GOLEM", "PONYTA", "RAPIDASH", "SLOWPOKE", "SLOWBRO",				//76 - 80
		"MAGNEMITE", "MAGNETON", "FARFETCH'D", "DODUO",	"DODRIO",			//81 - 85
		"SEEL", "DEWGONG", "GRIMER", "MUK",	"SHELLDER",						//86 - 90
		"CLOYSTER",	"GASTLY", "HAUNTER", "GENGAR", "ONIX",					//91 - 95
		"DROWZEE", "HYPNO",	"KRABBY", "KINGLER", "VOLTORB",                 //96 - 100
        "ELECTRODE", "EXEGGCUTE", "EXEGGUTOR", "CUBONE", "MAROWAK",			//101 - 105
		"HITMONLEE", "HITMONCHAN", "LICKITUNG",	"KOFFING", "WEEZING",		//106 - 110
		"RHYHORN", "RHYDON", "CHANSEY",	"TANGELA", "KANGASKHAN",			//111 - 115
		"HORSEA", "SEADRA",	"GOLDEEN", "SEAKING", "STARYU",					//116 - 120
		"STARMIE", "MR.MIME", "SCYTHER", "JYNX", "ELECTABUZZ",				//121 - 125
		"MAGMAR", "PINSIR", "TAUROS", "MAGIKARP", "GYARADOS",				//126 - 130
		"LAPRAS", "DITTO", "EEVEE",	"VAPOREON",	"JOLTEON",					//131 - 135
		"FLAREON", "PORYGON", "OMANYTE", "OMASTAR", "KABUTO",				//136 - 140
		"KABUTOPS",	"AERODACTYL", "SNORLAX", "ARTICUNO", "ZAPDOS",			//141 - 145
		"MOLTRES", "DRATINI", "DRAGONAIR", "DRAGONITE",	"MEWTWO", "MEW"		//146 - 151
    };

	/* An array of each Pokemon's types by Pokedex number
	 * Each Pokemon has {Primary, Secondary} typing */
	public static readonly Type[][] Types = new Type[][]
	{
		new Type[] {Type.NONE, Type.NONE}, 		//0 - Null Pokemon
		new Type[] {Type.GRASS, Type.POISON}, 	//1 - Bulbasaur
		new Type[] {Type.GRASS, Type.POISON}, 	//2 - Ivysaur
		new Type[] {Type.GRASS, Type.POISON}, 	//3 - Venusaur
		new Type[] {Type.FIRE, Type.NONE}, 		//4 - Charmander
		new Type[] {Type.FIRE, Type.NONE},		//5 - Charmeleon
		new Type[] {Type.FIRE, Type.FLYING},	//6 - Charizard
		new Type[] {Type.WATER, Type.NONE},		//7 - Squirtle
		new Type[] {Type.WATER, Type.NONE},		//8 - Wartortle
		new Type[] {Type.WATER, Type.NONE},		//9 - Blastoise
		new Type[] {Type.BUG, Type.NONE},		//10 - Caterpie
		new Type[] {Type.BUG, Type.NONE},		//11 - Metapod
		new Type[] {Type.BUG, Type.FLYING},		//12 - Butterfree
		new Type[] {Type.BUG, Type.POISON},		//13 - Weedle
		new Type[] {Type.BUG, Type.POISON},		//14 - Kakuna
		new Type[] {Type.BUG, Type.POISON},		//15 - Beedrill
		new Type[] {Type.NORMAL, Type.FLYING},	//16 - Pidgey
		new Type[] {Type.NORMAL, Type.FLYING},	//17 - Pidgeotto
		new Type[] {Type.NORMAL, Type.FLYING},	//18 - Pidgeot
		new Type[] {Type.NORMAL, Type.NONE},	//19 - Rattata
		new Type[] {Type.NORMAL, Type.NONE},	//20 - Raticate
		new Type[] {Type.NORMAL, Type.FLYING},	//21 - Spearow
		new Type[] {Type.NORMAL, Type.FLYING},	//22 - Fearow
		new Type[] {Type.POISON, Type.NONE},	//23 - Ekans
		new Type[] {Type.POISON, Type.NONE},	//24 - Arbok
		new Type[] {Type.ELECTRIC, Type.NONE},	//25 - Pikachu
		new Type[] {Type.ELECTRIC, Type.NONE},	//26 - Raichu
		new Type[] {Type.GROUND, Type.NONE},	//27 - Sandshrew
		new Type[] {Type.GROUND, Type.NONE},	//28 - Sandslash
		new Type[] {Type.POISON, Type.NONE},	//29 - Nidoran F
		new Type[] {Type.POISON, Type.NONE},	//30 - Nidorina
		new Type[] {Type.POISON, Type.GROUND},	//31 - Nidoqueen
		new Type[] {Type.POISON, Type.NONE},	//32 - Nidoran M
		new Type[] {Type.POISON, Type.NONE},	//33 - Nidorino
		new Type[] {Type.POISON, Type.GROUND},	//34 - Nidoking
		new Type[] {Type.NORMAL, Type.NONE},	//35 - Clefairy
		new Type[] {Type.NORMAL, Type.NONE},	//36 - Clefable
		new Type[] {Type.FIRE, Type.NONE},		//37 - Vulpix
		new Type[] {Type.FIRE, Type.NONE},		//38 - Ninetales
		new Type[] {Type.NORMAL, Type.NONE},	//39 - Jigglypuff
		new Type[] {Type.NORMAL, Type.NONE},	//40 - Wigglytuff
		new Type[] {Type.POISON, Type.FLYING},	//41 - Zubat
		new Type[] {Type.POISON, Type.FLYING},	//42 - Golbat
		new Type[] {Type.GRASS, Type.POISON},	//43 - Oddish
		new Type[] {Type.GRASS, Type.POISON},	//44 - Gloom
		new Type[] {Type.GRASS, Type.POISON},	//45 - Vileplume
		new Type[] {Type.BUG, Type.GRASS},		//46 - Paras
		new Type[] {Type.BUG, Type.GRASS},		//47 - Parasect
		new Type[] {Type.BUG, Type.POISON},		//48 - Venonat
		new Type[] {Type.BUG, Type.POISON},		//49 - Venomoth
		new Type[] {Type.GROUND, Type.NONE},	//50 - Diglett
		new Type[] {Type.GROUND, Type.NONE},	//51 - Dugtrio
		new Type[] {Type.NORMAL, Type.NONE},	//52 - Meowth
		new Type[] {Type.NORMAL, Type.NONE},	//53 - Persian
		new Type[] {Type.WATER, Type.NONE},		//54 - Psyduck
		new Type[] {Type.WATER, Type.NONE},		//55 - Golduck
		new Type[] {Type.FIGHTING, Type.NONE},	//56 - Mankey
		new Type[] {Type.FIGHTING, Type.NONE},	//57 - Primeape
		new Type[] {Type.FIRE, Type.NONE},		//58 - Growlithe
		new Type[] {Type.FIRE, Type.NONE},		//59 - Arcanine
		new Type[] {Type.WATER, Type.NONE},		//60 - Poliwag
		new Type[] {Type.WATER, Type.NONE},		//61 - Poliwhirl
		new Type[] {Type.WATER, Type.FIGHTING},	//62 - Poliwrath
		new Type[] {Type.PSYCHIC, Type.NONE},	//63 - Abra
		new Type[] {Type.PSYCHIC, Type.NONE},	//64 - Kadabra
		new Type[] {Type.PSYCHIC, Type.NONE},	//65 - Alakazam
		new Type[] {Type.FIGHTING, Type.NONE},	//66 - Machop
		new Type[] {Type.FIGHTING, Type.NONE},	//67 - Machoke
		new Type[] {Type.FIGHTING, Type.NONE},	//68 - Machamp
		new Type[] {Type.GRASS, Type.POISON},	//69 - Bellsprout
		new Type[] {Type.GRASS, Type.POISON},	//70 - Weepinbell
		new Type[] {Type.GRASS, Type.POISON},	//71 - Victreebel
		new Type[] {Type.WATER, Type.POISON},	//72 - Tentacool
		new Type[] {Type.WATER, Type.POISON},	//73 - Tentacruel
		new Type[] {Type.ROCK, Type.GROUND},	//74 - Geodude
		new Type[] {Type.ROCK, Type.GROUND},	//75 - Graveler
		new Type[] {Type.ROCK, Type.GROUND},	//76 - Golem
		new Type[] {Type.FIRE, Type.NONE},		//77 - Ponyta
		new Type[] {Type.FIRE, Type.NONE},		//78 - Rapidash
		new Type[] {Type.WATER, Type.PSYCHIC},	//79 - Slowpoke
		new Type[] {Type.WATER, Type.PSYCHIC},	//80 - Slowbro
		new Type[] {Type.ELECTRIC, Type.NONE},	//81 - Magnemite
		new Type[] {Type.ELECTRIC, Type.NONE},	//82 - Magneton
		new Type[] {Type.NORMAL, Type.FLYING},	//83 - Farfetch'd
		new Type[] {Type.NORMAL, Type.FLYING},	//84 - Doduo
		new Type[] {Type.NORMAL, Type.FLYING},	//85 - Dodrio
		new Type[] {Type.WATER, Type.NONE},		//86 - Seel
		new Type[] {Type.WATER, Type.ICE},		//87 - Dewgong
		new Type[] {Type.POISON, Type.NONE},	//88 - Grimer
		new Type[] {Type.POISON, Type.NONE},	//89 - Muk
		new Type[] {Type.WATER, Type.NONE},		//90 - Shellder
		new Type[] {Type.WATER, Type.ICE},		//91 - Cloyster
		new Type[] {Type.GHOST, Type.POISON},	//92 - Gastly
		new Type[] {Type.GHOST, Type.POISON},	//93 - Haunter
		new Type[] {Type.GHOST, Type.POISON},	//94 - Gengar
		new Type[] {Type.ROCK, Type.GROUND},	//95 - Onix
		new Type[] {Type.PSYCHIC, Type.NONE},	//96 - Drowzee
		new Type[] {Type.PSYCHIC, Type.NONE},	//97 - Hypno
		new Type[] {Type.WATER, Type.NONE},		//98 - Krabby
		new Type[] {Type.WATER, Type.NONE},		//99 - Kingler
		new Type[] {Type.ELECTRIC, Type.NONE},	//100 - Voltorb
		new Type[] {Type.ELECTRIC, Type.NONE},	//101 - Electrode
		new Type[] {Type.GRASS, Type.PSYCHIC},	//102 - Exeggcute
		new Type[] {Type.GRASS, Type.PSYCHIC},	//103 - Exeggutor
		new Type[] {Type.GROUND, Type.NONE},	//104 - Cubone
		new Type[] {Type.GROUND, Type.NONE},	//105 - Marowak
		new Type[] {Type.FIGHTING, Type.NONE},	//106 - Hitmonlee
		new Type[] {Type.FIGHTING, Type.NONE},	//107 - Hitmonchan
		new Type[] {Type.NORMAL, Type.NONE},	//108 - Lickitung
		new Type[] {Type.POISON, Type.NONE},	//109 - Koffing
		new Type[] {Type.POISON, Type.NONE},	//110 - Weezing
		new Type[] {Type.GROUND, Type.ROCK},	//111 - Rhyhorn
		new Type[] {Type.GROUND, Type.ROCK},	//112 - Rhydon
		new Type[] {Type.NORMAL, Type.NONE},	//113 - Chansey
		new Type[] {Type.GRASS, Type.NONE},		//114 - Tangela
		new Type[] {Type.NORMAL, Type.NONE},	//115 - Kangaskhan
		new Type[] {Type.WATER, Type.NONE},		//116 - Horsea
		new Type[] {Type.WATER, Type.NONE},		//117 - Seadra
		new Type[] {Type.WATER, Type.NONE},		//118 - Goldeen
		new Type[] {Type.WATER, Type.NONE},		//119 - Seaking
		new Type[] {Type.WATER, Type.NONE},		//120 - Staryu
		new Type[] {Type.WATER, Type.PSYCHIC},	//121 - Starmie
		new Type[] {Type.PSYCHIC, Type.NONE},	//122 - Mr.Mime
		new Type[] {Type.BUG, Type.FLYING},		//123 - Scyther
		new Type[] {Type.ICE, Type.PSYCHIC},	//124 - Jynx
		new Type[] {Type.ELECTRIC, Type.NONE},	//125 - Electabuzz
		new Type[] {Type.FIRE, Type.NONE},		//126 - Magmar
		new Type[] {Type.BUG, Type.NONE},		//127 - Pinsir
		new Type[] {Type.NORMAL, Type.NONE},	//128 - Tauros
		new Type[] {Type.WATER, Type.NONE},		//129 - Magikarp
		new Type[] {Type.WATER, Type.FLYING},	//130 - Gyarados
		new Type[] {Type.WATER, Type.ICE},		//131 - Lapras
		new Type[] {Type.NORMAL, Type.NONE},	//132 - Ditto
		new Type[] {Type.NORMAL, Type.NONE},	//133 - Eevee
		new Type[] {Type.WATER, Type.NONE},		//134 - Vaporeon
		new Type[] {Type.ELECTRIC, Type.NONE},	//135 - Jolteon
		new Type[] {Type.FIRE, Type.NONE},		//136 - Flareon
		new Type[] {Type.NORMAL, Type.NONE},	//137 - Porygon
		new Type[] {Type.ROCK, Type.WATER},		//138 - Omanyte
		new Type[] {Type.ROCK, Type.WATER},		//139 - Omastar
		new Type[] {Type.ROCK, Type.WATER},		//140 - Kabuto
		new Type[] {Type.ROCK, Type.WATER},		//141 - Kabutops
		new Type[] {Type.ROCK, Type.FLYING},	//142 - Aerodactyl
		new Type[] {Type.NORMAL, Type.NONE},	//143 - Snorlax
		new Type[] {Type.ICE, Type.FLYING},		//144 - Articuno
		new Type[] {Type.ELECTRIC, Type.FLYING},//145 - Zapdos
		new Type[] {Type.FIRE, Type.FLYING},	//146 - Moltres
		new Type[] {Type.DRAGON, Type.NONE},	//147 - Dratini
		new Type[] {Type.DRAGON, Type.NONE},	//148 - Dragonair
		new Type[] {Type.DRAGON, Type.FLYING},	//149 - Dragonite
		new Type[] {Type.PSYCHIC, Type.NONE},	//150 - Mewtwo
		new Type[] {Type.PSYCHIC, Type.NONE},	//151 - Mew
	};

	/* An array of which Experience group each Pokemon belongs to
	 * Sorted by Pokedex number */
	public static readonly ExperienceGroup[] ExpGroups = new ExperienceGroup[]
	{
		ExperienceGroup.Fast, 		//0 - Null Pokemon
		ExperienceGroup.MediumSlow, //1 - Bulbasaur
		ExperienceGroup.MediumSlow,	//2 - Ivysaur
		ExperienceGroup.MediumSlow,	//3 - Venusaur
		ExperienceGroup.MediumSlow,	//4 - Charmander
		ExperienceGroup.MediumSlow,	//5 - Charmeleon
		ExperienceGroup.MediumSlow,	//6 - Charizard
		ExperienceGroup.MediumSlow,	//7 - Squirtle
		ExperienceGroup.MediumSlow,	//8 - Wartortle
		ExperienceGroup.MediumSlow,	//9 - Blastoise
		ExperienceGroup.MediumFast,	//10 - Caterpie
		ExperienceGroup.MediumFast,	//11 - Metapod
		ExperienceGroup.MediumFast,	//12 - Butterfree
		ExperienceGroup.MediumFast,	//13 - Weedle
		ExperienceGroup.MediumFast,	//14 - Kakuna
		ExperienceGroup.MediumFast,	//15 - Beedrill
		ExperienceGroup.MediumSlow,	//16 - Pidgey
		ExperienceGroup.MediumSlow,	//17 - Pidgeotto
		ExperienceGroup.MediumSlow,	//18 - Pidgeot
		ExperienceGroup.MediumFast,	//19 - Rattata
		ExperienceGroup.MediumFast,	//20 - Raticate
		ExperienceGroup.MediumFast,	//21 - Spearow
		ExperienceGroup.MediumFast,	//22 - Fearow
		ExperienceGroup.MediumFast,	//23 - Ekans
		ExperienceGroup.MediumFast,	//24 - Arbok
		ExperienceGroup.MediumFast,	//25 - Pikachu
		ExperienceGroup.MediumFast,	//26 - Raichu
		ExperienceGroup.MediumFast,	//27 - Sandshrew
		ExperienceGroup.MediumFast,	//28 - Sandslash
		ExperienceGroup.MediumSlow,	//29 - Nidoran F
		ExperienceGroup.MediumSlow,	//30 - Nidorina
		ExperienceGroup.MediumSlow,	//31 - Nidoqueen
		ExperienceGroup.MediumSlow,	//32 - Nidoran M
		ExperienceGroup.MediumSlow,	//33 - Nidorino
		ExperienceGroup.MediumSlow,	//34 - Nidoking
		ExperienceGroup.Fast, 		//35 - Clefairy
		ExperienceGroup.Fast, 		//36 - Clefable
		ExperienceGroup.MediumFast,	//37 - Vulpix
		ExperienceGroup.MediumFast,	//38 - Ninetales
		ExperienceGroup.Fast, 		//39 - Jigglypuff
		ExperienceGroup.Fast, 		//40 - Wigglytuff
		ExperienceGroup.MediumFast,	//41 - Zubat
		ExperienceGroup.MediumFast,	//42 - Golbat
		ExperienceGroup.MediumSlow,	//43 - Oddish
		ExperienceGroup.MediumSlow,	//44 - Gloom
		ExperienceGroup.MediumSlow,	//45 - Vileplume
		ExperienceGroup.MediumFast,	//46 - Paras
		ExperienceGroup.MediumFast,	//47 - Parasect
		ExperienceGroup.MediumFast,	//48 - Venonat
		ExperienceGroup.MediumFast,	//49 - Venomoth
		ExperienceGroup.MediumFast,	//50 - Diglett
		ExperienceGroup.MediumFast,	//51 - Dugtrio
		ExperienceGroup.MediumFast,	//52 - Meowth
		ExperienceGroup.MediumFast,	//53 - Persian
		ExperienceGroup.MediumFast,	//54 - Psyduck
		ExperienceGroup.MediumFast,	//55 - Golduck
		ExperienceGroup.MediumFast,	//56 - Mankey
		ExperienceGroup.MediumFast,	//57 - Primeape
		ExperienceGroup.Slow,		//58 - Growlithe
		ExperienceGroup.Slow,		//59 - Arcanine
		ExperienceGroup.MediumSlow,	//60 - Poliwag
		ExperienceGroup.MediumSlow,	//61 - Poliwhirl
		ExperienceGroup.MediumSlow,	//62 - Poliwrath
		ExperienceGroup.MediumSlow,	//63 - Abra
		ExperienceGroup.MediumSlow,	//64 - Kadabra
		ExperienceGroup.MediumSlow,	//65 - Alakazam
		ExperienceGroup.MediumSlow,	//66 - Machop
		ExperienceGroup.MediumSlow,	//67 - Machoke
		ExperienceGroup.MediumSlow,	//68 - Machamp
		ExperienceGroup.MediumSlow,	//69 - Bellsprout
		ExperienceGroup.MediumSlow,	//70 - Weepinbell
		ExperienceGroup.MediumSlow,	//71 - Victreebel
		ExperienceGroup.Slow,		//72 - Tentacool
		ExperienceGroup.Slow,		//73 - Tentacruel
		ExperienceGroup.MediumSlow,	//74 - Geodude
		ExperienceGroup.MediumSlow,	//75 - Graveler
		ExperienceGroup.MediumSlow,	//76 - Golem
		ExperienceGroup.MediumFast,	//77 - Ponyta
		ExperienceGroup.MediumFast,	//78 - Rapidash
		ExperienceGroup.MediumFast,	//79 - Slowpoke
		ExperienceGroup.MediumFast,	//80 - Slowbro
		ExperienceGroup.MediumFast,	//81 - Magnemite
		ExperienceGroup.MediumFast,	//82 - Magneton
		ExperienceGroup.MediumFast,	//83 - Farfetch'd
		ExperienceGroup.MediumFast,	//84 - Doduo
		ExperienceGroup.MediumFast,	//85 - Dodrio
		ExperienceGroup.MediumFast,	//86 - Seel
		ExperienceGroup.MediumFast,	//87 - Dewgong
		ExperienceGroup.MediumFast,	//88 - Grimer
		ExperienceGroup.MediumFast,	//89 - Muk
		ExperienceGroup.Slow,		//90 - Shellder
		ExperienceGroup.Slow,		//91 - Cloyster
		ExperienceGroup.MediumSlow,	//92 - Gastly
		ExperienceGroup.MediumSlow,	//93 - Haunter
		ExperienceGroup.MediumSlow,	//94 - Gengar
		ExperienceGroup.MediumFast,	//95 - Onix
		ExperienceGroup.MediumFast,	//96 - Drowzee
		ExperienceGroup.MediumFast,	//97 - Hypno
		ExperienceGroup.MediumFast,	//98 - Krabby
		ExperienceGroup.MediumFast,	//99 - Kingler
		ExperienceGroup.MediumFast,	//100 - Voltorb
		ExperienceGroup.MediumFast,	//101 - Electrode
		ExperienceGroup.Slow,		//102 - Exeggcute
		ExperienceGroup.Slow,		//103 - Exeggutor
		ExperienceGroup.MediumFast,	//104 - Cubone
		ExperienceGroup.MediumFast,	//105 - Marowak
		ExperienceGroup.MediumFast,	//106 - Hitmonlee
		ExperienceGroup.MediumFast,	//107 - Hitmonchan
		ExperienceGroup.MediumFast,	//108 - Lickitung
		ExperienceGroup.MediumFast,	//109 - Koffing
		ExperienceGroup.MediumFast,	//110 - Weezing
		ExperienceGroup.Slow,		//111 - Rhyhorn
		ExperienceGroup.Slow,		//112 - Rhydon
		ExperienceGroup.Fast,		//113 - Chansey
		ExperienceGroup.MediumFast,	//114 - Tangela
		ExperienceGroup.MediumFast,	//115 - Kangaskhan
		ExperienceGroup.MediumFast,	//116 - Horsea
		ExperienceGroup.MediumFast,	//117 - Seadra
		ExperienceGroup.MediumFast,	//118 - Goldeen
		ExperienceGroup.MediumFast,	//119 - Seaking
		ExperienceGroup.Slow,		//120 - Staryu
		ExperienceGroup.Slow,		//121 - Starmie
		ExperienceGroup.MediumFast,	//122 - Mr.Mime
		ExperienceGroup.MediumFast,	//123 - Scyther
		ExperienceGroup.MediumFast,	//124 - Jynx
		ExperienceGroup.MediumFast,	//125 - Electabuzz
		ExperienceGroup.MediumFast,	//126 - Magmar
		ExperienceGroup.Slow,		//127 - Pinsir
		ExperienceGroup.Slow,		//128 - Tauros
		ExperienceGroup.Slow,		//129 - Magikarp
		ExperienceGroup.Slow,		//130 - Gyarados
		ExperienceGroup.Slow,		//131 - Lapras
		ExperienceGroup.MediumFast,	//132 - Ditto
		ExperienceGroup.MediumFast,	//133 - Eevee
		ExperienceGroup.MediumFast,	//134 - Vaporeon
		ExperienceGroup.MediumFast,	//135 - Jolteon
		ExperienceGroup.MediumFast,	//136 - Flareon
		ExperienceGroup.MediumFast,	//137 - Porygon
		ExperienceGroup.MediumFast,	//138 - Omanyte
		ExperienceGroup.MediumFast,	//139 - Omastar
		ExperienceGroup.MediumFast,	//140 - Kabuto
		ExperienceGroup.MediumFast,	//141 - Kabutops
		ExperienceGroup.Slow,		//142 - Aerodactyl
		ExperienceGroup.Slow,		//143 - Snorlax
		ExperienceGroup.Slow,		//144 - Articuno
		ExperienceGroup.Slow,		//145 - Zapdos
		ExperienceGroup.Slow,		//146 - Moltres
		ExperienceGroup.Slow,		//147 - Dratini
		ExperienceGroup.Slow,		//148 - Dragonair
		ExperienceGroup.Slow,		//149 - Dragonite
		ExperienceGroup.Slow,		//150 - Mewtwo
		ExperienceGroup.MediumSlow,	//151 - Mew
	};

	/* An array of base stats for each Pokemon by Pokedex number
	 * Stats are added as {HP, Attack, Defense, Speed, Special} */
	public static readonly Stats[] BaseStats = new Stats[]
	{
		new Stats(0, 0, 0, 0, 0),			//0 - Null Pokemon
		new Stats(45, 49, 49, 45, 65),		//1 - Bulbasaur
		new Stats(60, 62, 63, 60, 80),		//2 - Ivysaur
		new Stats(80, 82, 83, 80, 100),		//3 - Venusaur
		new Stats(39, 52, 43, 65, 50),		//4 - Charmander
		new Stats(58, 64, 58, 80, 65),		//5 - Charmeleon
		new Stats(78, 84, 78, 100, 85),		//6 - Charizard
		new Stats(44, 48, 65, 43, 50),		//7 - Squirtle
		new Stats(59, 63, 80, 58, 65),		//8 - Wartortle
		new Stats(79, 83, 100, 78, 85),		//9 - Blastoise
		new Stats(45, 30, 35, 45, 20),		//10 - Caterpie
		new Stats(50, 20, 55, 30, 25),		//11 - Metapod
		new Stats(60, 45, 50, 70, 80),		//12 - Butterfree
		new Stats(40, 35, 30, 50, 20),		//13 - Weedle
		new Stats(45, 25, 50, 35, 25),		//14 - Kakuna
		new Stats(65, 80, 40, 75, 45),		//15 - Beedrill
		new Stats(40, 45, 40, 56, 35),		//16 - Pidgey
		new Stats(63, 60, 55, 71, 50),		//17 - Pidgeotto
		new Stats(83, 80, 75, 91, 70),		//18 - Pidgeot
		new Stats(30, 56, 35, 72, 25),		//19 - Rattata
		new Stats(55, 81, 60, 97, 50),		//20 - Raticate
		new Stats(40, 60, 30, 70, 31),		//21 - Spearow
		new Stats(65, 90, 65, 100, 61),		//22 - Fearow
		new Stats(35, 60, 44, 55, 40),		//23 - Ekans
		new Stats(60, 85, 69, 80, 65),		//24 - Arbok
		new Stats(35, 55, 30, 90, 50),		//25 - Pikachu
		new Stats(60, 90, 55, 100, 90),		//26 - Raichu
		new Stats(50, 75, 85, 40, 30),		//27 - Sandshrew
		new Stats(75, 100, 110, 65, 55),	//28 - Sandslash
		new Stats(55, 47, 52, 41, 40),		//29 - Nidoran F
		new Stats(70, 62, 67, 56, 55),		//30 - Nidorina
		new Stats(90, 82, 87, 76, 75),		//31 - Nidoqueen
		new Stats(46, 57, 40, 50, 40),		//32 - Nidoran M
		new Stats(61, 72, 57, 65, 55),		//33 - Nidorino
		new Stats(81, 92, 77, 85, 75),		//34 - Nidoking
		new Stats(70, 45, 48, 35, 60),		//35 - Clefairy
		new Stats(95, 70, 73, 60, 85),		//36 - Clefable
		new Stats(38, 41, 40, 65, 65),		//37 - Vulpix
		new Stats(73, 76, 75, 100, 100),	//38 - Ninetales
		new Stats(11, 45, 20, 20, 25),		//39 - Jigglypuff
		new Stats(14, 70, 45, 45, 50),		//40 - Wigglytuff
		new Stats(40, 45, 35, 55, 40),		//41 - Zubat
		new Stats(75, 80, 70, 90, 75),		//42 - Golbat
		new Stats(45, 50, 55, 30, 75),		//43 - Oddish
		new Stats(60, 65, 70, 40, 85),		//44 - Gloom
		new Stats(75, 80, 85, 50, 100),		//45 - Vileplume
		new Stats(35, 70, 55, 25, 45),		//46 - Paras
		new Stats(60, 95, 80, 30, 80),		//47 - Parasect
		new Stats(60, 55, 50, 45, 40),		//48 - Venonat
		new Stats(70, 65, 60, 90, 90),		//49 - Venomoth
		new Stats(10, 55, 25, 95, 45),		//50 - Diglett
		new Stats(35, 80, 50, 120, 70),		//51 - Dugtrio
		new Stats(40, 45, 35, 90, 40),		//52 - Meowth
		new Stats(65, 70, 60, 115, 65),		//53 - Persian
		new Stats(50, 52, 48, 55, 50),		//54 - Psyduck
		new Stats(80, 82, 78, 85, 80),		//55 - Golduck
		new Stats(40, 80, 35, 70, 35),		//56 - Mankey
		new Stats(65, 105, 60, 95, 60),		//57 - Primeape
		new Stats(55, 70, 45, 60, 50),		//58 - Growlithe
		new Stats(90, 110, 80, 95, 80),		//59 - Arcanine
		new Stats(40, 50, 40, 90, 40),		//60 - Poliwag
		new Stats(65, 65, 65, 90, 50),		//61 - Poliwhirl
		new Stats(90, 85, 95, 70, 70),		//62 - Poliwrath
		new Stats(25, 20, 15, 90, 105),		//63 - Abra
		new Stats(40, 35, 30, 105, 120),	//64 - Kadabra
		new Stats(55, 50, 45, 120, 135),	//65 - Alakazam
		new Stats(70, 80, 50, 35, 35),		//66 - Machop
		new Stats(80, 100, 70, 45, 50),		//67 - Machoke
		new Stats(90, 130, 80, 55, 65),		//68 - Machamp
		new Stats(50, 75, 35, 40, 70),		//69 - Bellsprout
		new Stats(65, 90, 50, 55, 85),		//70 - Weepinbell
		new Stats(80, 105, 65, 70, 100),	//71 - Victreebel
		new Stats(40, 40, 35, 70, 100),		//72 - Tentacool
		new Stats(80, 70, 65, 100, 120),	//73 - Tentacruel
		new Stats(40, 80, 100, 20, 30),		//74 - Geodude
		new Stats(55, 95, 115, 35, 45),		//75 - Graveler
		new Stats(80, 110, 130, 45, 55),	//76 - Golem
		new Stats(50, 85, 55, 90, 65),		//77 - Ponyta
		new Stats(65, 100, 70, 105, 80),	//78 - Rapidash
		new Stats(90, 65, 65, 15, 40),		//79 - Slowpoke
		new Stats(95, 75, 110, 30, 80),		//80 - Slowbro
		new Stats(25, 35, 70, 45, 95),		//81 - Magnemite
		new Stats(50, 60, 95, 70, 120),		//82 - Magneton
		new Stats(52, 65, 55, 60, 58),		//83 - Farfetch'd
		new Stats(35, 85, 45, 75, 35),		//84 - Doduo
		new Stats(60, 110, 70, 100, 60),	//85 - Dodrio
		new Stats(65, 45, 55, 45, 70),		//86 - Seel
		new Stats(90, 70, 80, 70, 95),		//87 - Dewgong
		new Stats(80, 80, 50, 25, 40),		//88 - Grimer
		new Stats(10, 105, 75, 50, 65),		//89 - Muk
		new Stats(30, 65, 100, 40, 45),		//90 - Shellder
		new Stats(50, 95, 180, 70, 85),		//91 - Cloyster
		new Stats(30, 35, 30, 80, 100),		//92 - Gastly
		new Stats(45, 50, 45, 95, 115),		//93 - Haunter
		new Stats(60, 65, 60, 110, 130),	//94 - Gengar
		new Stats(35, 45, 160, 70, 30),		//95 - Onix
		new Stats(60, 48, 45, 42, 90),		//96 - Drowzee
		new Stats(85, 73, 70, 67, 115),		//97 - Hypno
		new Stats(30, 105, 90, 50, 25),		//98 - Krabby
		new Stats(50, 130, 115, 75, 50),	//99 - Kingler
		new Stats(40, 30, 50, 100, 55),		//100 - Voltorb
		new Stats(60, 50, 70, 140, 80),		//101 - Electrode
		new Stats(60, 40, 80, 40, 60),		//102 - Exeggcute
		new Stats(95, 95, 85, 55, 125),		//103 - Exeggutor
		new Stats(50, 50, 95, 35, 40),		//104 - Cubone
		new Stats(60, 80, 110, 45, 50),		//105 - Marowak
		new Stats(50, 120, 53, 87, 35),		//106 - Hitmonlee
		new Stats(50, 105, 79, 76, 35),		//107 - Hitmonchan
		new Stats(90, 55, 75, 30, 60),		//108 - Lickitung
		new Stats(40, 65, 95, 35, 60),		//109 - Koffing
		new Stats(65, 90, 120, 60, 85),		//110 - Weezing
		new Stats(80, 85, 95, 25, 30),		//111 - Rhyhorn
		new Stats(105, 130, 120, 40, 45),	//112 - Rhydon
		new Stats(250, 5, 5, 50, 105),		//113 - Chansey
		new Stats(65, 55, 115, 60, 100),	//114 - Tangela
		new Stats(105, 95, 80, 90, 40),		//115 - Kangaskhan
		new Stats(30, 40, 70, 60, 70),		//116 - Horsea
		new Stats(55, 65, 95, 85, 95),		//117 - Seadra
		new Stats(45, 67, 60, 63, 50),		//118 - Goldeen
		new Stats(80, 92, 65, 68, 80),		//119 - Seaking
		new Stats(30, 45, 55, 85, 70),		//120 - Staryu
		new Stats(60, 75, 85, 115, 100),	//121 - Starmie
		new Stats(40, 45, 65, 90, 100),		//122 - Mr.Mime
		new Stats(70, 110, 80, 105, 55),	//123 - Scyther
		new Stats(65, 50, 35, 95, 95),		//124 - Jynx
		new Stats(65, 83, 57, 105, 85),		//125 - Electabuzz
		new Stats(65, 95, 57, 93, 85),		//126 - Magmar
		new Stats(65, 125, 100, 85, 55),	//127 - Pinsir
		new Stats(75, 100, 95, 110, 70),	//128 - Tauros
		new Stats(20, 10, 55, 80, 20),		//129 - Magikarp
		new Stats(95, 125, 79, 81, 100),	//130 - Gyarados
		new Stats(130, 85, 80, 60, 95),		//131 - Lapras
		new Stats(48, 48, 48, 48, 48),		//132 - Ditto
		new Stats(55, 55, 50, 55, 65),		//133 - Eevee
		new Stats(130, 65, 60, 65, 110),	//134 - Vaporeon
		new Stats(65, 65, 60, 130, 110),	//135 - Jolteon
		new Stats(65, 130, 60, 65, 110),	//136 - Flareon
		new Stats(65, 60, 70, 40, 75),		//137 - Porygon
		new Stats(35, 40, 100, 35, 90),		//138 - Omanyte
		new Stats(70, 60, 125, 55, 115),	//139 - Omastar
		new Stats(30, 80, 90, 55, 45),		//140 - Kabuto
		new Stats(60, 115, 105, 80, 70),	//141 - Kabutops
		new Stats(80, 105, 65, 130, 60),	//142 - Aerodactyl
		new Stats(160, 110, 65, 30, 65),	//143 - Snorlax
		new Stats(90, 85, 100, 85, 125),	//144 - Articuno
		new Stats(90, 90, 85, 100, 125),	//145 - Zapdos
		new Stats(90, 100, 90, 90, 125),	//146 - Moltres
		new Stats(41, 64, 45, 50, 50),		//147 - Dratini
		new Stats(61, 84, 65, 70, 70),		//148 - Dragonair
		new Stats(91, 134, 95, 80, 100),	//149 - Dragonite
		new Stats(106, 110, 90, 130, 154),	//150 - Mewtwo
		new Stats(100, 100, 100, 100, 100),	//151 - Mew
	};

	/* An array of experience given when a Pokemon is defeated
	 * Sorted by Pokedex number */
	public static readonly int[] ExpYield = new int[]
	{
		0, 64, 141, 208, 65, 142, 209, 66, 143, 210, 53,	//0 - 10
		72, 160, 52, 71, 159, 55, 113, 172, 57, 116,		//11 - 20
		58, 162, 62, 147, 82, 122, 93, 163, 59, 117,		//21 - 30
		194, 60, 118, 195, 68, 129, 63, 178, 76, 109,		//31 - 40
		54, 171, 78, 132, 184, 70, 128, 75, 138, 81,		//41 - 50
		153, 69, 148, 80, 174, 74, 149, 91, 213, 77,		//51 - 60
		131, 185, 73, 145, 186, 88, 146, 193, 84, 151,		//61 - 70
		191, 105, 205, 86, 134, 177, 152, 192, 99, 164,		//71 - 80
		89, 161, 94, 96, 158, 100, 176, 90, 157, 97,		//81 - 90
		203, 95, 126, 190, 108, 102, 165, 115, 206, 103,	//91 - 100
		150, 98, 212, 87, 124, 139, 140, 127, 114, 173,		//101 - 110
		135, 204, 255, 166, 175, 83, 155, 111, 170, 106,	//111 - 120
		207, 136, 187, 137, 156, 167, 200, 211, 20, 214,	//121 - 130
		219, 61, 92, 196, 197, 198, 130, 120, 199, 119,		//131 - 140
		201, 202, 154, 215, 216, 217, 67, 144, 218, 220, 64	//141 - 151
	};

	/* An array of each Pokemon's base catch rate by Pokedex number
	 * Values are between 0 and 255 */
	public static readonly int[] CatchRate = new int[]
	{
		0, 45, 45, 45, 45, 45, 45, 45, 45, 45, 255,		//0 - 10
		120, 45, 255, 120, 45, 255, 120, 45, 255, 127,	//11 - 20
		255, 90, 255, 90, 190, 75, 255, 90, 235, 120,	//21 - 30
		45, 235, 120, 45, 150, 25, 190, 75, 170, 50,	//31 - 40
		255, 90, 255, 120, 45, 190, 75, 190, 75, 255,	//41 - 50
		50, 255, 90, 190, 75, 190, 75, 190, 75, 255,	//51 - 60
		120, 45, 200, 100, 50, 180, 90, 45, 255, 120,	//61 - 70
		45, 190, 60, 255, 120, 45, 190, 60, 190, 75,	//71 - 80
		190, 60, 45, 190, 45, 190, 75, 190, 75, 190,	//81 - 90
		60, 190, 90, 45, 45, 190, 75, 255, 60, 190,		//91 - 100
		60, 90, 45, 190, 75, 45, 45, 45, 190, 60,		//101 - 110
		120, 60, 30, 45, 45, 225, 75, 225, 60, 225,		//111 - 120
		60, 45, 45, 45, 45, 45, 45, 45, 255, 45,		//121 - 130
		45, 35, 45, 45, 45, 45, 45, 45, 45, 45,			//131 - 140
		45, 45, 25, 3, 3, 3, 45, 45, 45, 3, 45			//141 - 151
	};

    /* An array of all possible mini sprites
    //These are usually displayed in a menu */
    private static readonly Sprite[] MiniSprites = Resources.LoadAll<Sprite>("Sprites/Pokemon/Mini Sprites");

    //An array of each Pokemon's front sprites by Pokedex number
    private static readonly Sprite[] FrontSprites = Resources.LoadAll<Sprite>("Sprites/Pokemon/Kanto Pokemon");

	//An array of each Pokemon's back sprites by Pokedex number
	private static readonly Sprite[] BackSprites = Resources.LoadAll<Sprite>("Sprites/Pokemon/Kanto Pokemon Back");

	/* An array of each Pokemon's sprites by Pokedex number
	 * Format is {MiniSprite1, MiniSprite2, FrontSprite, BackSprite} */
	public static readonly Sprite[][] Sprites = new Sprite[][]
	{
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[0], BackSprites[0]},		//0 - Null Pokemon
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[1], BackSprites[1] },		//1 - Bulbasaur
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[2], BackSprites[2] },		//2 - Ivysaur
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[3], BackSprites[3] },		//3 - Venusaur
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[4], BackSprites[4] },		//4 - Charmander
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[5], BackSprites[5] },		//5 - Charmeleon
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[6], BackSprites[6] },		//6 - Charizard
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[7], BackSprites[7] },		//7 - Squirtle
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[8], BackSprites[8] },		//8 - Wartortle
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[9], BackSprites[9] },		//9 - Blastoise
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[10], BackSprites[10] },		//10 - Caterpie
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[11], BackSprites[11] },		//11 - Metapod
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[12], BackSprites[12] },		//12 - Butterfree
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[13], BackSprites[13] },		//13 - Weedle
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[14], BackSprites[14] },		//14 - Kakuna
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[15], BackSprites[15] },		//15 - Beedrill
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[16], BackSprites[16] },		//16 - Pidgey
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[17], BackSprites[17] },		//17 - Pidgeotto
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[18], BackSprites[18] },		//18 - Pidgeot
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[19], BackSprites[19] },		//19 - Rattata
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[20], BackSprites[20] },		//20 - Raticate
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[21], BackSprites[21] },		//21 - Spearow
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[22], BackSprites[22] },		//22 - Fearow
		new Sprite[] { MiniSprites[14], MiniSprites[15], FrontSprites[23], BackSprites[23] },	//23 - Ekans
		new Sprite[] { MiniSprites[14], MiniSprites[15], FrontSprites[24], BackSprites[24] },	//24 - Arbok
		new Sprite[] { MiniSprites[7], MiniSprites[8], FrontSprites[25], BackSprites[25] },		//25 - Pikachu
		new Sprite[] { MiniSprites[7], MiniSprites[8], FrontSprites[26], BackSprites[26] },		//26 - Raichu
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[27], BackSprites[27] },	//27 - Sandshrew
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[28], BackSprites[28] },	//28 - Sandslash
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[29], BackSprites[29] },	//29 - Nidoran F
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[30], BackSprites[30] },	//30 - Nidorina
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[31], BackSprites[31] },	//31 - Nidoqueen
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[32], BackSprites[32] },	//32 - Nidoran M
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[33], BackSprites[33] },	//33 - Nidorino
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[34], BackSprites[34] },	//34 - Nidoking
		new Sprite[] { MiniSprites[7], MiniSprites[8], FrontSprites[35], BackSprites[35] },		//35 - Clefairy
		new Sprite[] { MiniSprites[7], MiniSprites[8], FrontSprites[36], BackSprites[36] },		//36 - Clefable
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[37], BackSprites[37] },		//37 - Vulpix
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[38], BackSprites[38] },		//38 - Ninetales
		new Sprite[] { MiniSprites[7], MiniSprites[8], FrontSprites[39], BackSprites[39] },		//39 - Jigglypuff
		new Sprite[] { MiniSprites[7], MiniSprites[8], FrontSprites[40], BackSprites[40] },		//40 - Wigglytuff
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[41], BackSprites[41] },	//41 - Zubat
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[42], BackSprites[42] },	//42 - Golbat
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[43], BackSprites[43] },	//43 - Oddish
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[44], BackSprites[44] },	//44 - Gloom
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[45], BackSprites[45] },	//45 - Vileplume
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[46], BackSprites[46] },		//46 - Paras
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[47], BackSprites[47] },		//47 - Parasect
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[48], BackSprites[48] },		//48 - Venonat
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[49], BackSprites[49] },		//49 - Venomoth
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[50], BackSprites[50] },	//50 - Diglett
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[51], BackSprites[51] },	//51 - Dugtrio
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[52], BackSprites[52] },	//52 - Meowth
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[53], BackSprites[53] },	//53 - Persian
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[54], BackSprites[54] },	//54 - Psyduck
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[55], BackSprites[55] },	//55 - Golduck
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[56], BackSprites[56] },	//56 - Mankey
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[57], BackSprites[57] },	//57 - Primeape
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[58], BackSprites[58] },		//58 - Growlithe
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[59], BackSprites[59] },		//59 - Arcanine
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[60], BackSprites[60] },	//60 - Poliwag
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[61], BackSprites[61] },	//61 - Poliwhirl
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[62], BackSprites[62] },	//62 - Poliwrath
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[63], BackSprites[63] },	//63 - Abra
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[64], BackSprites[64] },	//64 - Kadabra
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[65], BackSprites[65] },	//65 - Alakazam
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[66], BackSprites[66] },	//66 - Machop
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[67], BackSprites[67] },	//67 - Machoke
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[68], BackSprites[68] },	//68 - Machamp
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[69], BackSprites[69] },	//69 - Bellsprout
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[70], BackSprites[70] },	//70 - Weepinbell
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[71], BackSprites[71] },	//71 - Victreebel
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[72], BackSprites[72] },	//72 - Tentacool
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[73], BackSprites[73] },	//73 - Tentacruel
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[74], BackSprites[74] },	//74 - Geodude
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[75], BackSprites[75] },	//75 - Graveler
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[76], BackSprites[76] },	//76 - Golem
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[77], BackSprites[77] },		//77 - Ponyta
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[78], BackSprites[78] },		//78 - Rapidash
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[79], BackSprites[79] },		//79 - Slowpoke
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[80], BackSprites[80] },	//80 - Slowbro
		new Sprite[] { MiniSprites[0], MiniSprites[0], FrontSprites[81], BackSprites[81] },		//81 - Magnemite
		new Sprite[] { MiniSprites[0], MiniSprites[0], FrontSprites[82], BackSprites[82] },		//82 - Magneton
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[83], BackSprites[83] },		//83 - Farfetch'd
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[84], BackSprites[84] },		//84 - Doduo
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[85], BackSprites[85] },		//85 - Dodrio
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[86], BackSprites[86] },	//86 - Seel
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[87], BackSprites[87] },	//87 - Dewgong
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[88], BackSprites[88] },	//88 - Grimer
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[89], BackSprites[89] },	//89 - Muk
		new Sprite[] { MiniSprites[13], MiniSprites[13], FrontSprites[90], BackSprites[90] },	//90 - Shellder
		new Sprite[] { MiniSprites[13], MiniSprites[13], FrontSprites[91], BackSprites[91] },	//91 - Cloyster
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[92], BackSprites[92] },	//92 - Gastly
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[93], BackSprites[93] },	//93 - Haunter
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[94], BackSprites[94] },	//94 - Gengar
		new Sprite[] { MiniSprites[14], MiniSprites[15], FrontSprites[95], BackSprites[95] },	//95 - Onix
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[96], BackSprites[96] },	//96 - Drowzee
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[97], BackSprites[97] },	//97 - Hypno
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[98], BackSprites[98] },	//98 - Krabby
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[99], BackSprites[99] },	//99 - Kingler
		new Sprite[] { MiniSprites[0], MiniSprites[0], FrontSprites[100], BackSprites[100] },	//100 - Voltorb
		new Sprite[] { MiniSprites[0], MiniSprites[0], FrontSprites[101], BackSprites[101] },	//101 - Electrode
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[102], BackSprites[102] },	//102 - Exeggcute
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[103], BackSprites[103] },	//103 - Exeggutor
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[104], BackSprites[104] },	//104 - Cubone
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[105], BackSprites[105] },	//105 - Marowak
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[106], BackSprites[106] },	//106 - Hitmonlee
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[107], BackSprites[107] },	//107 - Hitmonchan
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[108], BackSprites[108] },	//108 - Lickitung
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[109], BackSprites[109] },	//109 - Koffing
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[110], BackSprites[110] },	//110 - Weezing
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[111], BackSprites[111] },	//111 - Rhyhorn
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[112], BackSprites[112] },	//112 - Rhydon
		new Sprite[] { MiniSprites[7], MiniSprites[8], FrontSprites[113], BackSprites[113] },	//113 - Chansey
		new Sprite[] { MiniSprites[11], MiniSprites[12], FrontSprites[114], BackSprites[114] },	//114 - Tangela
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[115], BackSprites[115] },	//115 - Kangaskhan
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[116], BackSprites[116] },	//116 - Horsea
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[117], BackSprites[117] },	//117 - Seadra
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[118], BackSprites[118] },	//118 - Goldeen
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[119], BackSprites[119] },	//119 - Seaking
		new Sprite[] { MiniSprites[13], MiniSprites[13], FrontSprites[120], BackSprites[120] },	//120 - Staryu
		new Sprite[] { MiniSprites[13], MiniSprites[13], FrontSprites[121], BackSprites[121] },	//121 - Starmie
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[122], BackSprites[122] },	//122 - Mr.Mime
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[123], BackSprites[123] },	//123 - Scyther
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[124], BackSprites[124] },	//124 - Jynx
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[125], BackSprites[125] },	//125 - Electabuzz
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[126], BackSprites[126] },	//126 - Magmar
		new Sprite[] { MiniSprites[3], MiniSprites[4], FrontSprites[127], BackSprites[127] },	//127 - Pinsir
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[128], BackSprites[128] },	//128 - Tauros
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[129], BackSprites[129] },	//129 - Magikarp
		new Sprite[] { MiniSprites[14], MiniSprites[15], FrontSprites[130], BackSprites[130] },	//130 - Gyarados
		new Sprite[] { MiniSprites[16], MiniSprites[17], FrontSprites[131], BackSprites[131] },	//131 - Lapras
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[132], BackSprites[132] },	//132 - Ditto
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[133], BackSprites[133] },	//133 - Eevee
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[134], BackSprites[134] },	//134 - Vaporeon
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[135], BackSprites[135] },	//135 - Jolteon
		new Sprite[] { MiniSprites[5], MiniSprites[6], FrontSprites[136], BackSprites[136] },	//136 - Flareon
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[137], BackSprites[137] },	//137 - Porygon
		new Sprite[] { MiniSprites[13], MiniSprites[13], FrontSprites[138], BackSprites[138] },	//138 - Omanyte
		new Sprite[] { MiniSprites[13], MiniSprites[13], FrontSprites[139], BackSprites[139] },	//139 - Omastar
		new Sprite[] { MiniSprites[13], MiniSprites[13], FrontSprites[140], BackSprites[140] },	//140 - Kabuto
		new Sprite[] { MiniSprites[13], MiniSprites[13], FrontSprites[141], BackSprites[141] },	//141 - Kabutops
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[142], BackSprites[142] },	//142 - Aerodactyl
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[143], BackSprites[143] },	//143 - Snorlax
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[144], BackSprites[144] },	//144 - Articuno
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[145], BackSprites[145] },	//145 - Zapdos
		new Sprite[] { MiniSprites[1], MiniSprites[2], FrontSprites[146], BackSprites[146] },	//146 - Moltres
		new Sprite[] { MiniSprites[14], MiniSprites[15], FrontSprites[147], BackSprites[147] },	//147 - Dratini
		new Sprite[] { MiniSprites[14], MiniSprites[15], FrontSprites[148], BackSprites[148] },	//148 - Dragonair
		new Sprite[] { MiniSprites[14], MiniSprites[15], FrontSprites[149], BackSprites[149] },	//149 - Dragonite
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[150], BackSprites[150] },	//150 - Mewtwo
		new Sprite[] { MiniSprites[9], MiniSprites[10], FrontSprites[151], BackSprites[151] },	//151 - Mew
	};

	//An array of each Pokemon's Pokedex category by Pokedex number
	public static readonly string[] Categories = new string[]
	{
		"NONE", "SEED", "SEED", "SEED", "LIZARD", "FLAME",			//0 - 5
		"FLAME", "TINYTURTLE", "TURTLE", "SHELLFISH", "WORM",		//6 - 10
		"COCOON", "BUTTERFLY", "HAIRY BUG", "COCOON", "POISON BEE",	//11 - 15
		"TINY BIRD", "BIRD", "BIRD", "RAT", "RAT",					//16 - 20
		"TINY BIRD", "BEAK", "SNAKE", "COBRA", "MOUSE",				//21 - 25
		"MOUSE", "MOUSE", "MOUSE", "POISON PIN", "POISON PIN",		//26 - 30
		"DRILL", "POISON PIN", "POISON PIN", "DRILL", "FAIRY",		//31 - 35
		"FAIRY", "FOX", "FOX", "BALLOON", "BALLOON",				//36 - 40
		"BAT", "BAT", "WEED", "WEED", "FLOWER",						//41 - 45
		"MUSHROOM", "MUSHROOM", "INSECT", "POISONMOTH", "MOLE",		//46 - 50
		"MOLE", "SCRATCHCAT", "CLASSY CAT", "DUCK", "DUCK",			//51 - 55
		"PIG MONKEY", "PIG MONKEY", "PUPPY", "LEGENDARY", "TADPOLE",//56 - 60
		"TADPOLE", "TADPOLE", "PSI", "PSI", "PSI",					//61 - 65
		"SUPERPOWER", "SUPERPOWER", "SUPERPOWER", "FLOWER", "FLYCATCHER",	//66 - 70
		"FLYCATCHER", "JELLYFISH", "JELLYFISH", "ROCK", "ROCK",		//71 - 75
		"MEGATON", "FIRE HORSE", "FIRE HORSE", "DOPEY", "HERMITCRAB",//76 - 80
		"MAGNET", "MAGNET", "WILD DUCK", "TWIN BIRD", "TRIPLEBIRD",	//81 - 85
		"SEA LION", "SEA LION", "SLUDGE", "SLUDGE", "BIVALVE",		//86 - 90
		"BIVALVE", "GAS", "GAS", "SHADOW", "ROCKSNAKE",				//91 - 95
		"HYPNOSIS", "HYPNOSIS", "RIVER CRAB", "PINCER", "BALL",		//96 - 100
		"BALL", "EGG", "COCONUT", "LONELY", "BONEKEEPER",			//101 - 105
		"KICKING", "PUNCHING", "LICKING", "POISON GAS", "POISON GAS",//106 - 110
		"SPIKES", "DRILL", "EGG", "VINE", "PARENT",					//111 - 115
		"DRAGON", "DRAGON", "GOLDFISH", "GOLDFISH", "STARSHAPE",	//116 - 120
		"MYSTERIOUS", "BARRIER", "MANTIS", "HUMANSHAPE", "ELECTRIC",//121 - 125
		"SPITFIRE", "STAGBEETLE", "WILD BULL", "FISH", "ATROCIOUS",	//126 - 130
		"TRANSPORT", "TRANSFORM", "EVOLUTION", "BUBBLE JET", "LIGHTNING",	//131 - 135
		"FLAME", "VIRTUAL", "SPIRAL", "SPIRAL", "SHELLFISH",		//136 - 140
		"SHELLFISH", "FOSSIL", "SLEEPING", "FREEZE", "ELECTRIC",	//141 - 145
		"FLAME", "DRAGON", "DRAGON", "DRAGON", "GENETIC", "NEW SPECIE"	//146 - 151
	};

	//An array of each Pokemon's height by Pokedex number
	public static readonly int[] Heights = new int[]
	{
		0, 28, 39, 79, 24, 43, 67, 20, 39, 63, 12,	//0 - 10
		28, 43, 12, 24, 39, 12, 43, 59, 12, 28,		//11 - 20
		12, 47, 79, 138, 16, 31, 24, 39, 16, 31,	//21 - 30
		51, 20, 35, 55, 24, 51, 24, 43, 20, 39,		//31 - 40
		31, 63, 20, 31, 47, 12, 39, 39, 59, 8,		//41 - 50
		28, 16, 39, 31, 67, 20, 39, 28, 78, 24,		//51 - 60
		39, 51, 35, 51, 59, 31, 59, 63, 28, 39,		//61 - 70
		67, 35, 63, 16, 39, 55, 39, 67, 47, 63,		//71 - 80
		12, 39, 31, 55, 71, 43, 67, 35, 47, 12,		//81 - 90
		59, 51, 63, 59, 346, 39, 63, 16, 51, 20,	//91 - 100
		47, 16, 79, 16, 39, 59, 55, 47, 24, 47,		//101 - 110
		39, 75, 43, 39, 87, 16, 47, 24, 51, 31,		//111 - 120
		43, 51, 59, 55, 43, 51, 59, 55, 35, 256,	//121 - 130
		98, 12, 12, 39, 31, 35, 31, 16, 39, 20,		//131 - 140
		51, 71, 83, 67, 63, 79, 71, 157, 85, 79, 16	//141 - 151
	};

	//An array of each Pokemon's wieght by Pokedex number
	public static readonly float[] Weights = new float[]
	{
		0, 15, 29, 221, 19, 42, 200, 20, 50, 189, 6,	//0 - 10
		22, 71, 7, 22, 65, 4, 66, 87, 8, 41,			//11 - 20
		4, 84, 15, 143, 13, 66, 26, 65, 15, 44,			//21 - 30
		132, 20, 43, 137, 17, 88, 22, 44, 12, 26,		//31 - 40
		17, 121, 12, 19, 41, 12, 65, 66, 28, 2,			//41 - 50
		73, 9, 71, 43, 169, 62, 71, 42, 342, 27,		//51 - 60
		44, 119, 43, 125, 106, 43, 155, 287, 9, 14,		//61 - 70
		34, 100, 121, 44, 232, 662, 66, 209, 79, 173,	//71 - 80
		13, 132, 33, 86, 188, 198, 265, 66, 66, 9,		//81 - 90
		292, 0.2f, 0.2f, 89, 463, 71, 167, 14, 132, 23,	//91 - 100
		147, 6, 265, 14, 99, 110, 111, 144, 2, 21,		//101 - 110
		254, 265, 76, 77, 176, 18, 55, 33, 86, 76,		//111 - 120
		176, 120, 123, 90, 66, 98, 121, 195, 22, 518,	//121 - 130
		485, 9, 14, 64, 54, 55, 80, 17, 77, 25,			//131 - 140
		89, 130, 1014, 122, 116, 132, 7, 36, 463, 269, 9//141 - 151
	};

	//An array of each Pokemon's Pokedex description by Pokedex number
	public static readonly string[][] Descriptions = new string[][]
	{
		new string[] {"", ""},	//0 - Null Pokemon
		new string[] {"A strange seed was planted on its back at birth.", "The plant sprouts and grows with this POKÈMON."},		//1 - Bulbasaur
		new string[] {"When the bulb on its back grows large, it appears", "to lose the ability to stand on its hind legs."},		//2 - Ivysaur
		new string[] {"The plant blooms when it is absorbing solar", "energy. It stays on the move to seek sunlight."},				//3 - Venusaur
		new string[] {"Obviously prefers hot places. When it rains, steam", "is said to spout from the tip of its tail."},			//4 - Charmander
		new string[] {"When it swings its burning tail, it elevates the", "temperature to unbearably high levels."},				//5 - Charmeleon
		new string[] {"Spits fire that is hot enough to melt boulders.", "Known to cause forest fires unintentionally."},			//6 - Charizard
		new string[] {"After birth, its back swells and hardens into a", "shell. Powerfully sprays foam from its mouth."},			//7 - Squirtle
		new string[] {"Often hides in water to stalk unwary prey. For", "swimming fast, it moves its ears to maintain balance."},	//8 - Wartortle
		new string[] {"A brutal POKÈMON with pressurized water jets on its", "shell. They are used for high speed tackles."},		//9 - Blastoise
		new string[] {"Its short feet are tipped with suction pads that", "enable it to tirelessly climb slopes and walls."},		//10 - Caterpie
		new string[] {"This POKÈMON is vulnerable to attack while its", "shell is soft, exposing its weak and tender body."},		//11 - Metapod
		new string[] {"In battle, it flaps its wings at high speed to", "release highly toxic dust into the air."},					//12 - Butterfree
		new string[] {"Often found in forests, eating leaves.", "It has a sharp venomous stinger on its head."},					//13 - Weedle
		new string[] {"Almost incapable of moving, this POKÈMON can only", "harden its shell to protect itself from predators."},	//14 - Kakuna
		new string[] {"Flies at high speed and attacks using its large", "venomous stingers on its forelegs and tail."},			//15 - Beedrill
		new string[] {"A common sight in forests and woods. It flaps its", "wings at ground level to kick up blinding sand."},		//16 - Pidgey
		new string[] {"Very protective of its sprawling territorial area,", "this POKÈMON will fiercely peck at any intruder."},	//17 - Pidgeotto
		new string[] {"When hunting, it skims the surface of water at high", "speed to pick off unwary prey such as MAGIKARP."},	//18 - Pidgeot
		new string[] {"Bites anything when it attacks. Small and very", "quick, it is a common sight in many places."},				//19 - Rattata
		new string[] {"It uses its whis-\nkers to maintain its balance.", "It apparently slows down if they are cut off."},			//20 - Raticate
		new string[] {"Eats bugs in grassy areas. It has to flap its", "short wings at high speed to stay airborne."},				//21 - Spearow
		new string[] {"With its huge and magnificent wings, it can keep aloft", "without ever having to land for rest."},			//22 - Fearow
		new string[] {"Moves silently and stealthily. Eats the eggs of", "birds, such as PIDGEY and SPEAROW, whole."},				//23 - Ekans
		new string[] {"It is rumored that the ferocious warning markings", "on its belly differ from area to area."},				//24 - Arbok
		new string[] {"When several of these POKÈMON gather, their", "electricity could build and cause lightning storms."},		//25 - Pikachu
		new string[] {"Its long tail serves as a ground to protect", "itself from its own high voltage power."},					//26 - Raichu
		new string[] {"Burrows deep underground in arid locations", "far from water. It only emerges to hunt for food."},			//27 - Sandshrew
		new string[] {"Curls up into a spiny ball when threatened. It", "can roll while curled up to attack or escape."},			//28 - Sandslash
		new string[] {"Although small, its venomous barbs render this", "POKÈMON dangerous. The female has smaller horns."},		//29 - Nidoran F
		new string[] {"The female< horn develops slowly. Prefers physical", "attacks such as clawing and biting."},					//30 - Nidorina
		new string[] {"Its hard scales provide strong protection. It", "uses its hefty bulk to execute powerful moves."},			//31 - Nidoqueen
		new string[] {"Stiffens its ears to sense danger. The larger its", "horns, the more powerful its secreted venom."},			//32 - Nidoran M
		new string[] {"An aggressive POKÈMON that is quick to attack.", "The horn on its head secretes a powerful venom."},			//33 - Nidorino
		new string[] {"It uses its powerful tail in battle to smash,", "constrict, then break the prey< bones."},					//34 - Nidoking
		new string[] {"Its magical and cute appeal has many admirers.", "It is rare and found only in certain areas."},				//35 - Clefairy
		new string[] {"A timid fairy POKÈMON that is rarely seen. It", "will run and hide the moment it senses people."},			//36 - Clefable
		new string[] {"At the time of birth, it has just one tail.", "The tail splits from its tip as it grows older"},				//37 - Vulpix
		new string[] {"Very smart and very vengeful. Grabbing one of", "its many tails could result in a 1000-year curse."},		//38 - Ninetales
		new string[] {"When its huge eyes light up, it sings a mysteriously", "soothing melody that lulls its enemies to sleep."},	//39 - Jigglypuff
		new string[] {"The body is soft and rubbery. When angered, it will", "suck in air and inflate itself to an enormous size."},//40 - Wigglytuff
		new string[] {"Forms colonies in perpetually dark places. Uses", "ultrasonic waves to identify and approach targets."},		//41 - Zubat
		new string[] {"Once it strikes, it will not stop draining energy,", "from the victim even if it gets too heavy to fly."},	//42 - Golbat
		new string[] {"During the day, it keeps its face buried in the", "ground. At night, it wanders around sowing its seeds."},	//43 - Oddish
		new string[] {"The fluid that oozes from its mouth isn= drool.", "It is a nectar that is used to attract prey."},			//44 - Gloom
		new string[] {"The larger its petals, the more toxic pollen it", "contains. Its big head is heavy and hard to hold up."},	//45 - Vileplume
		new string[] {"Burrows to suck tree roots. The mushrooms on its", "back grow by draw-\ning nutrients from the bug host"},	//46 - Paras
		new string[] {"A host-parasite pair in which the parasite mushroom", "has taken over the host bug. Prefers damp places."},	//47 - Parasect
		new string[] {"Lives in the shadows of tall trees where it", "eats insects. It is attracted by light at night."},			//48 - Venonat
		new string[] {"The dust-like scales covering its wings are", "color coded to indicate the kinds of poison it has."},		//49 - Venomoth
		new string[] {"Lives about one yard underground where it feeds on", "plant roots. It sometimes appears above ground."},		//50 - Diglett
		new string[] {"A team of DIGLETT triplets. It triggers huge", "earthquakes by burrowing 60 miles underground."},			//51 - Dugtrio
		new string[] {"Adores circular objects. Wanders the streets on a", "nightly basis to look for dropped loose change."},		//52 - Meowth
		new string[] {"Although its fur has many admirers, it is tough to", "raise as a pet because of its fickle meanness."},		//53 - Persian
		new string[] {"While lulling its enemies with its vacant look, this", "wily POkÈMON will use psychokinetic powers."},		//54 - Psyduck
		new string[] {"Often seen swim-\nming elegantly by lake shores. It", "is often mistaken for the Japanese monster, Kappa."},	//55 - Golduck
		new string[] {"Extremely quick to anger. It could be docile one", "moment then thrashing away the next instant."},			//56 - Mankey
		new string[] {"Always furious and tenacious to boot. It will not", "abandon chasing its quarry until it is caught."},		//57 - Primeape
		new string[] {"Very protective of its territory. It will bark and", "bite to repel intruders from its space."},				//58 - Growlithe
		new string[] {"A POKÈMON that has been admired since the past", "for its beauty. It runs agilely as if on wings."},			//59 - Arcanine
		new string[] {"Its newly grown legs prevent it from running. It", "appears to prefer swimming than trying to stand."},		//60 - Poliwag
		new string[] {"Capable of living in or out of water. When out", "of water, it sweats to keep its body slimy."},				//61 - Poliwhirl
		new string[] {"An adept swimmer at both the front crawl and breast", "stroke. Easily overtakes the best human swimmers."},	//62 - Poliwrath
		new string[] {"Using its ability to read minds, it will identify", "impending danger and TELEPORT to safety."},				//63 - Abra
		new string[] {"It emits special alpha waves from its body that,", "induce headaches just by being close by."},				//64 - Kadabra
		new string[] {"Its brain can out-\nperform a super-\ncomputer.", "Its intelligence quotient is said to be 5,000."},			//65 - Alakazam
		new string[] {"Loves to build its muscles. It trains in all", "styles of martial arts to become even stronger."},			//66 - Machop
		new string[] {"Its muscular body is so powerful, it must wear a power", "save belt to be able to regulate its motions."},	//67 - Machoke
		new string[] {"Using its heavy muscles, it throws powerful punches", "that can send the victim clear over the horizon."},	//68 - Machamp
		new string[] {"A carnivorous POKÈMON that traps and eats bugs.", "It uses its root feet to soak up needed moisture."},		//69 - Bellsprout
		new string[] {"It spits out POISONPOWDER to immobilize the", "enemy and then finishes it with a spray of ACID."},			//70 - Weepinbell
		new string[] {"Said to live in huge colonies deep in jungles", "although no one has ever returned from there."},			//71 - Victreebel
		new string[] {"Drifts in shallow seas. Anglers who hook them by", "accident are often punished by its stinging acid."},		//72 - Tentacool
		new string[] {"The tentacles are normally kept short. On hunts", "they are extended to ensnare and immobilize prey."},		//73 - Tentacruel
		new string[] {"Found in fields and mountains. Mistaking them", "for boulders, people often step or trip on them."},			//74 - Geodude
		new string[] {"Rolls down slopes to move. It rolls over any obstacle", "without slowing or changing its direction."},		//75 - Graveler
		new string[] {"Its boulder-like body is extremely hard. It can", "easily withstand dynamite blasts without damage."},		//76 - Golem
		new string[] {"Its hooves are 10 times harder than diamonds. It can", "trample anything completely flat in little time."},	//77 - Ponyta
		new string[] {"Very competitive, this POKÈMON will chase anything", "that moves fast in the hopes of racing it."},			//78 - Rapidash
		new string[] {"Incredibly slow and dopey. It takes 5 seconds", "for it to feel pain when under attack."},					//79 - Slowpoke
		new string[] {"The SHELLDER that is latched onto SLOWPOKE< tail", "is said to feed on the host< left over scraps."},		//80 - Slowbro
		new string[] {"Uses anti-gravity to stay suspended. Appears without", "warning and uses THUNDER WAVE and similar moves."},	//81 - Magnemite
		new string[] {"Formed by several MAGNEMITEs linked together. They", "frequently appear when sunspots flare up."},			//82 - Magneton
		new string[] {"The sprig of green onions it holds is its", "weapon. It is used much like a metal sword."},					//83 - Farfetch'd
		new string[] {"A bird that makes up for its poor flying with its", "fast foot speed. Leaves giant footprints."},			//84 - Doduo
		new string[] {"Uses its three brains to execute complex plans.", "While two heads sleep, one head stays awake."},			//85 - Dodrio
		new string[] {"The protruding horn on its head is very hard.", "It is used for bashing through thick ice."},				//86 - Seel
		new string[] {"Stores thermal energy in its body. Swims at a", "steady 8 knots even in intensely cold waters."},			//87 - Dewgong
		new string[] {"Appears in filthy areas. Thrives by sucking up", "polluted sludge that is pumped out of factories."},		//88 - Grimer
		new string[] {"Thickly covered with a filthy, vile sludge. It", "is so toxic, even its footprints contain poison."},		//89 - Muk
		new string[] {"Its hard shell repels any kind of attack.", "It is vulnerable only when its shell is open."},				//90 - Shellder
		new string[] {"When attacked, it launches its horns in quick", "volleys. Its innards have never been seen."},				//91 - Cloyster
		new string[] {"Almost invisible, this gaseous POKÈMON cloaks", "the target and puts it to sleep without notice."},			//92 - Gastly
		new string[] {"Because of its ability to slip through block", "walls, it is said to be from an-\nother dimension."},		//93 - Haunter
		new string[] {"Under a full moon, this POKÈMON likes to mimic", "the shadows of people and laugh at their fright."},		//94 - Gengar
		new string[] {"As it grows, the stone portions of its body harden", "to become similar to a diamond, but colored black."},	//95 - Onix
		new string[] {"Puts enemies to sleep then eats their dreams.", "Occasionally gets sick from eating bad dreams."},			//96 - Drowzee
		new string[] {"When it locks eyes with an enemy, it will use a mix of", "PSI moves such as HYPNOSIS and CONFUSION."},		//97 - Hypno
		new string[] {"Its pincers are not only powerful weapons, they are", "used for balance when walking sideways."},			//98 - Krabby
		new string[] {"The large pincer has 10000 hp of crushing power.", "However, its huge size makes it unwieldy to use."},		//99 - Kingler
		new string[] {"Usually found in power plants. Easily mistaken", "for a POKÈ BALL, they have zapped many people."},			//100 - Voltorb
		new string[] {"It stores electric energy under very high pressure.", "It often explodes with little or no provocation."},	//101 - Electrode
		new string[] {"Often mistaken for eggs. When disturbed,", "they quickly gather and attack in swarms."},						//102 - Exeggcute
		new string[] {"Legend has it that on rare occasions, one of its heads", "will drop off and continue on as an EXEGGCUTE."},	//103 - Exeggutor
		new string[] {"Because it never removes its skull helmet, no one", "has ever seen this POKÈMON< real face."},				//104 - Cubone
		new string[] {"The bone it holds is its key weapon. It throws the", "bone skillfully like a boomerang to KO targets."},		//105 - Marowak
		new string[] {"When in a hurry, its legs lengthen progressively.", "It runs smoothly with extra long, loping strides."},	//106 - Hitmonlee
		new string[] {"While apparently doing nothing, it fires punches in", "lightning fast volleys that are impossible to see."},	//107 - Hitmonchan
		new string[] {"Its tongue can be extended like a chameleon<. It", "leaves a tingling sensation when it licks enemies."},	//108 - Lickitung
		new string[] {"Because it stores several kinds of toxic gases in", "its body, it is prone to exploding without warning."},	//109 - Koffing
		new string[] {"Where two kinds of poison gases meet, 2 KOFFINGs", "can fuse into a WEEZING over many years."},				//110 - Weezing
		new string[] {"Its massive bones are 1000 times harder than human", "bones. It can easily knock a trailer flying."},		//111 - Rhyhorn
		new string[] {"Protected by an armor-like hide, it is capable of", "living in molten lava of 3,600 degrees."},				//112 - Rhydon
		new string[] {"A rare and elusive POKÈMON that is said to bring", "happiness to those who manage to get it."},				//113 - Chansey
		new string[] {"The whole body is swathed with wide vines that are", "similar to sea-\nweed. Its vines shake as it walks."},	//114 - Tangela
		new string[] {"The infant rarely ventures out of its mother<", "protective pouch until it is 3 years old."},				//115 - Kangaskhan
		new string[] {"Known to shoot down flying bugs with precision", "blasts of ink from the surface of the water."},			//116 - Horsea
		new string[] {"Capable of swim-\nming backwards by rapidly flapping", "its wing-like pectoral fins and stout tail."},		//117 - Seadra
		new string[] {"Its tail fin billows like an elegant ballroom", "dress, giving it the nickname of the Water Queen."},		//118 - Goldeen
		new string[] {"In the autumn spawning season, they can be seen", "swimming power- fully up rivers and creeks."},			//119 - Seaking
		new string[] {"An enigmatic POKÈMON that can effortlessly", "regenerate any appendage it loses in battle."},				//120 - Staryu
		new string[] {"Its central core glows with the seven colors of", "the rainbow. Some people value the core as a gem."},		//121 - Starmie
		new string[] {"If interrupted while it is miming, it will", "slap around the offender with its broad hands."},				//122 - Mr. Mime
		new string[] {"With ninja-like agility and speed, it can create the", "illusion that there is more than one."},				//123 - Scyther
		new string[] {"It seductively wiggles its hips as it walks. It", "can cause people to dance in unison with it."},			//124 - Jynx
		new string[] {"Normally found near power plants, they can wander", "away and cause major blackouts in cities."},			//125 - Electabuzz
		new string[] {"Its body always burns with an orange glow that", "enables it to hide perfectly among flames."},				//126 - Magmar
		new string[] {"If it fails to crush the victim in its pincers,", "it will swing it around and toss it hard."},				//127 - Pinsir
		new string[] {"When it targets an enemy, it charges furiously", "while whipping its body with its long tails."},			//128 - Tauros
		new string[] {"In the distant past, it was somewhat stronger", "than the horribly weak descendants that exist today."},		//129 - Magikarp
		new string[] {"Rarely seen in the wild. Huge and vicious, it", "is capable of destroying entire cities in a rage."},		//130 - Gyarados
		new string[] {"A PokÈmon that has been over-\nhunted almost to", "extinction. It can ferry people across the water."},		//131 - Lapras
		new string[] {"Capable of copying an enemy< genetic code to instantly", "transform itself into a duplicate of the enemy."},	//132 - Ditto
		new string[] {"Its genetic code is irregular. It may mutate if", "it is exposed to radiation from element Stones."},		//133 - Eevee
		new string[] {"Lives close to water. Its long tail is ridged", "with a fin which is often mistaken for a mermaid<."},		//134 - Vaporeon
		new string[] {"It accumulates negative ions in the atmosphere to", "blast out 10000-\nvolt lightning bolts."},				//135 - Jolteon
		new string[] {"When storing thermal energy in its body, its", "temperature could soar to over 1600 degrees."},				//136 - Flareon
		new string[] {"A POKÈMON that consists entirely of programming", "code. Capable of moving freely in cyberspace."},			//137 - Porygon
		new string[] {"Although long extinct, in rare cases, it can be", "genetically resurrected from fossils."},					//138 - Omantye
		new string[] {"A prehistoric POKÈMON that died out when its", "heavy shell made it impossible to catch prey."},				//139 - Omastar
		new string[] {"A POKÈMON that was resurrected from a fossil", "found in what was once the ocean floor eons ago."},			//140 - Kabuto
		new string[] {"Its sleek shape is perfect for swim-\nming. It slashes", "prey with its claws and drains the body fluids."},	//141 - Kabutops
		new string[] {"A ferocious, pre-\nhistoric POKÈMON that goes for the", "enemy< throat with its serrated saw-like fangs."},	//142 - Aerodactyl
		new string[] {"Very lazy. Just eats and sleeps. As its rotund", "bulk builds, it becomes steadily more slothful."},			//143 - Snorlax
		new string[] {"A legendary bird POKÈMON that is said to appear to", "doomed people who are lost in icy mountains."},		//144 - Articuno
		new string[] {"A legendary bird POKÈMON that is said to appear", "from clouds while dropping enormous lightning bolts."},	//145 - Zapdos
		new string[] {"Known as the legendary bird of fire. Every flap", "of its wings creates a dazzling flash of flames."},		//146 - Moltres
		new string[] {"Long considered a mythical POKÈMON until recently", "when a small colony was found living underwater."},		//147 - Dratini
		new string[] {"A mystical POKÈMON that exudes a gentle aura.", "Has the ability to change climate conditions."},			//148 - Dragonair
		new string[] {"An extremely rarely seen marine POKÈMON.", "Its intelligence is said to match that of humans."},				//149 - Dragonite
		new string[] {"It was created by a scientist after years of horrific", "gene splicing and DNA engineering experiments."},	//150 - Mewtwo
		new string[] {"So rare that it is still said to be a mirage by", "many experts. Only a few people have seen it worldwide."},//151 - Mew
	};

	//An array of each Pokemon's cry by Pokedex number
	public static readonly AudioClip[] Cries = Resources.LoadAll<AudioClip>("Sounds/Pokemon Cries");

	/* An array of each Pokemon's location(s) shown in the Pokedex
	 * Indexes are as follows:
	 * 0: Area Unknown, 1-25: Routes 1-25, 26: Viridian Forest, 27: Mt. Moon
	 * 28: Diglett's Cave, 29: Rock Tunnel, 30: Power Plant, 31: Pokemon Tower
	 * 32: Safari Zone, 33: Seafoam Islands, 34: Pokemon Mansion, 35: Victory Road */
	public static readonly int[][] RedLocations = new int[][]
	{
		new int[]{0 },		//0 - Null Pokemon
		new int[]{0 },		//1 - Bulbasaur
		new int[]{0 },		//2 - Ivysaur
		new int[]{0 },		//3 - Venusaur
		new int[]{0 },		//4 - Charmander
		new int[]{0 },		//5 - Charmeleon
		new int[]{0 },		//6 - Charizard
		new int[]{0 },		//7 - Squirtle
		new int[]{0 },		//8 - Wartortle
		new int[]{0 },			//9 - Blastoise
		new int[]{25, 26 },			//10 - Caterpie
		new int[]{25, 26 },			//11 - Metapod
		new int[]{0 },				//12 - Butterfree
		new int[]{2, 24, 25, 26 },	//13 - Weedle
		new int[]{24, 25, 26 },		//14 - Kakuna
		new int[]{0 },				//15 - Beedrill
		new int[]{1, 2, 3, 5, 6, 7, 8, 12, 13, 14, 15, 21, 24, 25 },	//16 - Pidgey
		new int[]{14, 15, 21 },		//17 - Pidgeotto
		new int[]{0 },				//18 - Pidgeot
		new int[]{1, 2, 4, 9, 16, 21, 22 },	//19 - Rattata
		new int[]{16, 17, 18, 21 },	//20 - Raticate
		new int[]{3, 4, 9, 10, 11, 16, 17, 18, 22, 23 },	//21 - Spearow
		new int[]{17, 18, 23 },		//22 - Fearow
		new int[]{4, 8, 9, 10, 11, 23 },	//23 - Ekans
		new int[]{23 },				//24 - Arbok
		new int[]{26, 30 },			//25 - Pikachu
		new int[]{0 },			//26 - Raichu
		new int[]{0 },			//27 - Sandshrew
		new int[]{0 },			//28 - Sandslash
		new int[]{22, 32 },		//29 - NidoranF
		new int[]{32 },			//30 - Nidorina
		new int[]{0 },			//31 - Nidoqueen
		new int[]{22, 32 },		//32 - NidoranM
		new int[]{32 },			//33 - Nidorino
		new int[]{0 },			//34 - Nidoking
		new int[]{27 },			//35 - Clefairy
		new int[]{0 },			//36 - Clefable
		new int[]{0 },			//37 - Vulpix
		new int[]{0 },			//38 - Ninetales
		new int[]{3 },			//39 - Jigglypuff
		new int[]{0 },				//40 - Wigglytuff
		new int[]{27, 29, 33, 35 },		//41 - Zubat
		new int[]{33, 35 },				//42 - Golbat
		new int[]{5, 6, 7, 12, 13, 14, 15, 24, 25 },	//43 - Oddish
		new int[]{12, 13, 14, 15 },		//44 - Gloom
		new int[]{0 },				//45 - Vileplume
		new int[]{27, 32 },			//46 - Paras
		new int[]{32 },				//47 - Parasect
		new int[]{12, 13, 14, 15, 32 },		//48 - Venonat
		new int[]{32 },				//49 - Venomoth
		new int[]{28 },			//50 - Diglett
		new int[]{28 },			//51 - Dugtrio
		new int[]{0 },			//52 - Meowth
		new int[]{0 },			//53 - Persian
		new int[]{33 },			//54 - Psyduck
		new int[]{33 },			//55 - Golduck
		new int[]{5, 6, 7, 8 },	//56 - Mankey
		new int[]{0 },			//57 - Primape
		new int[]{7, 8, 34 },	//58 - Growlithe
		new int[]{0 },			//59 - Arcanine
		new int[]{0 },			//60 - Poliwag
		new int[]{0 },			//61 - Poliwhirl
		new int[]{0 },			//62 - Poliwrath
		new int[]{24, 25 },		//63 - Abra
		new int[]{0 },			//64 - Kadabra
		new int[]{0 },			//65 - Alakazam
		new int[]{29, 35 },		//66 - Machop
		new int[]{35 },			//67 - Machoke
		new int[]{0 },			//68 - Machamp
		new int[]{0 },			//69 - Bellsprout
		new int[]{0 },			//70 - Weepinbell
		new int[]{0 },			//71 - Victreebel
		new int[]{19, 20, 21 },	//72 - Tentacool
		new int[]{0 },			//73 - Tentacruel
		new int[]{27, 29, 35 },	//74 - Geodude
		new int[]{35 },			//75 - Graveler
		new int[]{0 },			//76 - Golem
		new int[]{34 },			//77 - Ponyta
		new int[]{0 },			//78 - Rapidash
		new int[]{33 },			//79 - Slowpoke
		new int[]{33 },			//80 - Slowbro
		new int[]{30 },			//81 - Magnemite
		new int[]{30 },			//82 - Magneton
		new int[]{0 },			//83 - Farfecth'd
		new int[]{16, 17, 18, 32 },	//84 - Doduo
		new int[]{0 },			//85 - Dodrio
		new int[]{33 },			//86 - Seel
		new int[]{33 },			//87 - Dewgong
		new int[]{34 },			//88 - Grimer
		new int[]{34 },			//89 - Muk
		new int[]{33 },			//90 - Shellder
		new int[]{0 },			//91 - Cloyster
		new int[]{31 },			//92 - Gastly
		new int[]{31 },			//93 - Haunter
		new int[]{0 },			//94 - Gengar
		new int[]{29, 35 },		//95 - Onix
		new int[]{11 },			//96 - Drowzee
		new int[]{0 },			//97 - Hypno
		new int[]{33 },			//98 - Krabby
		new int[]{0 },			//99 - Kingler
		new int[]{30 },			//100 - Voltorb
		new int[]{0 },			//101 - Electrode
		new int[]{32 },			//102 - Exeggcute
		new int[]{0 },			//103 - Exeggutor
		new int[]{31 },			//104 - Cubone
		new int[]{35 },			//105 - Marowak
		new int[]{0 },			//106 - Hitmonlee
		new int[]{0 },			//107 - Hitmonchan
		new int[]{0 },			//108 - Lickitung
		new int[]{34 },			//109 - Koffing
		new int[]{34 },			//110 - Weezing
		new int[]{32 },			//111 - Rhyhorn
		new int[]{0 },			//112 - Rhydon
		new int[]{32 },			//113 - Chansey
		new int[]{21 },			//114 - Tangela
		new int[]{32 },			//115 - Kangaskhan
		new int[]{33 },			//116 - Horsea
		new int[]{33 },			//117 - Seadra
		new int[]{0 },			//118 - Goldeen
		new int[]{0 },			//119 - Seaking
		new int[]{33 },			//120 - Staryu
		new int[]{0 },			//121 - Starmie
		new int[]{0 },			//122 - Mr. Mime
		new int[]{32 },			//123 - Scyther
		new int[]{0 },			//124 - Jynx
		new int[]{30 },			//125 - Electabuzz
		new int[]{0 },			//126 - Magmar
		new int[]{0 },			//127 - Pinsir
		new int[]{32 },			//128 - Tauros
		new int[]{0 },			//129 - Magikarp
		new int[]{0 },			//130 - Gyarados
		new int[]{0 },			//131 - Lapras
		new int[]{13, 14, 15, 23 },	//132 - Ditto
		new int[]{0 },			//133 - Eevee
		new int[]{0 },			//134 - Vaporeon
		new int[]{0 },			//135 - Jolteon
		new int[]{0 },			//136 - Flareon
		new int[]{0 },			//137 - Porygon
		new int[]{0 },			//138 - Omanyte
		new int[]{0 },			//139 - Omastar
		new int[]{0 },			//140 - Kabuto
		new int[]{0 },			//141 - Kabutops
		new int[]{0 },			//142 - Aerodactyl
		new int[]{0 },			//143 - Snorlax
		new int[]{0 },			//144 - Articuno
		new int[]{0 },			//145 - Zapdos
		new int[]{0 },			//146 - Moltres
		new int[]{0 },			//147 - Dratini
		new int[]{0 },			//148 - Dragonair
		new int[]{0 },			//149 - Dragonite
		new int[]{0 },			//150 - Mewtwo
		new int[]{0 },			//151 - Mew
	};

	public static readonly int[][] BlueLocations = new int[][]
	{
		new int[]{0 },		//0 - Null Pokemon
		new int[]{0 },		//1 - Bulbasaur
		new int[]{0 },		//2 - Ivysaur
		new int[]{0 },		//3 - Venusaur
		new int[]{0 },		//4 - Charmander
		new int[]{0 },		//5 - Charmeleon
		new int[]{0 },		//6 - Charizard
		new int[]{0 },		//7 - Squirtle
		new int[]{0 },		//8 - Wartortle
		new int[]{0 },			//9 - Blastoise
		new int[]{25, 26 },			//10 - Caterpie
		new int[]{25, 26 },			//11 - Metapod
		new int[]{0 },				//12 - Butterfree
		new int[]{2, 24, 25, 26 },	//13 - Weedle
		new int[]{24, 25, 26 },		//14 - Kakuna
		new int[]{0 },				//15 - Beedrill
		new int[]{1, 2, 3, 5, 6, 7, 8, 12, 13, 14, 15, 21, 24, 25 },	//16 - Pidgey
		new int[]{14, 15, 21 },		//17 - Pidgeotto
		new int[]{0 },				//18 - Pidgeot
		new int[]{1, 2, 4, 9, 16, 21, 22 },	//19 - Rattata
		new int[]{16, 17, 18, 21 },	//20 - Raticate
		new int[]{3, 4, 9, 10, 11, 16, 17, 18, 22, 23 },	//21 - Spearow
		new int[]{17, 18, 23 },		//22 - Fearow
		new int[]{0 },				//23 - Ekans
		new int[]{0 },				//24 - Arbok
		new int[]{26, 30 },			//25 - Pikachu
		new int[]{0 },				//26 - Raichu
		new int[]{4, 8, 9, 10, 11, 23 },	//27 - Sandshrew
		new int[]{23 },				//28 - Sandslash
		new int[]{22, 32 },			//29 - NidoranF
		new int[]{32 },				//30 - Nidorina
		new int[]{0 },				//31 - Nidoqueen
		new int[]{22, 32 },			//32 - NidoranM
		new int[]{32 },				//33 - Nidorino
		new int[]{0 },				//34 - Nidoking
		new int[]{27 },				//35 - Clefairy
		new int[]{0 },				//36 - Clefable
		new int[]{7, 8, 34 },		//37 - Vulpix
		new int[]{0 },				//38 - Ninetales
		new int[]{3 },				//39 - Jigglypuff
		new int[]{0 },				//40 - Wigglytuff
		new int[]{27, 29, 33, 35 },		//41 - Zubat
		new int[]{33, 35 },				//42 - Golbat
		new int[]{0 },				//43 - Oddish
		new int[]{0 },				//44 - Gloom
		new int[]{0 },				//45 - Vileplume
		new int[]{27, 32 },			//46 - Paras
		new int[]{32 },				//47 - Parasect
		new int[]{12, 13, 14, 15, 32 },		//48 - Venonat
		new int[]{32 },				//49 - Venomoth
		new int[]{28 },			//50 - Diglett
		new int[]{28 },			//51 - Dugtrio
		new int[]{5, 6, 7, 8 },	//52 - Meowth
		new int[]{0 },			//53 - Persian
		new int[]{33 },			//54 - Psyduck
		new int[]{33 },			//55 - Golduck
		new int[]{0 },			//56 - Mankey
		new int[]{0 },			//57 - Primape
		new int[]{0 },			//58 - Growlithe
		new int[]{0 },			//59 - Arcanine
		new int[]{0 },			//60 - Poliwag
		new int[]{0 },			//61 - Poliwhirl
		new int[]{0 },			//62 - Poliwrath
		new int[]{24, 25 },		//63 - Abra
		new int[]{0 },			//64 - Kadabra
		new int[]{0 },			//65 - Alakazam
		new int[]{29, 35 },		//66 - Machop
		new int[]{35 },			//67 - Machoke
		new int[]{0 },			//68 - Machamp
		new int[]{5, 6, 7, 12, 13, 14, 15, 24, 25 },	//69 - Bellsprout
		new int[]{12, 13, 14, 15 },		//70 - Weepinbell
		new int[]{0 },			//71 - Victreebel
		new int[]{19, 20, 21 },	//72 - Tentacool
		new int[]{0 },			//73 - Tentacruel
		new int[]{27, 29, 35 },	//74 - Geodude
		new int[]{35 },			//75 - Graveler
		new int[]{0 },			//76 - Golem
		new int[]{34 },			//77 - Ponyta
		new int[]{0 },			//78 - Rapidash
		new int[]{33 },			//79 - Slowpoke
		new int[]{33 },			//80 - Slowbro
		new int[]{30 },			//81 - Magnemite
		new int[]{30 },			//82 - Magneton
		new int[]{0 },			//83 - Farfecth'd
		new int[]{16, 17, 18, 32 },	//84 - Doduo
		new int[]{0 },			//85 - Dodrio
		new int[]{33 },			//86 - Seel
		new int[]{33 },			//87 - Dewgong
		new int[]{34 },			//88 - Grimer
		new int[]{34 },			//89 - Muk
		new int[]{33 },			//90 - Shellder
		new int[]{0 },			//91 - Cloyster
		new int[]{31 },			//92 - Gastly
		new int[]{31 },			//93 - Haunter
		new int[]{0 },			//94 - Gengar
		new int[]{29, 35 },		//95 - Onix
		new int[]{11 },			//96 - Drowzee
		new int[]{0 },			//97 - Hypno
		new int[]{33 },			//98 - Krabby
		new int[]{0 },			//99 - Kingler
		new int[]{30 },			//100 - Voltorb
		new int[]{0 },			//101 - Electrode
		new int[]{32 },			//102 - Exeggcute
		new int[]{0 },			//103 - Exeggutor
		new int[]{31 },			//104 - Cubone
		new int[]{35 },			//105 - Marowak
		new int[]{0 },			//106 - Hitmonlee
		new int[]{0 },			//107 - Hitmonchan
		new int[]{0 },			//108 - Lickitung
		new int[]{34 },			//109 - Koffing
		new int[]{34 },			//110 - Weezing
		new int[]{32 },			//111 - Rhyhorn
		new int[]{0 },			//112 - Rhydon
		new int[]{32 },			//113 - Chansey
		new int[]{21 },			//114 - Tangela
		new int[]{32 },			//115 - Kangaskhan
		new int[]{33 },			//116 - Horsea
		new int[]{33 },			//117 - Seadra
		new int[]{0 },			//118 - Goldeen
		new int[]{0 },			//119 - Seaking
		new int[]{33 },			//120 - Staryu
		new int[]{0 },			//121 - Starmie
		new int[]{0 },			//122 - Mr. Mime
		new int[]{0 },			//123 - Scyther
		new int[]{0 },			//124 - Jynx
		new int[]{0 },			//125 - Electabuzz
		new int[]{34 },			//126 - Magmar
		new int[]{32 },			//127 - Pinsir
		new int[]{32 },			//128 - Tauros
		new int[]{0 },			//129 - Magikarp
		new int[]{0 },			//130 - Gyarados
		new int[]{0 },			//131 - Lapras
		new int[]{13, 14, 15, 23 },	//132 - Ditto
		new int[]{0 },			//133 - Eevee
		new int[]{0 },			//134 - Vaporeon
		new int[]{0 },			//135 - Jolteon
		new int[]{0 },			//136 - Flareon
		new int[]{0 },			//137 - Porygon
		new int[]{0 },			//138 - Omanyte
		new int[]{0 },			//139 - Omastar
		new int[]{0 },			//140 - Kabuto
		new int[]{0 },			//141 - Kabutops
		new int[]{0 },			//142 - Aerodactyl
		new int[]{0 },			//143 - Snorlax
		new int[]{0 },			//144 - Articuno
		new int[]{0 },			//145 - Zapdos
		new int[]{0 },			//146 - Moltres
		new int[]{0 },			//147 - Dratini
		new int[]{0 },			//148 - Dragonair
		new int[]{0 },			//149 - Dragonite
		new int[]{0 },			//150 - Mewtwo
		new int[]{0 },			//151 - Mew
	};
}