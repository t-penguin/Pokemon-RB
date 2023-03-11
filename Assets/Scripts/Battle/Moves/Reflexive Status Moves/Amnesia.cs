using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amnesia : ReflexiveStatusMove
{
    public Amnesia(BattleStateManager battle) : base("AMNESIA", Type.PSYCHIC, 30, battle) { }

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