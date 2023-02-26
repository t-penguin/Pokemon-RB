using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownMap : Item
{
    public TownMap() : base("TOWN MAP", true) { }

    public override void OnUse(bool inBattle)
    {
        if (!inBattle)
        {
            /* The Town Map item should open up the Map screen
             * with the player's sprite at their current location.
             * A square reticle initially surrounds the player and
             * pressing up/down moves the reticle around the map in
             * a predetermined order. */
        }
    }
}
