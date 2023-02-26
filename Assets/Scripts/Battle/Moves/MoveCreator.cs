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
            case 53: return new Growl(battle);
            case 56: return new Gust(battle);
            case 144: return new Tackle(battle);
            case 112: return new Scratch(battle);
            case 145: return new TailWhip(battle);
        }
    }
}
