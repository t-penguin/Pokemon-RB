using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : ReflexiveStatusMove
{
    public Splash(BattleStateManager battle)
        : base (
            name: "SPLASH",
            type: Type.NORMAL,
            basePP: 40,
            battle: battle)
    { }

    protected override IEnumerator Execute(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        yield return Battle.StartCoroutine(OnNoEffect());

        SetLastMoveUsed(user);
        CurrentPP--;
    }
}