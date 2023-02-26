using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trainer : MonoBehaviour
{
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public List<Pokemon> Team { get; protected set; }
    [field: SerializeField] public Bag Bag { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; protected set; }

    private void Awake()
    {
        if (Team == null)
            Team = new List<Pokemon>();

        if (Bag == null)
            Bag = new Bag();
    }

    /// <summary>
    /// Attemps to add the given Pokemon to this trainer's team. 
    /// Returns whether or not the attempt was successful.
    /// </summary>
    /// <param name="pokemon">The Pokemon to add to the team.</param>
    public bool AddToTeam(Pokemon pokemon)
    {
        if (Team.Count >= 6)
        {
            Debug.Log("Your Pokemon team is full!");
            return false;
        }

        Team.Add(pokemon);
        Debug.Log($"Added {pokemon.Nickname} to {Name}'s team at slot {Team.Count}.");
        return true;
    }

    public Pokemon GetFirstPokemon()
    {
        foreach(Pokemon pokemon in Team)
        {
            if (pokemon.CurrentHP > 0)
                return pokemon;
        }

        Debug.LogError("This trainer has no Pokemon available to battle.");
        return null;
    }

    public void Clear()
    {
        Name = "";
        Team.Clear();
        Bag.Clear();
    }

    public void SetTrainerName(string name) => Name = name;

    public void SetTrainerIcon(Sprite icon) => Icon = icon;
}

[System.Serializable]
public class Bag
{
    public List<Item> Items;
    public List<int> Quantities;
    public static int MaxItems = 20;

    public Bag()
    {
        Items = new List<Item>();
        Quantities = new List<int>();
    }

    public Bag(List<Item> items, List<int> quantities)
    {
        // Copy, at most, the first 20 items in the list
        Items = items.Count > 20 ? items.GetRange(0, 20) : items;
        // Quantities size should match the Items list
        Quantities = quantities.GetRange(0, Items.Count);
    }

    /// <summary> Attempts to add the given quantity of an item to the bag. Returns true if successful </summary>
    public bool Add(Item item, int quantity)
    {
        // There is no space for the given quantity of Item
        if (Items.Count >= MaxItems)
        {
            Debug.Log($"There is no space in the bag for {quantity} {item.name}(s).");
            return false;
        }
        
        quantity = item.isKeyItem ? 1 : Mathf.Clamp(quantity, 1, 99);

        // Check if there is a non-full stack of the Item already in the bag
        if (ContainsIncomplete(item, out int itemIndex))
        {
            // Only one of each Key Item can be present in the bag at a time
            if (item.isKeyItem)
            {
                Debug.Log($"Cannot have more than one {item.name} in the bag.");
                return false;
            }

            // Check if the additional quantity doesn't overflow the stack
            if (Quantities[itemIndex] + quantity <= 99)
            {
                Quantities[itemIndex] += quantity;
                Debug.Log($"Added {quantity} {item.name}(s) to the bag.");
                return true;
            }
            else if (Items.Count < MaxItems)
            {
                // Complete one stack of items and create a new one with the remainder
                int remainder = (quantity + Quantities[itemIndex]) % 99;
                Quantities[itemIndex] = 99;
                Items.Add(item);
                Quantities.Add(remainder);
                Debug.Log($"Added {quantity} {item.name}(s) to the bag.");
                return true;
            }
        }

        // Adds a new item stack to the bag if there is space and there wasn't a stack to add to already
        Items.Add(item);
        Quantities.Add(quantity);
        Debug.Log($"Added {quantity} {item.name}(s) to the bag.");
        return true;
    }

    public void Use(int index, bool inBattle)
    {
        
    }

    public void Clear()
    {
        Items.Clear();
        Quantities.Clear();
    }

    /// <summary>
    /// Returns true if the specified item is already in the bag as an incomplete stack.
    /// Outputs the index of the item as well. 
    /// </summary>
    private bool ContainsIncomplete(Item item, out int index)
    {
        index = -1;
        if (Items.Count == 0)
            return false;

        for (int i = 0; i < Items.Count; i++)
        {
            if (item.name == Items[i].name && Quantities[i] < 99)
            {
                index = i;
                return true;
            }
        }

        return false;
    }
}