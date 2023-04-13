using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agility : ReflexiveStatusMove
{
    public Agility(BattleStateManager battle)
        : base (
            name: "AGILITY",
            type: Type.PSYCHIC,
            basePP: 30,
            battle: battle )
    { }

    protected override IEnumerator Execute(BattlePokemon user)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        if (user.CanStatBeRaised(StatType.Speed))
        {
            user.ModifyStatAsPrimary(StatType.Speed, 2);
            yield return Battle.StartCoroutine(OnRaisedStat(user, StatType.Speed, true));
        }
        else
            yield return Battle.StartCoroutine(OnFailed());

        SetLastMoveUsed(user);
        CurrentPP--;
    }
}