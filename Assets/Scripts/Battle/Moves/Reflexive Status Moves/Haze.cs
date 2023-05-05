using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haze : ReflexiveStatusMove
{
    private const string STATUS_CHANGES_ELIMINATED = "All STATUS changes\nare eliminated!";

    public Haze(BattleStateManager battle)
        : base (
            name: "HAZE",
            type: Type.ICE,
            basePP: 30,
            battle: battle )
    { }

    public override IEnumerator Execute(BattlePokemon user, BattlePokemon opponent)
    {
        yield return Battle.StartCoroutine(OnUsed(user));

        // Resets the stat stages for both sides to 0
        user.StatModifiers.ResetStages();
        opponent.StatModifiers.ResetStages();

        /* Lifts the effects of Focus Energy/Dire Hit, Mist/Guard Spec., X Accuracy
         * Leech Seed, Disable, Reflect, and Light Screen from both sides */
        user.Focused = false;
        opponent.Focused = false;

        user.IsMistActive = false;
        opponent.IsMistActive = false;

        // ADD HERE: X ACCURACY 

        user.Seeded = false;
        opponent.Seeded = false;

        user.ClearDisable();
        opponent.ClearDisable();

        user.IsReflectActive = false;
        opponent.IsReflectActive = false;

        user.IsLightScreenActive = false;
        opponent.IsLightScreenActive = false;

        // Cures Confusion and turns bad poison into regular poison for both sides
        user.Confused = false;
        opponent.Confused = false;

        user.BadlyPoisoned = false;
        opponent.BadlyPoisoned = false;

        // Removes non-volatile status conditions from the opponent
        opponent.ClearNonVolatileStatus();

        yield return Battle.StartCoroutine(Battle.DisplayMessage(STATUS_CHANGES_ELIMINATED, true));
        yield return new WaitForSeconds(6 / 60f);

        user.SetLastMoveUsed(this);
        CurrentPP--;
    }
}