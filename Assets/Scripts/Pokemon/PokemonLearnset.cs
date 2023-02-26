using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PokemonLearnset
{
	//Checks the list of all level up moves
	//Returns true if the specified Pokemon can learn a move at the specified level
	public static bool CanLearnMoveAtLevel(int dexNum, int level)
    {
		//Loop through each Move-Level Pair in this Pokemon's list of learnable moves
		foreach(MoveLevelPair ml in AllLevelUpMoves[dexNum])
        {
			if (ml.Level == level)
				return true;
        }

		return false;
    }

	//Returns a list of move indexes that the specified Pokemon can learn at the specified level
	public static List<int> GetAllMovesAtLevel(int dexNum, int level)
    {
		List<int> listOfMoves = new List<int>();

		//Loop through each Move-Level Pair in this Pokemon's list of learnable moves
		foreach (MoveLevelPair ml in AllLevelUpMoves[dexNum])
        {
			//If the Pokemon can learn a move at this level, add it to the list
			if (ml.Level == level)
				listOfMoves.Add(ml.MoveIndex);
			//Break out of the loop once this level has been passed to prevent unnecessary loops/checks
			else if (ml.Level > level)
				break;
        }

		return listOfMoves;
    }

	//This is a list of every Pokemon's moves learned through leveling up
	//The first index corresponds to the Pokemon's Pokedex number
	//The first element is blank to represent an empty Pokemon with dex number 0
	public static readonly MoveLevelPair[][] AllLevelUpMoves = new MoveLevelPair[][]
	{
		//0 - Null Pokemon
		new MoveLevelPair[]
		{

		},
		//1 - Bulbasaur
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(7, 73),	//Leech Seed
			new MoveLevelPair(13, 159),	//Vine Whip
			new MoveLevelPair(20, 94),	//Poison Powder
			new MoveLevelPair(27, 102),	//Razor Leaf
			new MoveLevelPair(34, 54),	//Growth
			new MoveLevelPair(41, 122),	//Sleep Powder
			new MoveLevelPair(48, 127)	//Solar Beam
		},
		//2 - Ivysaur
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 73),	//Leech Seed
			new MoveLevelPair(7, 73),	//Leech Seed
			new MoveLevelPair(13, 159),	//Vine Whip
			new MoveLevelPair(22, 94),	//Poison Powder
			new MoveLevelPair(30, 102),	//Razor Leaf
			new MoveLevelPair(38, 54),	//Growth
			new MoveLevelPair(46, 122),	//Sleep Powder
			new MoveLevelPair(54, 127)	//Solar Beam
		},
		//3 - Venusaur
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 73),	//Leech Seed
			new MoveLevelPair(1, 159),	//Vine Whip
			new MoveLevelPair(7, 73),	//Leech Seed
			new MoveLevelPair(13, 159),	//Vine Whip
			new MoveLevelPair(22, 94),	//Poison Powder
			new MoveLevelPair(30, 102),	//Razor Leaf
			new MoveLevelPair(43, 54),	//Growth
			new MoveLevelPair(55, 122),	//Sleep Powder
			new MoveLevelPair(65, 127)	//Solar Beam
		},
		//4 - Charmander
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(9, 40),	//Ember
			new MoveLevelPair(15, 74),	//Leer
			new MoveLevelPair(22, 101),	//Rage
			new MoveLevelPair(30, 121),	//Slash
			new MoveLevelPair(38, 46),	//Flamethrower
			new MoveLevelPair(46, 44)	//Fire Spin
		},
		//5 - Charmeleon
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 40),	//Ember
			new MoveLevelPair(9, 40),	//Ember
			new MoveLevelPair(15, 74),	//Leer
			new MoveLevelPair(24, 101),	//Rage
			new MoveLevelPair(33, 121),	//Slash
			new MoveLevelPair(42, 46),	//Flamethrower
			new MoveLevelPair(56, 44)	//Fire Spin
		},
		//6 - Charizard
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 40),	//Ember
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(9, 40),	//Ember
			new MoveLevelPair(15, 74),	//Leer
			new MoveLevelPair(24, 101),	//Rage
			new MoveLevelPair(36, 121),	//Slash
			new MoveLevelPair(46, 46),	//Flamethrower
			new MoveLevelPair(55, 44)	//Fire Spin
		},
		//7 - Squirtle
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(8, 16),	//Bubble
			new MoveLevelPair(15, 160),	//Water Gun
			new MoveLevelPair(22, 11),	//Bite
			new MoveLevelPair(28, 164),	//Withdraw
			new MoveLevelPair(35, 118),	//Skull Bash
			new MoveLevelPair(42, 63)	//Hydro Pump
		},
		//8 - Wartortle
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(1, 16),	//Bubble
			new MoveLevelPair(8, 16),	//Bubble
			new MoveLevelPair(15, 160),	//Water Gun
			new MoveLevelPair(24, 11),	//Bite
			new MoveLevelPair(31, 164),	//Withdraw
			new MoveLevelPair(39, 118),	//Skull Bash
			new MoveLevelPair(47, 63)	//Hydro Pump
		},
		//9 - Blastoise
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(1, 16),	//Bubble
			new MoveLevelPair(1, 160),	//Water Gun
			new MoveLevelPair(8, 16),	//Bubble
			new MoveLevelPair(15, 160),	//Water Gun
			new MoveLevelPair(24, 11),	//Bite
			new MoveLevelPair(31, 164),	//Withdraw
			new MoveLevelPair(42, 118),	//Skull Bash
			new MoveLevelPair(52, 63)	//Hydro Pump
		},
		//10 - Caterpie
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 134)	//String Shot
		},
		//11 - Metapod
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 57)	//Harden
		},
		//12 - Butterfree
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 21),	//Confusion
			new MoveLevelPair(12, 21),	//Confusion
			new MoveLevelPair(15, 94),	//Poison Powder
			new MoveLevelPair(16, 136),	//Stun Spore
			new MoveLevelPair(17, 122),	//Sleep Powder
			new MoveLevelPair(21, 140),	//Supersonic
			new MoveLevelPair(26, 162),	//Whirlwind
			new MoveLevelPair(32, 97)	//Psybeam
		},
		//13 - Weedle
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 95),	//Poison Sting
			new MoveLevelPair(1, 134)	//String Shot
		},
		//14 - Kakuna
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 57)	//Harden
		},
		//15 - Beedrill
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 50),	//Fury Attack
			new MoveLevelPair(12, 50),	//Fury Attack
			new MoveLevelPair(16, 49),	//Focus Energy
			new MoveLevelPair(20, 157),	//Twineedle
			new MoveLevelPair(25, 101),	//Rage
			new MoveLevelPair(30, 92),	//Pin Missle
			new MoveLevelPair(35, 4)	//Agility
		},
		//16 - Pidgey
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 56),	//Gust
			new MoveLevelPair(5, 111),	//Sand Attack
			new MoveLevelPair(12, 100),	//Quick Attack
			new MoveLevelPair(19, 162),	//Whirlwind
			new MoveLevelPair(28, 163),	//Wing Attack
			new MoveLevelPair(36, 4),	//Agility
			new MoveLevelPair(44, 86)	//Mirror Move
		},
		//17 - Pidgeotto
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 56),	//Gust
			new MoveLevelPair(1, 111),	//Sand Attack
			new MoveLevelPair(5, 111),	//Sand Attack
			new MoveLevelPair(12, 100),	//Quick Attack
			new MoveLevelPair(21, 162),	//Whirlwind
			new MoveLevelPair(31, 163),	//Wing Attack
			new MoveLevelPair(40, 4),	//Agility
			new MoveLevelPair(49, 86)	//Mirror Move
		},
		//18 - Pidgeot
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 56),	//Gust
			new MoveLevelPair(1, 111),	//Sand Attack
			new MoveLevelPair(1, 100),	//Quick Attack
			new MoveLevelPair(5, 111),	//Sand Attack
			new MoveLevelPair(12, 100),	//Quick Attack
			new MoveLevelPair(21, 162),	//Whirlwind
			new MoveLevelPair(31, 163),	//Wing Attack
			new MoveLevelPair(44, 4),	//Agility
			new MoveLevelPair(54, 86)	//Mirror Move
		},
		//19 - Rattata
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(7, 100),	//Quick Attack
			new MoveLevelPair(14, 65),	//Hyper Fang
			new MoveLevelPair(23, 49),	//Focus Energy
			new MoveLevelPair(34, 139)	//Super Fang
		},
		//20 - Raticate
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(1, 100),	//Quick Attack
			new MoveLevelPair(7, 100),	//Quick Attack
			new MoveLevelPair(14, 65),	//Hyper Fang
			new MoveLevelPair(27, 49),	//Focus Energy
			new MoveLevelPair(41, 139)	//Super Fang
		},
		//21 - Spearow
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 90),	//Peck
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(9, 74),	//Leer
			new MoveLevelPair(15, 50),	//Fury Attack
			new MoveLevelPair(22, 86),	//Mirror Move
			new MoveLevelPair(29, 37),	//Drill Peck
			new MoveLevelPair(36, 4)	//Agility
		},
		//22 - Fearow
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 90),	//Peck
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(9, 74),	//Leer
			new MoveLevelPair(15, 50),	//Fury Attack
			new MoveLevelPair(25, 86),	//Mirror Move
			new MoveLevelPair(34, 37),	//Drill Peck
			new MoveLevelPair(43, 4)	//Agility
		},
		//23 - Ekans
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 165),	//Wrap
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(10, 95),	//Poison Sting
			new MoveLevelPair(17, 11),	//Bite
			new MoveLevelPair(24, 52),	//Glare
			new MoveLevelPair(31, 113),	//Screech
			new MoveLevelPair(38, 2)	//Acid
		},
		//24 - Arbok
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 165),	//Wrap
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 95),	//Poison Sting
			new MoveLevelPair(10, 95),	//Poison Sting
			new MoveLevelPair(17, 11),	//Bite
			new MoveLevelPair(27, 52),	//Glare
			new MoveLevelPair(36, 113),	//Screech
			new MoveLevelPair(47, 2)	//Acid
		},
		//25 - Pikachu
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 151),	//Thundershock
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(9, 152),	//Thunderwave
			new MoveLevelPair(16, 100),	//Quick Attack
			new MoveLevelPair(26, 142),	//Swift
			new MoveLevelPair(33, 4),	//Agility
			new MoveLevelPair(43, 149)	//Thunder
		},
		//26 - Raichu
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 151),	//Thundershock
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 152)	//Thunderwave
		},
		//27 - Sandshrew
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(10, 111),	//Sand Attack
			new MoveLevelPair(17, 121),	//Slash
			new MoveLevelPair(24, 95),	//Poison Sting
			new MoveLevelPair(31, 142),	//Swift
			new MoveLevelPair(38, 51)	//Fury Swipes
		},
		//28 - Sandslash
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 111),	//Sand Attack
			new MoveLevelPair(10, 111),	//Sand Attack
			new MoveLevelPair(17, 121),	//Slash
			new MoveLevelPair(27, 95),	//Poison Sting
			new MoveLevelPair(36, 142),	//Swift
			new MoveLevelPair(47, 51)	//Fury Swipes
		},
		//29 - Nidoran F
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(8, 112),	//Scratch
			new MoveLevelPair(14, 95),	//Poison Sting
			new MoveLevelPair(21, 145),	//Tail Whip
			new MoveLevelPair(29, 11),	//Bite
			new MoveLevelPair(36, 51),	//Fury Swipes
			new MoveLevelPair(43, 31)	//Double Kick
		},
		//30 - Nidorina
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(8, 112),	//Scratch
			new MoveLevelPair(14, 95),	//Poison Sting
			new MoveLevelPair(23, 145),	//Tail Whip
			new MoveLevelPair(32, 11),	//Bite
			new MoveLevelPair(41, 51),	//Fury Swipes
			new MoveLevelPair(50, 31)	//Double Kick
		},
		//31 - Nidoqueen
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(1, 13),	//Body Slam
			new MoveLevelPair(8, 112),	//Scratch
			new MoveLevelPair(14, 95),	//Poison Sting
			new MoveLevelPair(23, 13)	//Body Slam
		},
		//32 - Nidoran M
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(8, 61),	//Horn Attack
			new MoveLevelPair(14, 95),	//Poison Sting
			new MoveLevelPair(21, 49),	//Focus Energy
			new MoveLevelPair(29, 50),	//Fury Attack
			new MoveLevelPair(36, 62),	//Horn Drill
			new MoveLevelPair(43, 31)	//Double Kick
		},
		//33 - Nidorino
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 61),	//Horn Attack
			new MoveLevelPair(8, 61),	//Horn Attack
			new MoveLevelPair(14, 95),	//Poison Sting
			new MoveLevelPair(23, 49),	//Focus Energy
			new MoveLevelPair(32, 50),	//Fury Attack
			new MoveLevelPair(41, 62),	//Horn Drill
			new MoveLevelPair(50, 31)	//Double Kick
		},
		//34 - Nidoking
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 61),	//Horn Attack
			new MoveLevelPair(1, 95),	//Poison Sting
			new MoveLevelPair(1, 148),	//Thrash
			new MoveLevelPair(8, 61),	//Horn Attack
			new MoveLevelPair(14, 95),	//Poison Sting
			new MoveLevelPair(23, 148)	//Thrash
		},
		//35 - Clefairy
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 96),	//Pound
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(13, 117),	//Sing
			new MoveLevelPair(18, 32),	//Double Slap
			new MoveLevelPair(24, 85),	//Minimize
			new MoveLevelPair(31, 83),	//Metronome
			new MoveLevelPair(39, 27),	//Defense Curl
			new MoveLevelPair(48, 76)	//Light Screen
		},
		//36 - Clefable
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 117),	//Sing
			new MoveLevelPair(1, 32),	//Double Slap
			new MoveLevelPair(1, 85),	//Minimize
			new MoveLevelPair(1, 83)	//Metronome
		},
		//37 - Vulpix
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 40),	//Ember
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(16, 100),	//Quick Attack
			new MoveLevelPair(21, 107),	//Roar
			new MoveLevelPair(28, 20),	//Confuse Ray
			new MoveLevelPair(35, 46),	//Flamethrower
			new MoveLevelPair(42, 44)	//Fire Spin
		},
		//38 - Ninetales
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 40),	//Ember
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(1, 100),	//Quick Attack
			new MoveLevelPair(1, 107)	//Roar
		},
		//39 - Jigglypuff
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 117),	//Sing
			new MoveLevelPair(9, 96),	//Pound
			new MoveLevelPair(14, 29),	//Disable
			new MoveLevelPair(19, 27),	//Defense Curl
			new MoveLevelPair(24, 32),	//Double Slap
			new MoveLevelPair(29, 106),	//Rest
			new MoveLevelPair(34, 13),	//Body Slam
			new MoveLevelPair(39, 34)	//Double-Edge
		},
		//40 - Wigglytuff
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 117),	//Sing
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(1, 27),	//Defense Curl
			new MoveLevelPair(1, 32)	//Double Slap
		},
		//41 - Zubat
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 72),	//Leech Life
			new MoveLevelPair(10, 140),	//Supersonic
			new MoveLevelPair(15, 11),	//Bite
			new MoveLevelPair(21, 20),	//Confuse Ray
			new MoveLevelPair(28, 163),	//Wing Attack
			new MoveLevelPair(36, 58)	//Haze
		},
		//42 - Golbat
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 72),	//Leech Life
			new MoveLevelPair(1, 113),	//Screech
			new MoveLevelPair(1, 11),	//Bite
			new MoveLevelPair(10, 140),	//Supersonic
			new MoveLevelPair(15, 11),	//Bite
			new MoveLevelPair(21, 20),	//Confuse Ray
			new MoveLevelPair(32, 163),	//Wing Attack
			new MoveLevelPair(43, 58)	//Haze
		},
		//43 - Oddish
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 1),	//Absorb
			new MoveLevelPair(15, 94),	//Poison Powder
			new MoveLevelPair(17, 136),	//Stun Spore
			new MoveLevelPair(19, 122),	//Sleep Powder
			new MoveLevelPair(24, 2),	//Acid
			new MoveLevelPair(33, 91),	//Petal Dance
			new MoveLevelPair(46, 127)	//Solar Beam
		},
		//44 - Gloom
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 1),	//Absorb
			new MoveLevelPair(1, 94),	//Poison Powder
			new MoveLevelPair(1, 136),	//Stun Spore
			new MoveLevelPair(15, 94),	//Poison Powder
			new MoveLevelPair(17, 136),	//Stun Spore
			new MoveLevelPair(19, 122),	//Sleep Powder
			new MoveLevelPair(28, 2),	//Acid
			new MoveLevelPair(38, 91),	//Petal Dance
			new MoveLevelPair(52, 127)	//Solar Beam
		},
		//45 - Vileplume
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 136),	//Stun Spore
			new MoveLevelPair(1, 122),	//Sleep Powder
			new MoveLevelPair(1, 2),	//Acid
			new MoveLevelPair(1, 91),	//Petal Dance
			new MoveLevelPair(15, 94),	//Poison Powder
			new MoveLevelPair(17, 136),	//Stun Spore
			new MoveLevelPair(19, 122)	//Sleep Powder
		},
		//46 - Paras
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(13, 136),	//Stun Spore
			new MoveLevelPair(20, 72),	//Leech Life
			new MoveLevelPair(27, 131),	//Spore
			new MoveLevelPair(34, 121),	//Slash
			new MoveLevelPair(41, 53)	//Growl
		},
		//47 - Parasect
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 136),	//Stun Spore
			new MoveLevelPair(1, 72),	//Leech Life
			new MoveLevelPair(13, 136),	//Stun Spore
			new MoveLevelPair(20, 72),	//Leech Life
			new MoveLevelPair(27, 131),	//Spore
			new MoveLevelPair(34, 121),	//Slash
			new MoveLevelPair(41, 53)	//Growl
		},
		//48 - Venonat
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(24, 94),	//Poison Powder
			new MoveLevelPair(27, 72),	//Leech Life
			new MoveLevelPair(30, 136),	//Stun Spore
			new MoveLevelPair(35, 97),	//Psybeam
			new MoveLevelPair(38, 122),	//Sleep Powder
			new MoveLevelPair(43, 98)	//Psychic
		},
		//49 - Venomoth
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(1, 94),	//Poison Powder
			new MoveLevelPair(1, 72),	//Leech Life
			new MoveLevelPair(24, 94),	//Poison Powder
			new MoveLevelPair(27, 72),	//Leech Life
			new MoveLevelPair(30, 136),	//Stun Spore
			new MoveLevelPair(38, 97),	//Psybeam
			new MoveLevelPair(43, 122),	//Sleep Powder
			new MoveLevelPair(50, 98)	//Psychic
		},
		//50 - Diglett
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(15, 53),	//Growl
			new MoveLevelPair(19, 28),	//Dig
			new MoveLevelPair(24, 111),	//Sand Attack
			new MoveLevelPair(31, 121),	//Slash
			new MoveLevelPair(40, 38)	//Earthquake
		},
		//51 - Dugtrio
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 28),	//Dig
			new MoveLevelPair(15, 53),	//Growl
			new MoveLevelPair(19, 28),	//Dig
			new MoveLevelPair(24, 111),	//Sand Attack
			new MoveLevelPair(35, 121),	//Slash
			new MoveLevelPair(47, 38)	//Earthquake
		},
		//52 - Meowth
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(12, 11),	//Bite
			new MoveLevelPair(17, 89),	//Pay Day
			new MoveLevelPair(24, 113),	//Screech
			new MoveLevelPair(33, 51),	//Fury Swipes
			new MoveLevelPair(44, 121)	//Slash
		},
		//53 - Persian
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 11),	//Bite
			new MoveLevelPair(1, 113),	//Screech
			new MoveLevelPair(12, 11),	//Bite
			new MoveLevelPair(17, 89),	//Pay Day
			new MoveLevelPair(24, 113),	//Screech
			new MoveLevelPair(37, 51),	//Fury Swipes
			new MoveLevelPair(51, 121)	//Slash
		},
		//54 - Psyduck
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(28, 145),	//Tail Whip
			new MoveLevelPair(31, 29),	//Disable
			new MoveLevelPair(36, 21),	//Confusion
			new MoveLevelPair(43, 51),	//Fury Swipes
			new MoveLevelPair(52, 63)	//Hydro Pump
		},
		//55 - Golduck
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(28, 145),	//Tail Whip
			new MoveLevelPair(31, 29),	//Disable
			new MoveLevelPair(39, 21),	//Confusion
			new MoveLevelPair(48, 51),	//Fury Swipes
			new MoveLevelPair(59, 63)	//Hydro Pump
		},
		//56 - Mankey
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(15, 70),	//Karate Chop
			new MoveLevelPair(21, 51),	//Fury Swipes
			new MoveLevelPair(27, 49),	//Focus Energy
			new MoveLevelPair(33, 114),	//Seismic Toss
			new MoveLevelPair(39, 148)	//Thrash
		},
		//57 - Primeape
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 70),	//Karate Chop
			new MoveLevelPair(1, 51),	//Fury Swipes
			new MoveLevelPair(15, 70),	//Karate Chop
			new MoveLevelPair(21, 51),	//Fury Swipes
			new MoveLevelPair(27, 49),	//Focus Energy
			new MoveLevelPair(37, 114),	//Seismic Toss
			new MoveLevelPair(46, 148)	//Thrash
		},
		//58 - Growlithe
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 11),	//Bite
			new MoveLevelPair(1, 107),	//Roar
			new MoveLevelPair(18, 40),	//Ember
			new MoveLevelPair(23, 74),	//Leer
			new MoveLevelPair(30, 146),	//Take Down
			new MoveLevelPair(39, 4),	//Agility
			new MoveLevelPair(50, 46)	//Flamethrower
		},
		//59 - Arcanine
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 107),	//Roar
			new MoveLevelPair(1, 40),	//Ember
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 146)	//Take Down
		},
		//60 - Poliwag
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 16),	//Bubble
			new MoveLevelPair(16, 66),	//Hypnosis
			new MoveLevelPair(19, 160),	//Water Gun
			new MoveLevelPair(25, 32),	//Double Slap
			new MoveLevelPair(31, 13),	//Body Slam
			new MoveLevelPair(38, 5),	//Amnesia
			new MoveLevelPair(45, 63)	//Hydro Pump
		},
		//61 - Poliwhirl
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 16),	//Bubble
			new MoveLevelPair(1, 66),	//Hypnosis
			new MoveLevelPair(1, 160),	//Water Gun
			new MoveLevelPair(16, 66),	//Hypnosis
			new MoveLevelPair(19, 160),	//Water Gun
			new MoveLevelPair(26, 32),	//Double Slap
			new MoveLevelPair(33, 13),	//Body Slam
			new MoveLevelPair(41, 5),	//Amnesia
			new MoveLevelPair(49, 63)	//Hydro Pump
		},
		//62 - Poliwrath
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 66),	//Hypnosis
			new MoveLevelPair(1, 160),	//Water Gun
			new MoveLevelPair(1, 32),	//Double Slap
			new MoveLevelPair(1, 13),	//Body Slam
			new MoveLevelPair(16, 66),	//Hypnosis
			new MoveLevelPair(19, 160)	//Water Gun
		},
		//63 - Abra
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 147)	//Teleport
		},
		//64 - Kadabra
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 147),	//Teleport
			new MoveLevelPair(1, 21),	//Confusion
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(16, 21),	//Confusion
			new MoveLevelPair(20, 29),	//Disable
			new MoveLevelPair(27, 97),	//Psybeam
			new MoveLevelPair(31, 104),	//Recover
			new MoveLevelPair(38, 98),	//Psychic
			new MoveLevelPair(42, 105)	//Reflect
		},
		//65 - Alakazam
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 147),	//Teleport
			new MoveLevelPair(1, 21),	//Confusion
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(16, 21),	//Confusion
			new MoveLevelPair(20, 29),	//Disable
			new MoveLevelPair(27, 97),	//Psybeam
			new MoveLevelPair(31, 104),	//Recover
			new MoveLevelPair(38, 98),	//Psychic
			new MoveLevelPair(42, 105)	//Reflect
		},
		//66 - Machop
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 70),	//Karate Chop
			new MoveLevelPair(20, 78),	//Low Kick
			new MoveLevelPair(25, 74),	//Leer
			new MoveLevelPair(32, 49),	//Focus Energy
			new MoveLevelPair(39, 114),	//Seismic Toss
			new MoveLevelPair(46, 137)	//Submission
		},
		//67 - Machoke
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 70),	//Karate Chop
			new MoveLevelPair(1, 78),	//Low Kick
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(20, 78),	//Low Kick
			new MoveLevelPair(25, 74),	//Leer
			new MoveLevelPair(36, 49),	//Focus Energy
			new MoveLevelPair(44, 114),	//Seismic Toss
			new MoveLevelPair(52, 137)	//Submission
		},
		//68 - Machamp
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 70),	//Karate Chop
			new MoveLevelPair(1, 78),	//Low Kick
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(20, 78),	//Low Kick
			new MoveLevelPair(25, 74),	//Leer
			new MoveLevelPair(36, 49),	//Focus Energy
			new MoveLevelPair(44, 114),	//Seismic Toss
			new MoveLevelPair(52, 137)	//Submission
		},
		//69 - Bellsprout
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 159),	//Vine Whip
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(13, 165),	//Wrap
			new MoveLevelPair(15, 94),	//Poison Powder
			new MoveLevelPair(18, 122),	//Sleep Powder
			new MoveLevelPair(21, 136),	//Stun Spore
			new MoveLevelPair(26, 2),	//Acid
			new MoveLevelPair(33, 102),	//Razor Leaf
			new MoveLevelPair(42, 120)	//Slam
		},
		//70 - Weepinbell
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 159),	//Vine Whip
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 165),	//Wrap
			new MoveLevelPair(13, 165),	//Wrap
			new MoveLevelPair(15, 94),	//Poison Powder
			new MoveLevelPair(18, 122),	//Sleep Powder
			new MoveLevelPair(23, 136),	//Stun Spore
			new MoveLevelPair(29, 2),	//Acid
			new MoveLevelPair(38, 102),	//Razor Leaf
			new MoveLevelPair(49, 120)	//Slam
		},
		//71 - Victreebel
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 122),	//Sleep Powder
			new MoveLevelPair(1, 136),	//Stun Spore
			new MoveLevelPair(1, 2),	//Acid
			new MoveLevelPair(1, 102),	//Razor Leaf
			new MoveLevelPair(13, 165),	//Wrap
			new MoveLevelPair(15, 94),	//Poison Powder
			new MoveLevelPair(18, 122),	//Sleep Powder
		},
		//72 - Tentacool
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 2),	//Acid
			new MoveLevelPair(7, 140),	//Supersonic
			new MoveLevelPair(13, 165),	//Wrap
			new MoveLevelPair(18, 95),	//Poison Sting
			new MoveLevelPair(22, 160),	//Water Gun
			new MoveLevelPair(27, 22),	//Constrict
			new MoveLevelPair(33, 8),	//Barrier
			new MoveLevelPair(40, 113),	//Screech
			new MoveLevelPair(48, 63)	//Hydro Pump
		},
		//73 - Tentacruel
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 2),	//Acid
			new MoveLevelPair(1, 140),	//Supersonic
			new MoveLevelPair(1, 165),	//Wrap
			new MoveLevelPair(7, 140),	//Supersonic
			new MoveLevelPair(13, 165),	//Wrap
			new MoveLevelPair(18, 95),	//Poison Sting
			new MoveLevelPair(22, 160),	//Water Gun
			new MoveLevelPair(27, 22),	//Constrict
			new MoveLevelPair(35, 8),	//Barrier
			new MoveLevelPair(43, 113),	//Screech
			new MoveLevelPair(50, 63)	//Hydro Pump
		},
		//74 - Geodude
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(11, 27),	//Defense Curl
			new MoveLevelPair(16, 109),	//Rock Throw
			new MoveLevelPair(21, 115),	//Self-Destruct
			new MoveLevelPair(26, 57),	//Harden
			new MoveLevelPair(31, 38),	//Earthquake
			new MoveLevelPair(36, 41)	//Explosion
		},
		//75 - Graveler
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 27),	//Defense Curl
			new MoveLevelPair(11, 27),	//Defense Curl
			new MoveLevelPair(16, 109),	//Rock Throw
			new MoveLevelPair(21, 115),	//Self-Destruct
			new MoveLevelPair(29, 57),	//Harden
			new MoveLevelPair(36, 38),	//Earthquake
			new MoveLevelPair(43, 41)	//Explosion
		},
		//76 - Golem
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 27),	//Defense Curl
			new MoveLevelPair(11, 27),	//Defense Curl
			new MoveLevelPair(16, 109),	//Rock Throw
			new MoveLevelPair(21, 115),	//Self-Destruct
			new MoveLevelPair(29, 57),	//Harden
			new MoveLevelPair(36, 38),	//Earthquake
			new MoveLevelPair(43, 41)	//Explosion
		},
		//77 - Ponyta
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 40),	//Ember
			new MoveLevelPair(30, 145),	//Tail Whip
			new MoveLevelPair(32, 132),	//Stomp
			new MoveLevelPair(35, 53),	//Growl
			new MoveLevelPair(39, 44),	//Fire Spin
			new MoveLevelPair(43, 146),	//Take Down
			new MoveLevelPair(48, 4)	//Agility
		},
		//78 - Rapidash
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 40),	//Ember
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(1, 132),	//Stomp
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(30, 145),	//Tail Whip
			new MoveLevelPair(32, 132),	//Stomp
			new MoveLevelPair(35, 53),	//Growl
			new MoveLevelPair(39, 44),	//Fire Spin
			new MoveLevelPair(47, 146),	//Take Down
			new MoveLevelPair(55, 4)	//Agility
		},
		//79 - Slowpoke
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 21),	//Confusion
			new MoveLevelPair(18, 29),	//Disable
			new MoveLevelPair(22, 59),	//Headbutt
			new MoveLevelPair(27, 53),	//Growl
			new MoveLevelPair(33, 160),	//Water Gun
			new MoveLevelPair(40, 5),	//Amnesia
			new MoveLevelPair(48, 98)	//Psychic
		},
		//80 - Slowbro
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 21),	//Confusion
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(1, 59),	//Headbutt
			new MoveLevelPair(18, 29),	//Disable
			new MoveLevelPair(22, 59),	//Headbutt
			new MoveLevelPair(27, 53),	//Growl
			new MoveLevelPair(33, 160),	//Water Gun
			new MoveLevelPair(37, 164),	//Withdraw
			new MoveLevelPair(44, 5),	//Amnesia
			new MoveLevelPair(55, 98)	//Psychic
		},
		//81 - Magnemite
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(21, 128),	//Sonic Boom
			new MoveLevelPair(25, 151),	//Thundershock
			new MoveLevelPair(29, 140),	//Supersonic
			new MoveLevelPair(35, 152),	//Thunder Wave
			new MoveLevelPair(41, 142),	//Swift
			new MoveLevelPair(47, 113)	//Screech
		},
		//82 - Magneton
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 128),	//Sonic Boom
			new MoveLevelPair(1, 151),	//Thundershock
			new MoveLevelPair(21, 128),	//Sonic Boom
			new MoveLevelPair(25, 151),	//Thundershock
			new MoveLevelPair(29, 140),	//Supersonic
			new MoveLevelPair(38, 152),	//Thunder Wave
			new MoveLevelPair(46, 142),	//Swift
			new MoveLevelPair(54, 113)	//Screech
		},
		//83 - Farfetch'd
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 90),	//Peck
			new MoveLevelPair(1, 111),	//Sand Attack
			new MoveLevelPair(7, 74),	//Leer
			new MoveLevelPair(15, 50),	//Fury Attack
			new MoveLevelPair(23, 143),	//Swords Dance
			new MoveLevelPair(31, 4),	//Agility
			new MoveLevelPair(39, 121)	//Slash
		},
		//84 - Doduo
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 90),	//Peck
			new MoveLevelPair(20, 53),	//Growl
			new MoveLevelPair(24, 50),	//Fury Attack
			new MoveLevelPair(30, 37),	//Drill Peck
			new MoveLevelPair(36, 101),	//Rage
			new MoveLevelPair(40, 156),	//Tri Attack
			new MoveLevelPair(44, 4)	//Agility
		},
		//85 - Dodrio
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 90),	//Peck
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 50),	//Fury Attack
			new MoveLevelPair(20, 53),	//Growl
			new MoveLevelPair(24, 50),	//Fury Attack
			new MoveLevelPair(30, 37),	//Drill Peck
			new MoveLevelPair(39, 101),	//Rage
			new MoveLevelPair(45, 156),	//Tri Attack
			new MoveLevelPair(51, 4)	//Agility
		},
		//86 - Seel
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 59),	//Headbutt
			new MoveLevelPair(30, 53),	//Growl
			new MoveLevelPair(35, 6),	//Aurora Beam
			new MoveLevelPair(40, 106),	//Rest
			new MoveLevelPair(45, 146),	//Take Down
			new MoveLevelPair(50, 67)	//Ice Beam
		},
		//87 - Dewgong
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 59),	//Headbutt
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 6),	//Aurora Beam
			new MoveLevelPair(30, 53),	//Growl
			new MoveLevelPair(35, 6),	//Aurora Beam
			new MoveLevelPair(44, 106),	//Rest
			new MoveLevelPair(50, 146),	//Take Down
			new MoveLevelPair(56, 67)	//Ice Beam
		},
		//88 - Grimer
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 96),	//Pound
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(30, 93),	//Poison Gas
			new MoveLevelPair(33, 85),	//Minimize
			new MoveLevelPair(37, 123),	//Sludge
			new MoveLevelPair(42, 57),	//Harden
			new MoveLevelPair(48, 113),	//Screech
			new MoveLevelPair(55, 3)	//Acid Armor
		},
		//89 - Muk
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 96),	//Pound
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(1, 93),	//Poison Gas
			new MoveLevelPair(30, 93),	//Poison Gas
			new MoveLevelPair(33, 85),	//Minimize
			new MoveLevelPair(37, 123),	//Sludge
			new MoveLevelPair(45, 57),	//Harden
			new MoveLevelPair(53, 113),	//Screech
			new MoveLevelPair(60, 3)	//Acid Armor
		},
		//90 - Shellder
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 164),	//Withdraw
			new MoveLevelPair(18, 140),	//Supersonic
			new MoveLevelPair(23, 18),	//Clamp
			new MoveLevelPair(30, 6),	//Aurora Beam
			new MoveLevelPair(39, 74),	//Leer
			new MoveLevelPair(50, 67)	//Ice Beam
		},
		//91 - Cloyster
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 164),	//Withdraw
			new MoveLevelPair(1, 140),	//Supersonic
			new MoveLevelPair(1, 18),	//Clamp
			new MoveLevelPair(1, 6),	//Aurora Beam
			new MoveLevelPair(50, 129)	//Spike Cannon
		},
		//92 - Gastly
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 75),	//Lick
			new MoveLevelPair(1, 20),	//Confuse Ray
			new MoveLevelPair(1, 88),	//Night Shade
			new MoveLevelPair(27, 66),	//Hypnosis
			new MoveLevelPair(35, 36)	//Dream Eater
		},
		//93 - Haunter
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 75),	//Lick
			new MoveLevelPair(1, 20),	//Confuse Ray
			new MoveLevelPair(1, 88),	//Night Shade
			new MoveLevelPair(29, 66),	//Hypnosis
			new MoveLevelPair(38, 36)	//Dream Eater
		},
		//94 - Gengar
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 75),	//Lick
			new MoveLevelPair(1, 20),	//Confuse Ray
			new MoveLevelPair(1, 88),	//Night Shade
			new MoveLevelPair(29, 66),	//Hypnosis
			new MoveLevelPair(38, 36)	//Dream Eater
		},
		//95 - Onix
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 113),	//Screech
			new MoveLevelPair(15, 10),	//Bind
			new MoveLevelPair(19, 109),	//Rock Throw
			new MoveLevelPair(25, 101),	//Rage
			new MoveLevelPair(33, 120),	//Slam
			new MoveLevelPair(43, 57)	//Harden
		},
		//96 - Drowzee
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 96),	//Pound
			new MoveLevelPair(1, 66),	//Hypnosis
			new MoveLevelPair(12, 29),	//Disable
			new MoveLevelPair(17, 21),	//Confusion
			new MoveLevelPair(24, 59),	//Headbutt
			new MoveLevelPair(29, 93),	//Poison Gas
			new MoveLevelPair(32, 98),	//Psychic
			new MoveLevelPair(37, 79)	//Meditate
		},
		//97 - Hypno
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 96),	//Pound
			new MoveLevelPair(1, 66),	//Hypnosis
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(1, 21),	//Confusion
			new MoveLevelPair(12, 29),	//Disable
			new MoveLevelPair(17, 21),	//Confusion
			new MoveLevelPair(24, 59),	//Headbutt
			new MoveLevelPair(33, 93),	//Poison Gas
			new MoveLevelPair(37, 98),	//Psychic
			new MoveLevelPair(43, 79)	//Meditate
		},
		//98 - Krabby
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 16),	//Bubble
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(20, 158),	//Vice Grip
			new MoveLevelPair(25, 55),	//Guillotine
			new MoveLevelPair(30, 132),	//Stomp
			new MoveLevelPair(35, 25),	//Crabhammer
			new MoveLevelPair(40, 57)	//Harden
		},
		//99 - Kingler
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 16),	//Bubble
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 158),	//Vice Grip
			new MoveLevelPair(20, 158),	//Vice Grip
			new MoveLevelPair(25, 55),	//Guillotine
			new MoveLevelPair(34, 132),	//Stomp
			new MoveLevelPair(42, 25),	//Crabhammer
			new MoveLevelPair(49, 57)	//Harden
		},
		//100 - Voltorb
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 113),	//Screech
			new MoveLevelPair(17, 128),	//Sonic Boom
			new MoveLevelPair(22, 115),	//Self-Destruct
			new MoveLevelPair(29, 76),	//Light Screen
			new MoveLevelPair(36, 142),	//Swift
			new MoveLevelPair(43, 41)	//Explosion
		},
		//101 - Electrode
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 113),	//Screech
			new MoveLevelPair(1, 128),	//Sonic Boom
			new MoveLevelPair(17, 128),	//Sonic Boom
			new MoveLevelPair(22, 115),	//Self-Destruct
			new MoveLevelPair(29, 76),	//Light Screen
			new MoveLevelPair(40, 142),	//Swift
			new MoveLevelPair(50, 41)	//Explosion
		},
		//102 - Exeggcute
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 7),	//Barrage
			new MoveLevelPair(1, 66),	//Hypnosis
			new MoveLevelPair(25, 105),	//Reflect
			new MoveLevelPair(28, 73),	//Leech Seed
			new MoveLevelPair(32, 136),	//Stun Spore
			new MoveLevelPair(37, 94),	//Poison Powder
			new MoveLevelPair(42, 127),	//Solar Beam
			new MoveLevelPair(48, 122)	//Sleep Powder
		},
		//103 - Exeggutor
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 7),	//Barrage
			new MoveLevelPair(1, 66),	//Hypnosis
			new MoveLevelPair(28, 132)	//Stomp
		},
		//104 - Cubone
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 14),	//Bone Club
			new MoveLevelPair(25, 74),	//Leer
			new MoveLevelPair(31, 49),	//Focus Energy
			new MoveLevelPair(38, 148),	//Thrash
			new MoveLevelPair(43, 15),	//Bonemerang
			new MoveLevelPair(46, 101)	//Rage
		},
		//105 - Marowak
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 14),	//Bone Club
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 49),	//Focus Energy
			new MoveLevelPair(25, 74),	//Leer
			new MoveLevelPair(33, 49),	//Focus Energy
			new MoveLevelPair(41, 148),	//Thrash
			new MoveLevelPair(48, 15),	//Bonemerang
			new MoveLevelPair(55, 101)	//Rage
		},
		//106 - Hitmonlee
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 31),	//Double Kick
			new MoveLevelPair(1, 79),	//Meditate
			new MoveLevelPair(33, 110),	//Rolling Kick
			new MoveLevelPair(38, 69),	//Jump Kick
			new MoveLevelPair(43, 49),	//Focus Energy
			new MoveLevelPair(48, 60),	//High Jump Kick
			new MoveLevelPair(53, 81)	//Mega Kick
		},
		//107 - Hitmonchan
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 19),	//Comet Punch
			new MoveLevelPair(1, 4),	//Agility
			new MoveLevelPair(33, 43),	//Fire Punch
			new MoveLevelPair(38, 68),	//Ice Punch
			new MoveLevelPair(43, 150),	//Thunder Punch
			new MoveLevelPair(48, 82),	//Mega Punch
			new MoveLevelPair(53, 24)	//Counter
		},
		//108 - Lickitung
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 165),	//Wrap
			new MoveLevelPair(1, 140),	//Supersonic
			new MoveLevelPair(7, 132),	//Stomp
			new MoveLevelPair(15, 29),	//Disable
			new MoveLevelPair(23, 27),	//Defense Curl
			new MoveLevelPair(31, 120),	//Slam
			new MoveLevelPair(39, 113)	//Screech
		},
		//109 - Koffing
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 124),	//Smog
			new MoveLevelPair(32, 123),	//Sludge
			new MoveLevelPair(37, 125),	//Smokescreen
			new MoveLevelPair(40, 115),	//Self-Destruct
			new MoveLevelPair(45, 58),	//Haze
			new MoveLevelPair(48, 41)	//Explosion
		},
		//110 - Weezing
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 124),	//Smog
			new MoveLevelPair(1, 123),	//Sludge
			new MoveLevelPair(32, 123),	//Sludge
			new MoveLevelPair(39, 125),	//Smokescreen
			new MoveLevelPair(43, 115),	//Self-Destruct
			new MoveLevelPair(49, 58),	//Haze
			new MoveLevelPair(53, 41)	//Explosion
		},
		//111 - Rhyhorn
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 61),	//Horn Attack
			new MoveLevelPair(30, 132),	//Stomp
			new MoveLevelPair(35, 145),	//Tail Whip
			new MoveLevelPair(40, 50),	//Fury Attack
			new MoveLevelPair(45, 62),	//Horn Drill
			new MoveLevelPair(50, 74),	//Leer
			new MoveLevelPair(55, 146)	//Take Down
		},
		//112 - Rhydon
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 61),	//Horn Attack
			new MoveLevelPair(1, 132),	//Stomp
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(1, 50),	//Fury Attack
			new MoveLevelPair(30, 132),	//Stomp
			new MoveLevelPair(35, 145),	//Tail Whip
			new MoveLevelPair(40, 50),	//Fury Attack
			new MoveLevelPair(48, 62),	//Horn Drill
			new MoveLevelPair(55, 74),	//Leer
			new MoveLevelPair(64, 146)	//Take Down
		},
		//113 - Chansey
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 96),	//Pound
			new MoveLevelPair(1, 32),	//Double Slap
			new MoveLevelPair(24, 117),	//Sing
			new MoveLevelPair(30, 53),	//Growl
			new MoveLevelPair(38, 85),	//Minimize
			new MoveLevelPair(44, 27),	//Defense Curl
			new MoveLevelPair(48, 76),	//Light Screen
			new MoveLevelPair(54, 34)	//Double-Edge
		},
		//114 - Tangela
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 22),	//Constrict
			new MoveLevelPair(1, 10),	//Bind
			new MoveLevelPair(29, 1),	//Absorb
			new MoveLevelPair(32, 94),	//Poison Powder
			new MoveLevelPair(36, 136),	//Stun Spore
			new MoveLevelPair(39, 122),	//Sleep Powder
			new MoveLevelPair(45, 120),	//Slam
			new MoveLevelPair(49, 54)	//Growth
		},
		//115 - Kangaskhan
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 19),	//Comet Punch
			new MoveLevelPair(1, 101),	//Rage
			new MoveLevelPair(26, 11),	//Bite
			new MoveLevelPair(31, 145),	//Tail Whip
			new MoveLevelPair(36, 82),	//Mega Punch
			new MoveLevelPair(41, 74),	//Leer
			new MoveLevelPair(46, 30)	//Dizzy Punch
		},
		//116 - Horsea
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 16),	//Bubble
			new MoveLevelPair(19, 125),	//Smokescreen
			new MoveLevelPair(24, 74),	//Leer
			new MoveLevelPair(30, 160),	//Water Gun
			new MoveLevelPair(37, 4),	//Leer
			new MoveLevelPair(45, 63)	//Hydro Pump
		},
		//117 - Seadra
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 16),	//Bubble
			new MoveLevelPair(1, 125),	//Smokescreen
			new MoveLevelPair(19, 125),	//Smokescreen
			new MoveLevelPair(24, 74),	//Leer
			new MoveLevelPair(30, 160),	//Water Gun
			new MoveLevelPair(41, 4),	//Leer
			new MoveLevelPair(52, 63)	//Hydro Pump
		},
		//118 - Goldeen
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 90),	//Peck
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(19, 140),	//Supersonic
			new MoveLevelPair(24, 61),	//Horn Attack
			new MoveLevelPair(30, 50),	//Fury Attack
			new MoveLevelPair(37, 161),	//Waterfall
			new MoveLevelPair(45, 62),	//Horn Drill
			new MoveLevelPair(54, 4)	//Agility
		},
		//119 - Seaking
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 90),	//Peck
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(1, 140),	//Supersonic
			new MoveLevelPair(19, 140),	//Supersonic
			new MoveLevelPair(24, 61),	//Horn Attack
			new MoveLevelPair(30, 50),	//Fury Attack
			new MoveLevelPair(39, 161),	//Waterfall
			new MoveLevelPair(48, 62),	//Horn Drill
			new MoveLevelPair(54, 4)	//Agility
		},
		//120 - Staryu
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 145),	//Tail Whip
			new MoveLevelPair(17, 160),	//Water Gun
			new MoveLevelPair(22, 57),	//Harden
			new MoveLevelPair(27, 104),	//Recover
			new MoveLevelPair(32, 142),	//Swift
			new MoveLevelPair(37, 85),	//Minimize
			new MoveLevelPair(42, 76),	//Light Screen
			new MoveLevelPair(47, 63)	//Hydro Pump
		},
		//121 - Starmie
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 160),	//Water Gun
			new MoveLevelPair(1, 57)	//Harden
		},
		//122 - Mr.Mime
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 21),	//Confusion
			new MoveLevelPair(1, 8),	//Barrier
			new MoveLevelPair(15, 21),	//Confusion
			new MoveLevelPair(23, 76),	//Light Screen
			new MoveLevelPair(31, 32),	//Double Slap
			new MoveLevelPair(39, 79),	//Meditate
			new MoveLevelPair(47, 138)	//Substitute
		},
		//123 - Scyther
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 100),	//Quick Attack
			new MoveLevelPair(17, 74),	//Leer
			new MoveLevelPair(20, 49),	//Focus Energy
			new MoveLevelPair(24, 33),	//Double Team
			new MoveLevelPair(29, 121),	//Slash
			new MoveLevelPair(35, 143),	//Swords Dance
			new MoveLevelPair(42, 4)	//Agility
		},
		//124 - Jynx
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 96),	//Pound
			new MoveLevelPair(1, 77),	//Lovely Kiss
			new MoveLevelPair(18, 75),	//Lick
			new MoveLevelPair(23, 32),	//Double Slap
			new MoveLevelPair(31, 68),	//Ice Punch
			new MoveLevelPair(39, 13),	//Body Slam
			new MoveLevelPair(47, 148),	//Thrash
			new MoveLevelPair(58, 12)	//Blizzard
		},
		//125 - Electabuzz
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 100),	//Quick Attack
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(34, 151),	//Thunder Shock
			new MoveLevelPair(37, 113),	//Screech
			new MoveLevelPair(42, 150),	//Thunder Punch
			new MoveLevelPair(49, 76),	//Light Screen
			new MoveLevelPair(54, 149)	//Thunder
		},
		//126 - Magmar
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 40),	//Ember
			new MoveLevelPair(36, 74),	//Leer
			new MoveLevelPair(39, 20),	//Confuse Ray
			new MoveLevelPair(43, 43),	//Fire Punch
			new MoveLevelPair(48, 125),	//Smokescreen
			new MoveLevelPair(52, 124),	//Smog
			new MoveLevelPair(55, 46)	//Flamethrower
		},
		//127 - Pinsir
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 158),	//Vice Grip
			new MoveLevelPair(25, 114),	//Seismic Toss
			new MoveLevelPair(30, 55),	//Guillotine
			new MoveLevelPair(36, 49),	//Focus Energy
			new MoveLevelPair(43, 57),	//Harden
			new MoveLevelPair(49, 121),	//Slash
			new MoveLevelPair(54, 143)	//Swords Dance
		},
		//128 - Tauros
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(21, 132),	//Stomp
			new MoveLevelPair(28, 145),	//Tail Whipe
			new MoveLevelPair(35, 74),	//Leer
			new MoveLevelPair(44, 101),	//Rage
			new MoveLevelPair(51, 146)	//Take Down
		},
		//129 - Magikarp
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 130),	//Splash
			new MoveLevelPair(15, 144)	//Tackle
		},
		//130 - Gyarados
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 11),	//Bite
			new MoveLevelPair(1, 35),	//Dragon Rage
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 63),	//Hydro Pump
			new MoveLevelPair(20, 11),	//Bite
			new MoveLevelPair(25, 35),	//Dragon Rage
			new MoveLevelPair(32, 74),	//Leer
			new MoveLevelPair(41, 63),	//Hydro Pump
			new MoveLevelPair(52, 64)	//Hyper Beam
		},
		//131 - Lapras
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 160),	//Water Gun
			new MoveLevelPair(1, 53),	//Growl
			new MoveLevelPair(16, 117),	//Sing
			new MoveLevelPair(20, 87),	//Mist
			new MoveLevelPair(25, 13),	//Body Slam
			new MoveLevelPair(31, 20),	//Confuse Ray
			new MoveLevelPair(38, 67),	//Ice Beam
			new MoveLevelPair(46, 63)	//Hydro Pump
		},
		//132 - Ditto
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 155)	//Transform
		},
		//133 - Eevee
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 111),	//Sand Attack
			new MoveLevelPair(27, 100),	//Quick Attack
			new MoveLevelPair(31, 145),	//Tail Whip
			new MoveLevelPair(37, 11),	//Bite
			new MoveLevelPair(45, 146)	//Take Down
		},
		//134 - Vaporeon
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 100),	//Quick Attack
			new MoveLevelPair(1, 160),	//Water Gun
			new MoveLevelPair(1, 111),	//Sand Attack
			new MoveLevelPair(27, 100),	//Quick Attack
			new MoveLevelPair(31, 160),	//Water Gun
			new MoveLevelPair(37, 145),	//Tail Whip
			new MoveLevelPair(40, 11),	//Bite
			new MoveLevelPair(42, 3),	//Acid Armor
			new MoveLevelPair(44, 58),	//Haze
			new MoveLevelPair(48, 87),	//Mist
			new MoveLevelPair(54, 63)	//Hydro Pump
		},
		//135 - Jolteon
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 100),	//Quick Attack
			new MoveLevelPair(1, 151),	//Thunder Shock
			new MoveLevelPair(1, 111),	//Sand Attack
			new MoveLevelPair(27, 100),	//Quick Attack
			new MoveLevelPair(31, 151),	//Thunder Shock
			new MoveLevelPair(37, 145),	//Tail Whip
			new MoveLevelPair(40, 152),	//Thunder Wave
			new MoveLevelPair(42, 31),	//Double Kick
			new MoveLevelPair(44, 4),	//Agility
			new MoveLevelPair(48, 92),	//Pin Missle
			new MoveLevelPair(54, 149)	//Thunder
		},
		//136 - Flareon
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 100),	//Quick Attack
			new MoveLevelPair(1, 40),	//Ember
			new MoveLevelPair(1, 111),	//Sand Attack
			new MoveLevelPair(27, 100),	//Quick Attack
			new MoveLevelPair(31, 40),	//Ember
			new MoveLevelPair(37, 145),	//Tail Whip
			new MoveLevelPair(40, 11),	//Bite
			new MoveLevelPair(42, 74),	//Leer
			new MoveLevelPair(44, 44),	//Fire Spin
			new MoveLevelPair(48, 101),	//Rage
			new MoveLevelPair(54, 46)	//Flamethrower
		},
		//137 - Porygon
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 144),	//Tackle
			new MoveLevelPair(1, 116),	//Sharpen
			new MoveLevelPair(1, 23),	//Conversion
			new MoveLevelPair(23, 97),	//Psybeam
			new MoveLevelPair(28, 104),	//Recover
			new MoveLevelPair(35, 4),	//Agility
			new MoveLevelPair(42, 156)	//Tri Attack
		},
		//138 - Omanyte
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 160),	//Water Gun
			new MoveLevelPair(1, 164),	//Withdraw
			new MoveLevelPair(34, 61),	//Horn Attack
			new MoveLevelPair(39, 74),	//Leer
			new MoveLevelPair(46, 129),	//Spike Cannon
			new MoveLevelPair(53, 63)	//Hydro Pump
		},
		//139 - Omastar
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 160),	//Water Gun
			new MoveLevelPair(1, 164),	//Withdraw
			new MoveLevelPair(1, 61),	//Horn Attack
			new MoveLevelPair(34, 61),	//Horn Attack
			new MoveLevelPair(39, 74),	//Leer
			new MoveLevelPair(44, 129),	//Spike Cannon
			new MoveLevelPair(49, 63)	//Hydro Pump
		},
		//140 - Kabuto
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 57),	//Harden
			new MoveLevelPair(34, 1),	//Absorb
			new MoveLevelPair(39, 121),	//Slash
			new MoveLevelPair(44, 74),	//Leer
			new MoveLevelPair(49, 63)	//Hydro Pump
		},
		//141 - Kabutops
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 112),	//Scratch
			new MoveLevelPair(1, 57),	//Harden
			new MoveLevelPair(1, 1),	//Absorb
			new MoveLevelPair(34, 1),	//Absorb
			new MoveLevelPair(39, 121),	//Slash
			new MoveLevelPair(46, 74),	//Leer
			new MoveLevelPair(53, 63)	//Hydro Pump
		},
		//142 - Aerodactyl
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 163),	//Wing Attack
			new MoveLevelPair(1, 4),	//Agility
			new MoveLevelPair(33, 140),	//Supersonic
			new MoveLevelPair(38, 11),	//Bite
			new MoveLevelPair(45, 146),	//Take Down
			new MoveLevelPair(54, 64)	//Hyper Beam
		},
		//143 - Snorlax
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 59),	//Headbutt
			new MoveLevelPair(1, 5),	//Amnesia
			new MoveLevelPair(1, 106),	//Rest
			new MoveLevelPair(35, 13),	//Body Slam
			new MoveLevelPair(41, 57),	//Harden
			new MoveLevelPair(48, 34),	//Double-Edge
			new MoveLevelPair(56, 64)	//Hyper Beam
		},
		//144 - Articuno
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 90),	//Peck
			new MoveLevelPair(1, 67),	//Ice Beam
			new MoveLevelPair(51, 12),	//Blizzard
			new MoveLevelPair(55, 4),	//Agility
			new MoveLevelPair(60, 87)	//Mist
		},
		//145 - Zapdos
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 151),	//Thunder Shock
			new MoveLevelPair(1, 37),	//Drill Peck
			new MoveLevelPair(51, 149),	//Thunder
			new MoveLevelPair(55, 4),	//Agility
			new MoveLevelPair(60, 76)	//Light Screen
		},
		//146 - Moltres
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 90),	//Peck
			new MoveLevelPair(1, 44),	//Fire Spin
			new MoveLevelPair(51, 74),	//Leer
			new MoveLevelPair(55, 4),	//Agility
			new MoveLevelPair(60, 119)	//Sky Attack
		},
		//147 - Dratini
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 165),	//Wrap
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(10, 152),	//Thunder Wave
			new MoveLevelPair(20, 4),	//Agility
			new MoveLevelPair(30, 120),	//Slam
			new MoveLevelPair(40, 35),	//Dragon Rage
			new MoveLevelPair(50, 64)	//Hyper Beam
		},
		//148 - Dragonair
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 165),	//Wrap
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 152),	//Thunder Wave
			new MoveLevelPair(10, 152),	//Thunder Wave
			new MoveLevelPair(20, 4),	//Agility
			new MoveLevelPair(35, 120),	//Slam
			new MoveLevelPair(45, 35),	//Dragon Rage
			new MoveLevelPair(55, 64)	//Hyper Beam
		},
		//149 - Dragonite
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 165),	//Wrap
			new MoveLevelPair(1, 74),	//Leer
			new MoveLevelPair(1, 152),	//Thunder Wave
			new MoveLevelPair(1, 4),	//Agility
			new MoveLevelPair(10, 152),	//Thunder Wave
			new MoveLevelPair(20, 4),	//Agility
			new MoveLevelPair(35, 120),	//Slam
			new MoveLevelPair(45, 35),	//Dragon Rage
			new MoveLevelPair(60, 64)	//Hyper Beam
		},
		//150 - Mewtwo
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 21),	//Confusion
			new MoveLevelPair(1, 29),	//Disable
			new MoveLevelPair(1, 142),	//Swift
			new MoveLevelPair(1, 98),	//Psychic
			new MoveLevelPair(63, 8),	//Barrier
			new MoveLevelPair(66, 98),	//Psychic
			new MoveLevelPair(70, 104),	//Recover
			new MoveLevelPair(75, 87),	//Mist
			new MoveLevelPair(81, 5)	//Amnesia
		},
		//151 - Mew
		new MoveLevelPair[]
		{
			new MoveLevelPair(1, 96),	//Pound
			new MoveLevelPair(10, 155),	//Transform
			new MoveLevelPair(20, 82),	//Mega Punch
			new MoveLevelPair(30, 83),	//Metronome
			new MoveLevelPair(40, 98)	//Psychic
		},
	};
}

//A Move-Level Pair consists of the index for a move, and the level at which it is learned
public class MoveLevelPair
{
    public int Level;
    public int MoveIndex;

    public MoveLevelPair(int level, int moveIndex)
    {
        Level = level;
        MoveIndex = moveIndex;
    }
}