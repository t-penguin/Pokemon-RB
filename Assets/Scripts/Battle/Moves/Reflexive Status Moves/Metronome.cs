using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : ReflexiveStatusMove
{
    public Metronome(BattleStateManager battle)
        : base (
            name: "METRONOME",
            type: Type.NORMAL,
            basePP: 10,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));
        yield return new WaitForSeconds(6 / 60f);

        int moveIndex = 83;
        // Metronome cannot call itself (83) or Struggle (135)
        while (moveIndex == 83 || moveIndex == 135)
            moveIndex = Random.Range(1, 166);

        BaseMove Move = MoveCreator.CreateMove(Battle, moveIndex);

        yield return Battle.StartCoroutine(Move.Execute(user, opponent));

        CurrentPP--;
    }
}