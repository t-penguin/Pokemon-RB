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
            case 10: return new Bind(battle);
            case 11: return new Bite(battle);
            case 12: return new Blizzard(battle);
            case 13: return new BodySlam(battle);
            case 14: return new BoneClub(battle);
            case 15: return new Bonemerang(battle);
            case 16: return new Bubble(battle);
            case 17: return new Bubblebeam(battle);
            case 18: return new Clamp(battle);
            case 19: return new CometPunch(battle);
            case 20: return new ConfuseRay(battle);
            case 21: return new Confusion(battle);
            case 22: return new Constrict(battle);
            case 23: return new Conversion(battle);
            case 24: return new Counter(battle);
            case 25: return new Crabhammer(battle);
            case 26: return new Cut(battle);
            case 27: return new DefenseCurl(battle);
            case 28: return new Dig(battle);
                // DISABLE
            case 30: return new DizzyPunch(battle);
            case 31: return new DoubleKick(battle);
            case 32: return new DoubleSlap(battle);
            case 33: return new DoubleTeam(battle);
            case 34: return new DoubleEdge(battle);
            case 35: return new DragonRage(battle);
            case 36: return new DreamEater(battle);
            case 37: return new DrillPeck(battle);
            case 38: return new Earthquake(battle);
            case 39: return new EggBomb(battle);
            case 40: return new Ember(battle);
            case 41: return new Explosion(battle);
            case 42: return new FireBlast(battle);
            case 43: return new FirePunch(battle);
            case 44: return new FireSpin(battle);
            case 45: return new Fissure(battle);
            case 46: return new Flamethrower(battle);
            case 47: return new Flash(battle);
            case 48: return new Fly(battle);
            case 49: return new FocusEnergy(battle);
            case 50: return new FuryAttack(battle);
            case 51: return new FurySwipes(battle);
            case 52: return new Glare(battle);
            case 53: return new Growl(battle);
            case 54: return new Growth(battle);
            case 55: return new Guillotine(battle);
            case 56: return new Gust(battle);
            case 57: return new Harden(battle);
                // HAZE
            case 59: return new Headbutt(battle);
            case 60: return new HiJumpKick(battle);
            case 61: return new HornAttack(battle);
            case 62: return new HornDrill(battle);
            case 63: return new HydroPump(battle);
            case 64: return new HyperBeam(battle);
            case 65: return new HyperFang(battle);
            case 66: return new Hypnosis(battle);
            case 67: return new IceBeam(battle);
            case 68: return new IcePunch(battle);
            case 69: return new JumpKick(battle);
            case 70: return new KarateChop(battle);
            case 71: return new Kinesis(battle);
            case 72: return new LeechLife(battle);
            case 73: return new LeechSeed(battle);
            case 74: return new Leer(battle);
            case 75: return new Lick(battle);
            case 76: return new LightScreen(battle);
            case 77: return new LovelyKiss(battle);
            case 78: return new LowKick(battle);
            case 79: return new Meditate(battle);
            case 80: return new MegaDrain(battle);
            case 81: return new MegaKick(battle);
            case 82: return new MegaPunch(battle);
                // METRONOME
                // MIMIC
            case 85: return new Minimize(battle);
                // MIRROR MOVE
                // MIST
            case 88: return new NightShade(battle);
            case 89: return new PayDay(battle);
            case 90: return new Peck(battle);
            case 91: return new PetalDance(battle);
            case 92: return new PinMissile(battle);
            case 93: return new PoisonGas(battle);
            case 94: return new PoisonPowder(battle);
            case 95: return new PoisonSting(battle);
            case 96: return new Pound(battle);
            case 97: return new Psybeam(battle);
            case 98: return new Psychic(battle);
            case 99: return new Psywave(battle);
            case 100: return new QuickAttack(battle);
                // RAGE
            case 102: return new RazorLeaf(battle);
            case 103: return new RazorWind(battle);
            case 104: return new Recover(battle);
            case 105: return new Reflect(battle);
            case 106: return new Rest(battle);
                // ROAR
            case 108: return new RockSlide(battle);
            case 109: return new RockThrow(battle);
            case 110: return new RollingKick(battle);
            case 111: return new SandAttack(battle);
            case 112: return new Scratch(battle);
            case 113: return new Screech(battle);
            case 114: return new SeismicToss(battle);
            case 115: return new Selfdestruct(battle);
            case 116: return new Sharpen(battle);
            case 117: return new Sing(battle);
            case 118: return new SkullBash(battle);
            case 119: return new SkyAttack(battle);
            case 120: return new Slam(battle);
            case 121: return new Slash(battle);
            case 122: return new SleepPowder(battle);
            case 123: return new Sludge(battle);
            case 124: return new Smog(battle);
            case 125: return new SmokeScreen(battle);
            case 126: return new Softboiled(battle);
            case 127: return new SolarBeam(battle);
            case 128: return new SonicBoom(battle);
            case 129: return new SpikeCannon(battle);
            case 130: return new Splash(battle);
            case 131: return new Spore(battle);
            case 132: return new Stomp(battle);
            case 133: return new Strength(battle);
            case 134: return new StringShot(battle);
                // STRUGGLE
            case 136: return new StunSpore(battle);
            case 137: return new Submission(battle);
                // SUBSTITUTE
            case 139: return new SuperFang(battle);
            case 140: return new Supersonic(battle);
            case 141: return new Surf(battle);
            case 142: return new Swift(battle);
            case 143: return new SwordsDance(battle);
            case 144: return new Tackle(battle);
            case 145: return new TailWhip(battle);
            case 146: return new TakeDown(battle);
                // TELEPORT
            case 148: return new Thrash(battle);
            case 149: return new Thunder(battle);
            case 150: return new ThunderPunch(battle);
            case 151: return new ThunderShock(battle);
            case 152: return new ThunderWave(battle);
            case 153: return new Thunderbolt(battle);
            case 154: return new Toxic(battle);
                // TRANSFORM
            case 156: return new TriAttack(battle);
            case 157: return new Twineedle(battle);
            case 158: return new ViceGrip(battle);
            case 159: return new VineWhip(battle);
            case 160: return new WaterGun(battle);
            case 161: return new Waterfall(battle);
                // WHIRLWIND
            case 163: return new WingAttack(battle);
            case 164: return new Withdraw(battle);
            case 165: return new Wrap(battle);
        }
    }
}