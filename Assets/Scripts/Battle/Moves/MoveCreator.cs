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
                // RAGE
            case 102: return new RazorLeaf(battle);
                // RAZOR WIND
                // RECOVER
                // REFLECT
                // REST
                // ROAR
            case 108: return new RockSlide(battle);
            case 109: return new RockThrow(battle);
            case 110: return new RollingKick(battle);
            case 111: return new SandAttack(battle);
            case 112: return new Scratch(battle);
                // SCREECH
            case 114: return new SeismicToss(battle);
                // SELF-DESTRUCT
                // SHARPEN
                // SING
                // SKULL BASH
                // SKY ATTACK
            case 120: return new Slam(battle);
            case 121: return new Slash(battle);
                // SLEEP POWDER
            case 123: return new Sludge(battle);
            case 124: return new Smog(battle);
                // SMOKESCREEN
                // SOFT-BOILED
                // SOLAR BEAM
            case 128: return new SonicBoom(battle);
            case 129: return new SpikeCannon(battle);
                // SPLASH
                // SPORE
            case 132: return new Stomp(battle);
            case 133: return new Strength(battle);
                // STRING SHOT
                // STRUGGLE
                // STUN SPORE
            case 137: return new Submission(battle);
                // SUBSTITUTE
            case 139: return new SuperFang(battle);
                // SUPERSONIC
            case 141: return new Surf(battle);
            case 142: return new Swift(battle);
                // SWORDS DANCE
            case 144: return new Tackle(battle);
            case 145: return new TailWhip(battle);
            case 146: return new TakeDown(battle);
                // TELEPORT
                // THRASH
            case 149: return new Thunder(battle);
            case 150: return new ThunderPunch(battle);
            case 151: return new ThunderShock(battle);
                // THUNDER WAVE
            case 153: return new Thunderbolt(battle);
                // TOXIC
                // TRANSFORM
            case 156: return new TriAttack(battle);
            case 157: return new Twineedle(battle);
            case 158: return new ViceGrip(battle);
            case 159: return new VineWhip(battle);
            case 160: return new WaterGun(battle);
            case 161: return new Waterfall(battle);
                // WHIRLWIND
            case 163: return new WingAttack(battle);
                // WITHDRAW
                // WRAP
        }
    }
}