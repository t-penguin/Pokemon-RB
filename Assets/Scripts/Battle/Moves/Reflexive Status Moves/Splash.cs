using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : ReflexiveStatusMove
{
    private const string NO_EFFECT = "No effect!";

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
        yield return Battle.StartCoroutine(Battle.DisplayMessage(NO_EFFECT, false));
        yield return new WaitForSeconds(60 / 60f);

        SetLastMoveUsed(user);
        CurrentPP--;
    }
}