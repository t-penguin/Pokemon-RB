using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bide : MultiTurnAttackMove
{
    public Bide(BattleStateManager battle) : base("BIDE", Type.NORMAL, Category.Physical, 10, 0, 0, battle) { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        // First Turn
        if(TurnsLeft == 0)
        {
            user.IsBideActive = true;
            user.BideDamage = 0;
            SetMoveLock(user, true);
            yield return Battle.StartCoroutine(OnUsed(user));
            TurnsLeft = Random.Range(2, 4);
            yield break;
        }
        
        // Final Turn
        if(TurnsLeft == 1)
        {
            string name = user.TrainerIsPlayer ? user.Name : $"Enemy {user.Name}";
            yield return Battle.StartCoroutine(Battle.DisplayMessage($"{name}\nunleashed energy!", true));
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
        if (target.IsBideActive)
        {
            target.LastDamageRecieved = damage;
            target.BideDamage += 2 * damage;
        }
        yield return Battle.StartCoroutine(target.RecieveDamge(damage, Type.NONE));
        yield return new WaitForSeconds(60 / 60f);
    }
}