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

            case 11: return new Bite(battle);
            case 12: return new Blizzard(battle);
            case 13: return new BodySlam(battle);
            case 14: return new BoneClub(battle);
            case 15: return new Bonemerang(battle);
            case 16: return new Bubble(battle);
            case 17: return new Bubblebeam(battle);

            case 19: return new CometPunch(battle);

            case 21: return new Confusion(battle);
            case 22: return new Constrict(battle);


            case 25: return new Crabhammer(battle);
            case 26: return new Cut(battle);



            case 30: return new DizzyPunch(battle);
            case 31: return new DoubleKick(battle);
            case 32: return new DoubleSlap(battle);

            case 34: return new DoubleEdge(battle);
            case 35: return new DragonRage(battle);
            case 36: return new DreamEater(battle);
            case 37: return new DrillPeck(battle);
            case 38: return new Earthquake(battle);
            case 39: return new EggBomb(battle);
            case 40: return new Ember(battle);

            case 42: return new FireBlast(battle);
            case 43: return new FirePunch(battle);

            case 45: return new Fissure(battle);
            case 46: return new Flamethrower(battle);

            case 48: return new Fly(battle);

            case 50: return new FuryAttack(battle);
            case 51: return new FurySwipes(battle);

            case 53: return new Growl(battle);

            case 55: return new Guillotine(battle);
            case 56: return new Gust(battle);

            case 72: return new LeechLife(battle);
            case 80: return new MegaDrain(battle);
            case 111: return new SandAttack(battle);
            case 112: return new Scratch(battle);
            case 144: return new Tackle(battle);
            case 145: return new TailWhip(battle);
        }
    }
}