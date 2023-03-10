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
            case 36: return new DreamEater(battle);
            case 48: return new Fly(battle);
            case 50: return new FuryAttack(battle);
            case 53: return new Growl(battle);
            case 56: return new Gust(battle);
            case 72: return new LeechLife(battle);
            case 80: return new MegaDrain(battle);
            case 144: return new Tackle(battle);
            case 111: return new SandAttack(battle);
            case 112: return new Scratch(battle);
            case 145: return new TailWhip(battle);
        }
    }
}
