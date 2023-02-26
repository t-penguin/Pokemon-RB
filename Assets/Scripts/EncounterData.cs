using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EncounterData
{
    #region Encounter Slots

    /* Area Encounter Arrays contain 10 possible Pokemon to encounter
     * [x, 0] - refers to the Pokemon's Pokedex number
     * [x, 1] - refers to the Pokemon's level
     * x values correspond to encounter slots with approximate percentages of:
     * 0-1: 20%, 2: 15%, 3-5: 10%, 6-7: 5%, 8: 4%, 9: 1%
     * For areas that have different encounters based on game version,
     * two arrays exist suffixed by R or B for the game version */

    // 16 - Pidgey, 19 - Rattata
    public static int[,] Route1 = new int[,]
    {{16, 3 }, {19, 3 }, {19, 3 }, {19, 2 }, {16, 2 }, {16, 3 }, {16, 3 }, {19, 4 }, {16, 4 }, {16, 5 }};

    // 10 - Caterpie, 13 - Weedle, 16 - Pidgey, 19 - Rattata
    public static int[,] Route2R = new int[,]
    {{19, 3 }, {16, 3 }, {16, 4 }, {19, 4 }, {16, 5 }, {13, 3 }, {19, 2 }, {19, 5 }, {13, 4 }, {13, 5 }};
    public static int[,] Route2B = new int[,]
    {{19, 3 }, {16, 3 }, {16, 4 }, {19, 4 }, {16, 5 }, {10, 3 }, {19, 2 }, {19, 5 }, {10, 4 }, {10, 5 }};

    // 16 - Pidgey, 21 - Spearow, 39 - Jigglypuff
    public static int[,] Route3 = new int[,]
    {{16, 6 }, {21, 5 }, {16, 7 }, {21, 6 }, {21, 7 }, {16, 8 }, {21, 8 }, {39, 3 }, {39, 5 }, {39, 7 }};

    // 19 - Rattata, 21 - Spearow, 23 - Ekans, 27 - Sandshrew
    public static int[,] Route4R = new int[,]
    {{19, 10}, {21, 10 }, {19, 8 }, {23, 6 }, {21, 8 }, {23, 10 }, {19, 12 }, {21, 12 }, {23, 8 }, {23, 12 }};
    public static int[,] Route4B = new int[,]
    {{19, 10}, {21, 10 }, {19, 8 }, {27, 6 }, {21, 8 }, {27, 10 }, {19, 12 }, {21, 12 }, {27, 8 }, {27, 12 }};

    // 16 - Pidgey, 43 - Oddish, 52 - Meowth, 56 - Mankey, 69 - Bellsprout
    public static int[,] Route5R = new int[,]
    {{43, 13 }, {16, 13 }, {16, 15 }, {56, 10 }, {56, 12 }, {43, 15 }, {43, 16 }, {16, 16 }, {56, 14 }, {56, 16 }};
    public static int[,] Route5B = new int[,]
    {{69, 13 }, {16, 13 }, {16, 15 }, {52, 10 }, {52, 12 }, {69, 15 }, {69, 16 }, {16, 16 }, {52, 14 }, {52, 16 }};

    // 16 - Pidgey, 43 - Oddish, 52 - Meowth, 56 - Mankey, 69 - Bellsprout
    public static int[,] Route6R = new int[,]
    {{43, 13 }, {16, 13 }, {16, 15 }, {56, 10 }, {56, 12 }, {43, 15 }, {43, 16 }, {16, 16 }, {56, 14 }, {56, 16 }};
    public static int[,] Route6B = new int[,]
    {{69, 13 }, {16, 13 }, {16, 15 }, {52, 10 }, {52, 12 }, {69, 15 }, {69, 16 }, {16, 16 }, {52, 14 }, {52, 16 }};

    // 16 - Pidgey, 37 - Vulpix, 43 - Oddish, 52 - Meowth, 56 - Mankey, 58 - Growlithe, 69 - Bellsprout
    public static int[,] Route7R = new int[,]
    {{16, 19 }, {43, 19 }, {56, 17 }, {43, 22 }, {16, 22 }, {56, 18 }, {58, 18 }, {58, 20 }, {56, 19 }, {56, 20 }};
    public static int[,] Route7B = new int[,]
    {{16, 19 }, {69, 19 }, {52, 17 }, {69, 22 }, {16, 22 }, {52, 18 }, {37, 18 }, {37, 20 }, {52, 19 }, {52, 20 }};

    // 16 - Pidgey, 23 - Ekans, 27 - Sandshrew, 37 - Vulpix, 52 - Meowth, 56 - Mankey, 58 - Growlithe
    public static int[,] Route8R = new int[,]
    {{16, 18 }, {56, 18 }, {23, 17 }, {58, 16 }, {16, 20 }, {56, 20 }, {23, 19 }, {58, 17 }, {58, 15 }, {58, 18 }};
    public static int[,] Route8B = new int[,]
    {{16, 18 }, {52, 18 }, {27, 17 }, {37, 16 }, {16, 20 }, {52, 20 }, {27, 19 }, {37, 17 }, {37, 15 }, {37, 18 }};

    // 19 - Rattata, 21 - Spearow, 23 - Ekans, 27 - Sandshrew
    public static int[,] Route9R = new int[,]
    {{19, 16 }, {21, 16 }, {19, 14 }, {23, 11 }, {21, 13 }, {23, 15 }, {19, 17 }, {21, 17 }, {23, 13 }, {23, 17 }};
    public static int[,] Route9B = new int[,]
    {{19, 16 }, {21, 16 }, {19, 14 }, {27, 11 }, {21, 13 }, {27, 15 }, {19, 17 }, {21, 17 }, {27, 13 }, {27, 17 }};

    // 21 - Spearow, 23 - Ekans, 27 - Sandshrew, 100 - Voltorb
    public static int[,] Route10R = new int[,]
    {{100, 16 }, {21, 16 }, {100, 14 }, {23, 11 }, {21, 13 }, {23, 15 }, {100, 17 }, {21, 17 }, {23, 13 }, {23, 17 }};
    public static int[,] Route10B = new int[,]
    {{100, 16 }, {21, 16 }, {100, 14 }, {27, 11 }, {21, 13 }, {27, 15 }, {100, 17 }, {21, 17 }, {27, 13 }, {27, 17 }};

    // 21 - Spearow, 23 - Ekans, 27 - Sandshrew, 96 - Drowzee
    public static int[,] Route11R = new int[,]
    {{23, 14 }, {21, 15 }, {23, 12 },  {96, 9 }, {21, 13 }, {96, 13 }, {23, 15 }, {21, 17 }, {96, 11 }, {96, 15 }};
    public static int[,] Route11B = new int[,]
    {{27, 14 }, {21, 15 }, {27, 12 },  {96, 9 }, {21, 13 }, {96, 13 }, {27, 15 }, {21, 17 }, {96, 11 }, {96, 15 }};

    // 16 - Pidgey, 43 - Oddish, 44 - Gloom, 48 - Venonat, 69 - Bellsprout, 70 - Weepinbell
    public static int[,] Route12R = new int[,]
    {{43, 24 }, {16, 25 }, {16, 23 }, {48, 24 }, {43, 22 }, {48, 26 }, {43, 26 }, {16, 27 }, {44, 28 }, {44, 30 }};
    public static int[,] Route12B = new int[,]
    {{69, 24 }, {16, 25 }, {16, 23 }, {48, 24 }, {69, 22 }, {48, 26 }, {69, 26 }, {16, 27 }, {70, 28 }, {70, 30 }};

    // 16 - Pidgey, 43 - Oddish, 44 - Gloom, 48 - Venonat, 69 - Bellsprout, 70 - Weepinbell, 132 - Ditto
    public static int[,] Route13R = new int[,]
    {{43, 24 }, {16, 25 }, {16, 27 }, {48, 24 }, {43, 22 }, {48, 26 }, {43, 26 }, {132, 25 }, {44, 28 }, {44, 30 }};
    public static int[,] Route13B = new int[,]
    {{69, 24 }, {16, 25 }, {16, 27 }, {48, 24 }, {69, 22 }, {48, 26 }, {69, 26 }, {132, 25 }, {70, 28 }, {70, 30 }};

    // 16 - Pidgey, 17 - Pidgeotto, 43 - Oddish, 44 - Gloom, 48 - Venonat, 69 - Bellsprout, 70 - Weepinbell, 132 - Ditto
    public static int[,] Route14R = new int[,]
    {{43, 24 }, {16, 26 }, {132, 23 }, {48, 24 }, {43, 22 }, {48, 26 }, {43, 26 }, {44, 30 }, {17, 28 }, {17, 30 }};
    public static int[,] Route14B = new int[,]
    {{69, 24 }, {16, 26 }, {132, 23 }, {48, 24 }, {69, 22 }, {48, 26 }, {69, 26 }, {70, 30 }, {17, 28 }, {17, 30 }};

    // 16 - Pidgey, 17 - Pidgeotto, 43 - Oddish, 44 - Gloom, 48 - Venonat, 69 - Bellsprout, 70 - Weepinbell, 132 - Ditto
    public static int[,] Route15R = new int[,]
    {{43, 24 }, {132, 26 }, {16, 23 }, {48, 26 }, {43, 22 }, {48, 28 }, {43, 26 }, {44, 30 }, {17, 28 }, {17, 30 }};
    public static int[,] Route15B = new int[,]
    {{69, 24 }, {132, 26 }, {16, 23 }, {48, 26 }, {69, 22 }, {48, 28 }, {69, 26 }, {70, 30 }, {17, 28 }, {17, 30 }};

    // 19 - Rattata, 20 - Raticate, 21 - Spearow, 84 - Doduo
    public static int[,] Route16 = new int[,]
    {{21, 20 }, {21, 22 }, {19, 18 }, {84, 20 }, {19, 20 }, {84, 18 }, {84, 22 }, {19, 22 }, {20, 23 }, {20, 25 }};

    // 20 - Raticate, 21 - Spearow, 22 - Fearow, 84 - Doduo
    public static int[,] Route17 = new int[,]
    {{21, 20 }, {21, 22 }, {20, 25 }, {84, 24 }, {20, 27 }, {84, 26 }, {84, 28 }, {20, 29 }, {22, 25 }, {22, 27 }};

    // 20 - Raticate, 21 - Spearow, 22 - Fearow, 84 - Doduo
    public static int[,] Route18 = new int[,]
    {{21, 20 }, {21, 22 }, {20, 25 }, {84, 24 }, {22, 25 }, {84, 26 }, {84, 28 }, {20, 29 }, {22, 27 }, {22, 29 }};

    // 72 - Tentacool
    public static int[,] SeaRoutes19_20_21 = new int[,]
    {{72, 5 }, {72, 10 }, {72, 15 }, {72, 5 }, {72, 10 }, {72, 15 }, {72, 20 }, {72, 30 }, {72, 35 }, {72, 40 }};

    // 16 - Pidgey, 17 - Pidgeotto, 19 - Rattata, 20 - Raticate, 114 - Tangela
    public static int[,] Route21 = new int[,]
    {{19, 21 }, {16, 23 }, {20, 30 }, {19, 23 }, {16, 21 }, {17, 30 }, {17, 32 }, {114, 28 }, {114, 30 }, {114, 32 }};

    // 19 - Rattata, 21 - Spearow, 29 - NidoranF, 32 - NidoranM
    public static int[,] Rotue22R = new int[,]
    {{19, 3 }, {32, 3 }, {19, 4 }, {32, 4 }, {19, 2 }, {32, 2 }, {21, 3 }, {21, 5 }, {29, 2 }, {29, 4 }};
    public static int[,] Rotue22B = new int[,]
    {{19, 3 }, {29, 3 }, {19, 4 }, {29, 4 }, {19, 2 }, {29, 2 }, {21, 3 }, {21, 5 }, {32, 2 }, {32, 4 }};

    // 21 - Spearow, 22 - Fearow, 23 - Ekans, 24 - Arbok, 27 - Sandshrew, 28 - Sandslash, 132 - Ditto
    public static int[,] Route23R = new int[,]
    {{23, 26 }, {132, 33 }, {21, 26 }, {22, 38 }, {132, 38 }, {22, 38 }, {24, 41 }, {132, 43 }, {22, 41 }, {22, 43 }};
    public static int[,] Route23B = new int[,]
    {{27, 26 }, {132, 33 }, {21, 26 }, {22, 38 }, {132, 38 }, {22, 38 }, {28, 41 }, {132, 43 }, {22, 41 }, {22, 43 }};

    // 10 - Caterpie, 11 - Metapod, 13 - Weedle, 14 - Kakuna, 16 - Pidgey, 43 - Oddish, 63 - Abra, 69 - Bellsprout
    public static int[,] Route24R = new int[,]
    {{13, 7 }, {14, 8 }, {16, 12 }, {43, 12 }, {43, 13 }, {63, 10 }, {43, 14 }, {16, 13 }, {63, 8 }, {63, 12 }};
    public static int[,] Route24B = new int[,]
    {{10, 7 }, {11, 8 }, {16, 12 }, {69, 12 }, {69, 13 }, {63, 10 }, {69, 14 }, {16, 13 }, {63, 8 }, {63, 12 }};

    // 10 - Caterpie, 11 - Metapod, 13 - Weedle, 14 - Kakuna, 16 - Pidgey, 43 - Oddish, 63 - Abra, 69 - Bellsprout
    public static int[,] Route25R = new int[,]
    {{13, 8 }, {14, 9 }, {16, 13 }, {43, 12 }, {43, 13 }, {63, 12 }, {43, 14 }, {63, 10 }, {11, 7 }, {10, 8 }};
    public static int[,] Route25B = new int[,]
    {{10, 8 }, {11, 9 }, {16, 13 }, {69, 12 }, {69, 13 }, {63, 12 }, {69, 14 }, {63, 10 }, {14, 7 }, {13, 8 }};

    // 10 - Caterpie, 11 - Metapod, 13 - Weedle, 14 - Kakuna, 25 - Pikachu
    public static int[,] ViridianForestR = new int[,]
    {{13, 4 }, {14, 5 }, {13, 3 }, {13, 5 }, {14, 4 }, {14, 6 }, {11, 4 }, {10, 3 }, {25, 3 }, {25, 5 }};
    public static int[,] ViridianForestB = new int[,]
    {{10, 4 }, {11, 5 }, {10, 3 }, {10, 5 }, {11, 4 }, {11, 6 }, {14, 4 }, {13, 3 }, {25, 3 }, {25, 5 }};

    // 35 - Clefairy, 41 - Zubat, 46 - Paras, 74 - Geodude
    public static int[,] MtMoon1F = new int[,]
    {{41, 8 }, {41, 7 }, {41, 9 }, {74, 8 }, {41, 6 }, {41, 10 }, {74, 10 }, {46, 8 }, {41, 11 }, {35, 8 }};
    public static int[,] MtMoonB1F = new int[,]
    {{41, 8 }, {41, 7 }, {74, 7 }, {74, 8 }, {41, 9 }, {46, 10 }, {41, 10 }, {41, 11 }, {35, 9 }, {74, 9 }};
    public static int[,] MtMoonB2F = new int[,]
    {{41, 9 }, {74, 9 }, {41, 10 }, {74, 10 }, {41, 11 }, {46, 10 }, {46, 12 }, {35, 10 }, {41, 12 }, {35, 12 }};

    // 50 - Diglett, 51 - Dugtrio
    public static int[,] DiglettsCave = new int[,]
    {{50, 18 }, {50, 19 }, {50, 17 }, {50, 20 }, {50, 16 }, {50, 15 }, {50, 21 }, {50, 22 }, {51, 29 }, {51, 31 }};

    // 41 - Zubat, 66 - Machop, 74 - Geodude, 95 - Onix
    public static int[,] RockTunnel1F = new int[,]
    {{41, 16 }, {41, 17 }, {74, 17 }, {66, 15 }, {74, 16 }, {41, 18 }, {41, 15 }, {66, 17 }, {95, 13 }, {95, 15 }};
    public static int[,] RockTunnelB1F = new int[,]
    {{41, 16 }, {41, 17 }, {74, 17 }, {66, 15 }, {74, 16 }, {41, 18 }, {66, 17 }, {95, 17 }, {95, 13 }, {66, 18 }};

    // 25 - Pikachu, 26 - Raichu, 81 - Magnemite, 82 - Magneton, 100 - Voltorb, 125 - Electabuzz
    public static int[,] PowerPlantR = new int[,]
    {{100, 21 }, {81, 21 }, {25, 20 }, {25, 24 }, {81, 23 }, {100, 23 }, {82, 32 }, {82, 35 }, {125, 33 }, {125, 36 }};
    public static int[,] PowerPlantB = new int[,]
    {{100, 21 }, {81, 21 }, {25, 20 }, {25, 24 }, {81, 23 }, {100, 23 }, {82, 32 }, {82, 35 }, {26, 33 }, {26, 36 }};

    // 92 - Gastly, 93 - Haunter, 104 - Cubone
    public static int[,] PokemonTower3F = new int[,]
    {{92, 20 }, {92, 21 }, {92, 22 }, {92, 23 }, {92, 19 }, {92, 18 }, {92, 24 }, {104, 20 }, {104, 22 }, {93, 25 }};
    public static int[,] PokemonTower4F = new int[,]
    {{92, 20 }, {92, 21 }, {92, 22 }, {92, 23 }, {92, 19 }, {92, 18 }, {93, 25 }, {104, 20 }, {104, 22 }, {92, 24 }};
    public static int[,] PokemonTower5F = new int[,]
    {{92, 20 }, {92, 21 }, {92, 22 }, {92, 23 }, {92, 19 }, {92, 18 }, {93, 25 }, {104, 20 }, {104, 22 }, {92, 24 }};
    public static int[,] PokemonTower6F = new int[,]
    {{92, 21 }, {92, 22 }, {92, 23 }, {92, 24 }, {92, 20 }, {92, 19 }, {93, 26 }, {104, 22 }, {104, 24 }, {93, 28 }};
    public static int[,] PokemonTower7F = new int[,]
    {{92, 21 }, {92, 22 }, {92, 23 }, {92, 24 }, {92, 20 }, {93, 28 }, {104, 22 }, {104, 24 }, {93, 28 }, {93, 30 }};

    // 29 - NidoranF, 30 - Nidorina, 32 - NidoranM, 33 - Nidorino, 46 - Paras,
    // 47 - Parasect, 48 - Venonat, 49 - Venomoth, 84 - Doduo, 102 - Exeggcute, 
    // 111 - Rhyhorn, 113 - Chansey, 115 - Kangaskhan, 123 - Scyther, 127 - Pinsir, 128 - Tauros
    public static int[,] SafariZoneCenterR = new int[,]
    {{32, 22 }, {111, 25 }, {48, 22 }, {102, 24 }, {33, 31 }, {102, 25 }, {30, 31 }, {47, 30 }, {123, 23 }, {113, 23 }};
    public static int[,] SafariZoneCenterB = new int[,]
    {{29, 22 }, {111, 25 }, {48, 22 }, {102, 24 }, {30, 31 }, {102, 25 }, {33, 31 }, {47, 30 }, {127, 23 }, {113, 23 }};

    public static int[,] SafariZoneArea1R = new int[,]
    {{32, 24 }, {84, 26 }, {46, 22 }, {102, 25 }, {33, 33 }, {102, 23 }, {29, 24 }, {47, 25 }, {115, 25 }, {123, 28 }};
    public static int[,] SafariZoneArea1B = new int[,]
    {{29, 24 }, {84, 26 }, {46, 22 }, {102, 25 }, {30, 33 }, {102, 23 }, {32, 24 }, {47, 25 }, {115, 25 }, {127, 28 }};

    public static int[,] SafariZoneArea2R = new int[,]
    {{32, 22 }, {111, 26 }, {46, 23 }, {102, 25 }, {33, 30 }, {102, 27 }, {30, 30 }, {49, 32 }, {113, 26 }, {128, 28 }};
    public static int[,] SafariZoneArea2B = new int[,]
    {{29, 22 }, {111, 26 }, {46, 23 }, {102, 25 }, {30, 30 }, {102, 27 }, {33, 30 }, {49, 32 }, {113, 26 }, {128, 28 }};

    public static int[,] SafariZoneArea3R = new int[,]
    {{32, 25 }, {84, 26 }, {48, 23 }, {102, 24 }, {33, 33 }, {102, 26 }, {29, 25 }, {49, 31 }, {128, 26 }, {115, 28 }};
    public static int[,] SafariZoneArea3B = new int[,]
    {{29, 25 }, {84, 26 }, {48, 23 }, {102, 24 }, {30, 33 }, {102, 26 }, {32, 25 }, {49, 31 }, {128, 26 }, {115, 28 }};

    // 41 - Zubat, 42 - Golbat, 54 - Psyduck, 55 - Golduck, 79 - Slowpoke, 80 - Slowbro,
    // 86 - Seel, 87 - Dewgong, 90 - Shellder, 98 - Krabby, 116 - Horsea, 117 - Seadra, 120 - Staryu
    public static int[,] SeafoamIslands1FR = new int[,]
    {{86, 30 }, {79, 30 }, {90, 30 }, {116, 30 }, {116, 28 }, {41, 21 }, {42, 29 }, {54, 28 }, {90, 28 }, {55, 38 }};
    public static int[,] SeafoamIslands1FB = new int[,]
    {{86, 30 }, {54, 30 }, {120, 30 }, {98, 30 }, {98, 28 }, {41, 21 }, {42, 29 }, {79, 28 }, {120, 28 }, {80, 38 }};

    public static int[,] SeafoamIslandsB1FR = new int[,]
    {{120, 30 }, {116, 30 }, {90, 32 }, {116, 32 }, {79, 28 }, {86, 30 }, {79, 30 }, {86, 28 }, {87, 38 }, {117, 37 }};
    public static int[,] SeafoamIslandsB1FB = new int[,]
    {{90, 30 }, {98, 30 }, {120, 32 }, {98, 32 }, {79, 28 }, {86, 30 }, {79, 30 }, {86, 28 }, {87, 38 }, {99, 37 }};

    public static int[,] SeafoamIslandsB2FR = new int[,]
    {{86, 30 }, {79, 30 }, {86, 32 }, {79, 32 }, {116, 28 }, {120, 30 }, {116, 30 }, {90, 28 }, {42, 30 }, {80, 37 }};
    public static int[,] SeafoamIslandsB2FB = new int[,]
    {{86, 30 }, {54, 30 }, {86, 32 }, {54, 32 }, {98, 28 }, {90, 30 }, {98, 30 }, {120, 28 }, {42, 30 }, {55, 37 }};

    public static int[,] SeafoamIslandsB3FR = new int[,]
    {{79, 31 }, {86, 31 }, {79, 33 }, {86, 33 }, {116, 29 }, {90, 31 }, {116, 31 }, {90, 29 }, {117, 39 }, {87, 37 }};
    public static int[,] SeafoamIslandsB3FB = new int[,]
    {{54, 31 }, {86, 31 }, {54, 33 }, {86, 33 }, {98, 29 }, {120, 31 }, {98, 31 }, {120, 29 }, {99, 39 }, {87, 37 }};

    public static int[,] SeafoamIslandsB4FR = new int[,]
    {{116, 31 }, {90, 31 }, {116, 33 }, {90, 31 }, {79, 29 }, {86, 31 }, {79, 31 }, {86, 29 }, {80, 39 }, {42, 32 }};
    public static int[,] SeafoamIslandsB4FB = new int[,]
    {{98, 31 }, {120, 31 }, {98, 33 }, {120, 33 }, {54, 29 }, {86, 31 }, {54, 31 }, {86, 29 }, {55, 39 }, {55, 32 }};

    // 37 - Vulpix, 58 - Growlithe, 77 - Ponyta, 88 - Grimer, 89 - Muk, 109 - Koffing, 110 - Weezing, 126 - Magmar
    public static int[,] PokemonMansion1FR = new int[,]
    {{109, 32 }, {109, 30 }, {77, 34 }, {77, 30 }, {58, 34 }, {77, 32 }, {88, 30 }, {77, 28 }, {110, 37 }, {89, 39 }};
    public static int[,] PokemonMansion1FB = new int[,]
    {{109, 32 }, {109, 30 }, {77, 34 }, {77, 30 }, {37, 34 }, {77, 32 }, {88, 30 }, {77, 28 }, {89, 37 }, {110, 39 }};

    public static int[,] PokemonMansion2FR = new int[,]
    {{58, 32 }, {109, 34 }, {109, 34 }, {77, 30 }, {109, 30 }, {77, 32 }, {88, 30 }, {77, 28 }, {110, 39 }, {89, 37 }};
    public static int[,] PokemonMansion2FB = new int[,]
    {{37, 32 }, {88, 34 }, {88, 34 }, {77, 30 }, {88, 30 }, {77, 32 }, {109, 30 }, {77, 28 }, {89, 39 }, {110, 37 }};

    public static int[,] PokemonMansion3FR = new int[,]
    {{109, 31 }, {58, 33 }, {109, 35 }, {77, 32 }, {77, 34 }, {110, 40 }, {88, 34 }, {110, 38 }, {77, 36 }, {89, 42 }};
    public static int[,] PokemonMansion3FB = new int[,]
    {{88, 31 }, {37, 33 }, {88, 35 }, {77, 32 }, {126, 34 }, {89, 40 }, {109, 34 }, {89, 38 }, {77, 36 }, {110, 42 }};

    public static int[,] PokemonMansionB1FR = new int[,]
    {{109, 33 }, {109, 31 }, {58, 35 }, {77, 32 }, {109, 31 }, {110, 40 }, {77, 34 }, {88, 35 }, {110, 42 }, {89, 42 }};
    public static int[,] PokemonMansionB1FB = new int[,]
    {{88, 33 }, {88, 31 }, {37, 35 }, {77, 32 }, {88, 31 }, {89, 40 }, {77, 34 }, {109, 35 }, {126, 38 }, {110, 42 }};

    // 41 - Zubat, 42 - Golbat, 66 - Machop, 67 - Machoke, 74 - Geodude, 75 - Graveler, 95 - Onix, 105 - Marowak
    public static int[,] VictoryRoad1F = new int[,]
    {{66, 24 }, {74, 26 }, {41, 22 }, {95, 36 }, {95, 39 }, {95, 42 }, {75, 41 }, {42, 41 }, {67, 42 }, {105, 43 }};
    public static int[,] VictoryRoad2F = new int[,]
    {{66, 22 }, {74, 24 }, {41, 26 }, {95, 36 }, {95, 39 }, {95, 42 }, {67, 41 }, {42, 40 }, {105, 40 }, {75, 43 }};

    // 24 - Arbok, 26 - Raichu, 28 - Sandslash, 40 - Wigglytuff, 42 - Golbat, 
    // 47 - Parasect, 49 - Venomoth, 64 - Kadabra, 82 - Magneton, 85 - Dodrio, 97 - Hypno,
    // 101 - Electrode, 105 - Marowak, 112 - Rhydon, 113 - Chansey, 132 - Ditto
    public static int[,] CeruleanCave1FR = new int[,]
    {{42, 46 }, {97, 46 }, {82, 46 }, {85, 49 }, {49, 49 }, {24, 52 }, {64, 49 }, {47, 52 }, {26, 53 }, {132, 53 }};
    public static int[,] CeruleanCave1FB = new int[,]
    {{42, 46 }, {97, 46 }, {82, 46 }, {85, 49 }, {49, 49 }, {28, 52 }, {64, 49 }, {47, 52 }, {26, 53 }, {132, 53 }};

    public static int[,] CeruleanCave2F = new int[,]
    {{85, 51 }, {49, 51 }, {64, 51 }, {112, 52 }, {105, 52 }, {101, 52 },  {113, 56 }, {40, 54 }, {132, 55 }, {132, 60 }};

    public static int[,] CeruleanCaveB1FR = new int[,]
    {{112, 55 }, {105, 55 }, {101, 55 }, {113, 64 }, {47, 64 }, {26, 64 }, {24, 57 }, {132, 65 }, {132, 63 }, {132, 67 }};
    public static int[,] CeruleanCaveB1FB = new int[,]
    {{112, 55 }, {105, 55 }, {101, 55 }, {113, 64 }, {47, 64 }, {26, 64 }, {28, 57 }, {132, 65 }, {132, 63 }, {132, 67 }};

    #endregion

    // Returns the encounter array corresponding to the area's given index
    // isR21Grass is used solely to distinguish between Route 21's grass and water encounters
    public static int[,] GetEncountersFromIndex(int index, bool isR21Grass)
    {
        bool isRedVersion = PlayerData.Version == GameVersion.Red;

        switch (index)
        {
            default:
                return null;
            case 1:
                return Route1;
            case 2:
                return isRedVersion ? Route2R : Route2B;
            case 3:
                return Route3;
            case 4:
                return isRedVersion ? Route4R : Route4B;
            case 5:
                return isRedVersion ? Route5R : Route5B;
            case 6:
                return isRedVersion ? Route6R : Route6B;
            case 7:
                return isRedVersion ? Route7R : Route7B;
            case 8:
                return isRedVersion ? Route8R : Route8B;
            case 9:
                return isRedVersion ? Route9R : Route9B;
            case 10:
                return isRedVersion ? Route10R : Route10B;
            case 11:
                return isRedVersion ? Route11R : Route11B;
            case 12:
                return isRedVersion ? Route12R : Route12B;
            case 13:
                return isRedVersion ? Route13R : Route13B;
            case 14:
                return isRedVersion ? Route14R : Route14B;
            case 15:
                return isRedVersion ? Route15R : Route15B;
            case 16:
                return Route16;
            case 17:
                return Route17;
            case 18:
                return Route18;
            case 19:
            case 20:
            case 21:
                return isR21Grass ? Route21 : SeaRoutes19_20_21;
            case 22:
                return isRedVersion ? Rotue22R : Rotue22B;
            case 23:
                return isRedVersion ? Route23R : Route23B;
            case 24:
                return isRedVersion ? Route24R : Route24B;
            case 25:
                return isRedVersion ? Route25R : Route25B;
            case 26:
                return isRedVersion ? ViridianForestR : ViridianForestB;
            case 27:
                return MtMoon1F;
            case 28:
                return MtMoonB1F;
            case 29:
                return MtMoonB2F;
            case 30:
                return DiglettsCave;
            case 31:
                return RockTunnel1F;
            case 32:
                return RockTunnelB1F;
            case 33:
                return isRedVersion ? PowerPlantR : PowerPlantB;
            case 34:
                return PokemonTower3F;
            case 35:
                return PokemonTower4F;
            case 36:
                return PokemonTower5F;
            case 37:
                return PokemonTower6F;
            case 38:
                return PokemonTower7F;
            case 39:
                return isRedVersion ? SafariZoneCenterR : SafariZoneCenterB;
            case 40:
                return isRedVersion ? SafariZoneArea1R : SafariZoneArea1B;
            case 41:
                return isRedVersion ? SafariZoneArea2R : SafariZoneArea2B;
            case 42:
                return isRedVersion ? SafariZoneArea3R : SafariZoneArea3B;
            case 43:
                return isRedVersion ? SeafoamIslands1FR : SeafoamIslands1FB;
            case 44:
                return isRedVersion ? SeafoamIslandsB1FR : SeafoamIslandsB1FB;
            case 45:
                return isRedVersion ? SeafoamIslandsB2FR : SeafoamIslandsB2FB;
            case 46:
                return isRedVersion ? SeafoamIslandsB3FR : SeafoamIslandsB3FB;
            case 47:
                return isRedVersion ? SeafoamIslandsB4FR : SeafoamIslandsB4FB;
            case 48:
                return isRedVersion ? PokemonMansion1FR : PokemonMansion1FB;
            case 49:
                return isRedVersion ? PokemonMansion2FR : PokemonMansion2FB;
            case 50:
                return isRedVersion ? PokemonMansion3FR : PokemonMansion3FB;
            case 51:
                return isRedVersion ? PokemonMansionB1FR : PokemonMansionB1FB;
            case 52:
                return VictoryRoad1F;
            case 53:
                return VictoryRoad2F;
            case 54:
                return isRedVersion ? CeruleanCave1FR : CeruleanCave1FB;
            case 55:
                return CeruleanCave2F;
            case 56:
                return isRedVersion ? CeruleanCaveB1FR : CeruleanCaveB1FB;
        }
    }
}