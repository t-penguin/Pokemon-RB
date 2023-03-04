using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Trainer
{
    private void Start()
    {
        Name = PlayerData.Name;

        AddToTeam(new Pokemon(1, 5));
        AddToTeam(new Pokemon(4, 5));
        AddToTeam(new Pokemon(7, 5));

        Team[0].TeachMove(50, 2);
        Team[0].TeachMove(48, 3);
        Team[2].TeachMove(141, 2);

        Bag.Add(new Pokeball(), 10);
        Bag.Add(new Pokeball(), 100);
        Bag.Add(new Pokeball(), -1);
        Bag.Add(new TownMap(), 2);
        Bag.Add(new TownMap(), 1);
    }

    #region Team Methods

    public void SwitchPokemon(int first, int second)
    {
        Pokemon temp = Team[first];
        Team[first] = Team[second];
        Team[second] = temp;
    }

    #endregion
}