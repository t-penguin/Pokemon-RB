using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public bool isKeyItem;

    public Item(string itemName, bool keyItem)
    {
        name = itemName;
        isKeyItem = keyItem;
    }

    public virtual void OnUse(bool inBattle)
    {
        /* Opens up the text box saying:
         * "OAK: {PlayerData.PlayerName}!\n
         * This isn't the\n         <-- Wait for input
         * time to use that!"       <-- Wait for input
         * 
         * Closes the text box and item options and returns to the item menu */
    }
}