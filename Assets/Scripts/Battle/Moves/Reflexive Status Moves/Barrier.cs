using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ReflexiveStatusMove
{
    public Barrier(BattleStateManager battle) : base("BARRIER", Type.PSYCHIC, 30, battle) { }

    protected override IEnumerator Execute(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (user.CanStatBeRaised(StatType.Defense))
        {
            user.ModifyStatAsPrimary(StatType.Defense, 2);
            yield return Battle.StartCoroutine(OnRaisedStat(user, StatType.Defense, true));
        }
        else
            yield return Battle.StartCoroutine(OnFailed());

        SetLastMoveUsed(user);
        CurrentPP--;
    }
}