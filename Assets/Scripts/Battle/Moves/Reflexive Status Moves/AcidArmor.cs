using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidArmor : ReflexiveStatusMove
{
    public AcidArmor(BattleStateManager battle) : base("ACID ARMOR", Type.POISON, 40, battle) { }

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