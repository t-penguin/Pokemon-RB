using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMove : TransitiveStatusMove
{
    public MirrorMove(BattleStateManager battle)
        : base (
            name: "MIRROR MOVE",
            type: Type.FLYING,
            basePP: 20,
            accuracy: 0,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        BaseMove Move = user.MirrorMove;
        if (Move == null)
            yield return Battle.StartCoroutine(OnFailed());
        else
            yield return Battle.StartCoroutine(Move.Execute(user, opponent));

        opponent.ClearMirrorMove();
        CurrentPP--;
    }
}