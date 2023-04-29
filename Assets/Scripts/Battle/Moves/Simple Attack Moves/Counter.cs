using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : SimpleAttackMove
{
    public Counter(BattleStateManager battle)
        : base (
            name: "COUNTER",
            type: Type.FIGHTING,
            category: Category.Physical,
            basePP: 20,
            accuracy: 100,
            power: 0,
            battle: battle,
            priority: -1 )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        opponent.LastDamageRecieved = 0;

        if (user == Battle.FirstSide.ActivePokemon || opponent.IsSemiInvulnerable || !AccuracyCheck(user, opponent))
            yield return Battle.StartCoroutine(OnMissed(user));
        else
        {
            BaseMove lastMove = opponent.LastMoveUsed;
            // Counters damaging moves, except for Counter itself
            bool validLastMove = lastMove.Category == Category.Physical
                                || lastMove.Category == Category.Special
                                || lastMove.GetType().BaseType != typeof(Counter);
            // Can only counter Normal or Fighting type moves
            bool validLastMoveType = lastMove.Type == Type.NORMAL || lastMove.Type == Type.FIGHTING;
            // Can only counter if damage was done to the user
            bool validLastDamage = user.LastDamageRecieved > 0;

            if(validLastMove && validLastMoveType && validLastDamage)
            {
                int damage = 2 * user.LastDamageRecieved;
                opponent.LastDamageRecieved = damage;

                yield return Battle.StartCoroutine(opponent.RecieveDamge(damage, Type));
                yield return new WaitForSeconds(60 / 60f);
            }
            else
                yield return Battle.StartCoroutine(OnMissed(user));
        }

        EndMove(user, opponent);
    }
}