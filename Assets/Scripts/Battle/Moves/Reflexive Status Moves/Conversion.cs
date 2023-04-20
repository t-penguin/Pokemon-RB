using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversion : ReflexiveStatusMove
{
    public Conversion(BattleStateManager battle)
        : base (
            name: "CONVERSION",
            type: Type.NORMAL,
            basePP: 30,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        user.Primary = opponent.Primary;
        user.Secondary = opponent.Secondary;

        string targetName = opponent == Battle.PlayerSide.ActivePokemon ? opponent.Name : $"Enemy {opponent.Name}";
        yield return Battle.StartCoroutine(Battle.DisplayMessage($"Converted type to\n{targetName}<!", true));
        yield return new WaitForSeconds(6 / 60f);

        SetLastMoveUsed(user);
        CurrentPP--;
    }
}