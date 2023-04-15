using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveCreator
{
    public static BaseMove CreateMove(BattleStateManager battle, int index)
    {
        switch(index)
        {
            default: return null;
            case 1: return new Absorb(battle);
            case 2: return new Acid(battle);
            case 3: return new AcidArmor(battle);
            case 4: return new Agility(battle);
            case 5: return new Amnesia(battle);
            case 6: return new AuroraBeam(battle);
            case 7: return new Barrage(battle);
            case 8: return new Barrier(battle);
            case 9: return new Bide(battle);
                // BIND
            case 11: return new Bite(battle);
            case 12: return new Blizzard(battle);
            case 13: return new BodySlam(battle);
            case 14: return new BoneClub(battle);
            case 15: return new Bonemerang(battle);
            case 16: return new Bubble(battle);
            case 17: return new Bubblebeam(battle);
                // CLAMP
            case 19: return new CometPunch(battle);
                // CONFUSE RAY
            case 21: return new Confusion(battle);
            case 22: return new Constrict(battle);
                // CONVERSION
                // COUNTER
            case 25: return new Crabhammer(battle);
            case 26: return new Cut(battle);
                // DEFENSE CURL
                // DIG
                // DISABLE
            case 30: return new DizzyPunch(battle);
            case 31: return new DoubleKick(battle);
            case 32: return new DoubleSlap(battle);
                // DOUBLE TEAM
            case 34: return new DoubleEdge(battle);
            case 35: return new DragonRage(battle);
            case 36: return new DreamEater(battle);
            case 37: return new DrillPeck(battle);
            case 38: return new Earthquake(battle);
            case 39: return new EggBomb(battle);
            case 40: return new Ember(battle);
                // EXPLOSION
            case 42: return new FireBlast(battle);
            case 43: return new FirePunch(battle);
                // FIRE SPIN
            case 45: return new Fissure(battle);
            case 46: return new Flamethrower(battle);
                // FLASH
            case 48: return new Fly(battle);
                // FOCUS ENERGY
            case 50: return new FuryAttack(battle);
            case 51: return new FurySwipes(battle);
                // GLARE
            case 53: return new Growl(battle);
                // GROWTH
            case 55: return new Guillotine(battle);
            case 56: return new Gust(battle);
                // HARDEN
                // HAZE
            case 59: return new Headbutt(battle);
            case 60: return new HiJumpKick(battle);
            case 61: return new HornAttack(battle);
            case 62: return new HornDrill(battle);
            case 63: return new HydroPump(battle);
                // HYPER BEAM
            case 65: return new HyperFang(battle);
                // HYPNOSIS
            case 67: return new IceBeam(battle);
            case 68: return new IcePunch(battle);
            case 69: return new JumpKick(battle);
            case 70: return new KarateChop(battle);
                // KENESIS
            case 72: return new LeechLife(battle);
                // LEECH SEED
                // LEER
            case 75: return new Lick(battle);
                // LIGHT SCREEN
                // LOVELY KISS
            case 78: return new LowKick(battle);
                // MEDITATE
            case 80: return new MegaDrain(battle);
            case 81: return new MegaKick(battle);
            case 82: return new MegaPunch(battle);
                // METRONOME
                // MIMIC
                // MINIMIZE
                // MIRROR MOVE
                // MIST
            case 88: return new NightShade(battle);
            case 89: return new PayDay(battle);
            case 90: return new Peck(battle);
                // PETAL DANCE
            case 92: return new PinMissile(battle);
                // POISON GAS
                // POISON POWDER
            case 95: return new PoisonSting(battle);
            case 96: return new Pound(battle);
            case 97: return new Psybeam(battle);
            case 98: return new Psychic(battle);
            case 99: return new Psywave(battle);
            case 100: return new QuickAttack(battle);

            case 111: return new SandAttack(battle);
            case 112: return new Scratch(battle);
            case 144: return new Tackle(battle);
            case 145: return new TailWhip(battle);
        }
    }
}