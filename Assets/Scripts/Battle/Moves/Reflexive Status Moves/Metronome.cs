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

        int metronome_index = 83;
        int struggle_index = 135;
        int moveIndex = metronome_index;

        // Metronome cannot call itself (83) or Struggle (135)
        while (moveIndex == metronome_index || moveIndex == struggle_index)
            moveIndex = Random.Range(1, MoveCreator.TotalMoves + 1);

        BaseMove Move = MoveCreator.CreateMove(Battle, moveIndex);

        yield return Battle.StartCoroutine(Move.Execute(user, opponent));

        CurrentPP--;
    }
}