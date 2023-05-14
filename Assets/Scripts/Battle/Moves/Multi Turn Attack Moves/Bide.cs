using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bide : MultiTurnAttackMove
{
    public Bide(BattleStateManager battle)
        : base (
            name: "BIDE",
            type: Type.NORMAL, 
            category: Category.Physical, 
            basePP: 10,
            accuracy: 0,
            power: 0,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        // First Turn
        if(TurnsLeft == 0)
        {
            user.IsBideActive = true;
            user.BideDamage = 0;
            opponent.LastDamageRecieved = 0;
            SetMoveLock(user, true);
            yield return Battle.StartCoroutine(OnUsed(user));
            TurnsLeft = Random.Range(2, 4);
            yield break;
        }
        
        // Final Turn
        if(TurnsLeft == 1)
        {
            yield return Battle.StartCoroutine(OnUnleashedEnergy(user));
            yield return DealDamage(user, opponent);
            TurnsLeft = 0;
            user.IsBideActive = false;
            SetMoveLock(user, false);
            yield break;
        }

        TurnsLeft--;
    }

    protected override IEnumerator DealDamage(BattlePokemon user, BattlePokemon target)
    {
        int damage = user.BideDamage;
        target.LastDamageRecieved = damage;

        yield return Battle.StartCoroutine(target.RecieveDamge(damage, Type.NONE));
        yield return new WaitForSeconds(60 / 60f);
    }
}