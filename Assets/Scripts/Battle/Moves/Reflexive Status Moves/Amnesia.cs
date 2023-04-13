using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amnesia : ReflexiveStatusMove
{
    public Amnesia(BattleStateManager battle)
        : base (
            name: "AMNESIA",
            type: Type.PSYCHIC,
            basePP: 30, 
            battle: battle )
    { }

    protected override IEnumerator Execute(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (user.CanStatBeRaised(StatType.Special))
        {
            user.ModifyStatAsPrimary(StatType.Special, 2);
            yield return Battle.StartCoroutine(OnRaisedStat(user, StatType.Special, true));
        }
        else
            yield return Battle.StartCoroutine(OnFailed());

        SetLastMoveUsed(user);
        CurrentPP--;
    }
}