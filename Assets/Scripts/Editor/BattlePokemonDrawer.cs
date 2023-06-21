using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BattlePokemon))]
public class BattlePokemonDrawer : PropertyDrawer
{
    bool movesFoldout = false;
    bool statsFoldout = false;
    bool battleFoldout = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField(property.displayName);
        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUI.indentLevel++;

        int dexNum = property.FindPropertyRelative("<PokedexNumber>k__BackingField").intValue;
        if (dexNum > 0 && dexNum < MoveData.Names.Length)
        {
            DisplayPokemonInfo(property);
        }
        else
        {
            EditorGUILayout.LabelField("No Battle Pokemon");
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

    private void DisplayPokemonInfo(SerializedProperty property)
    {
        EditorGUILayout.LabelField($"Species: {PokemonData.Names[property.FindPropertyRelative("<PokedexNumber>k__BackingField").intValue]}");
        EditorGUILayout.LabelField($"Nickname: {property.FindPropertyRelative("<Name>k__BackingField").stringValue}");

        Type primary = (Type)property.FindPropertyRelative("<Primary>k__BackingField").enumValueIndex;
        Type secondary = (Type)property.FindPropertyRelative("<Secondary>k__BackingField").enumValueIndex;
        if (secondary == Type.NONE)
            EditorGUILayout.LabelField($"Type: {primary}");
        else
            EditorGUILayout.LabelField($"Type: {primary}/{secondary}");

        EditorGUILayout.LabelField($"Status: {(StatusEffect)property.FindPropertyRelative("<Status>k__BackingField").enumValueIndex}");

        /* BaseMove Array displays weird because BaseMove has a custom property drawer
         * This just displays it as a foldout with 4 elements */
        SerializedProperty movesArray = property.FindPropertyRelative("<Moves>k__BackingField");
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        movesFoldout = EditorGUILayout.Foldout(movesFoldout, "Known Moves");
        EditorStyles.foldout.fontStyle = FontStyle.Normal;

        if (movesFoldout)
            DisplayMovesInfo(movesArray);

        EditorGUILayout.Space();
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        statsFoldout = EditorGUILayout.Foldout(statsFoldout, "Stat Info");
        EditorStyles.foldout.fontStyle = FontStyle.Normal;

        if (statsFoldout)
            DisplayStatsInfo(property);

        EditorGUILayout.Space();
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        battleFoldout = EditorGUILayout.Foldout(battleFoldout, "Battle Info");
        EditorStyles.foldout.fontStyle = FontStyle.Normal;

        if (battleFoldout)
            DisplayBattleInfo(property, movesArray);
    }

    private void DisplayMovesInfo(SerializedProperty movesArray)
    {
        EditorGUI.indentLevel++;
        if (movesArray.arraySize > 0)
        {
            for (int i = 0; i < 4; i++)
                EditorGUILayout.PropertyField(movesArray.GetArrayElementAtIndex(i), true);
        }
        else
        {
            EditorGUILayout.LabelField("Array not set");
        }
        EditorGUI.indentLevel--;
    }

    private void DisplayStatsInfo(SerializedProperty property)
    {
        EditorGUILayout.LabelField($"Level: {property.FindPropertyRelative("<Level>k__BackingField").intValue}");

        SerializedProperty stats = property.FindPropertyRelative("<Stats>k__BackingField");
        SerializedProperty modifiers = property.FindPropertyRelative("<StatModifiers>k__BackingField");
        SerializedProperty battleStats = property.FindPropertyRelative("<BattleStats>k__BackingField");

        stats.Next(true);
        battleStats.Next(true);
        int totalHP = stats.intValue;
        EditorGUILayout.LabelField($"HP: {property.FindPropertyRelative("<CurrentHP>k__BackingField").intValue} / {totalHP}");

        EditorGUILayout.Space();
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Stat / Modifier / In Battle Stat");
        EditorStyles.label.fontStyle = FontStyle.Normal;
        stats.Next(false);
        modifiers.Next(true);
        battleStats.Next(false);
        EditorGUILayout.LabelField($"Attack: {stats.intValue} / {modifiers.intValue} / {battleStats.intValue}");

        stats.Next(false);
        modifiers.Next(false);
        battleStats.Next(false);
        EditorGUILayout.LabelField($"Defense: {stats.intValue} / {modifiers.intValue} / {battleStats.intValue}");

        stats.Next(false);
        modifiers.Next(false);
        battleStats.Next(false);
        EditorGUILayout.LabelField($"Special: {stats.intValue} / {modifiers.intValue} / {battleStats.intValue}");

        stats.Next(false);
        modifiers.Next(false);
        battleStats.Next(false);
        EditorGUILayout.LabelField($"Speed: {stats.intValue} / {modifiers.intValue} / {battleStats.intValue}");

        EditorGUILayout.Space();
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Multiplier (x100) / Modifier Stage");
        EditorStyles.label.fontStyle = FontStyle.Normal;

        int accuracy = (int)(property.FindPropertyRelative("<Accuracy>k__BackingField").floatValue * 100);
        modifiers.Next(false);
        EditorGUILayout.LabelField($"Accuracy: {accuracy} / {modifiers.intValue}");

        int evasion = (int)(property.FindPropertyRelative("<Evasion>k__BackingField").floatValue * 100);
        modifiers.Next(false);
        EditorGUILayout.LabelField($"Evasion: {evasion} / {modifiers.intValue}");
    }

    private void DisplayBattleInfo(SerializedProperty property, SerializedProperty movesArray)
    {
        EditorGUILayout.LabelField($"Semi-Invulnerable: {property.FindPropertyRelative("<IsSemiInvulnerable>k__BackingField").boolValue}");
        EditorGUILayout.LabelField($"Last Damage Recieved: {property.FindPropertyRelative("<LastDamageRecieved>k__BackingField").intValue}");

        if (property.FindPropertyRelative("<IsBideActive>k__BackingField").boolValue)
            EditorGUILayout.LabelField($"Bide: Active / Damage Stored: {property.FindPropertyRelative("<BideDamage>k__BackingField").intValue}");
        else
            EditorGUILayout.LabelField("Bide: Not Active");

        if (property.FindPropertyRelative("<HasSubstitute>k__BackingField").boolValue)
            EditorGUILayout.LabelField($"Substitute: Active / HP: {property.FindPropertyRelative("<SubsituteHP>k__BackingField").intValue}");
        else
            EditorGUILayout.LabelField("Substitute: Not Active");

        EditorGUILayout.LabelField($"Mist: {(property.FindPropertyRelative("<IsMistActive>k__BackingField").boolValue ? "Active" : "Not Active")}");
        EditorGUILayout.LabelField($"Reflect: {(property.FindPropertyRelative("<IsReflectActive>k__BackingField").boolValue ? "Active" : "Not Active")}");
        EditorGUILayout.LabelField($"Light Screen: {(property.FindPropertyRelative("<IsLightScreenActive>k__BackingField").boolValue ? "Active" : "Not Active")}");

        if (property.FindPropertyRelative("<BadlyPoisoned>k__BackingField").boolValue)
            EditorGUILayout.LabelField($"Badly Poisioned: True / Counter: {property.FindPropertyRelative("<ToxicCounter>k__BackingField").intValue}");
        else
            EditorGUILayout.LabelField("Badly Poisoned: False");

        if ((StatusEffect)property.FindPropertyRelative("<Status>k__BackingField").enumValueIndex == StatusEffect.SLP)
            EditorGUILayout.LabelField($"Asleep: True / Counter: {property.FindPropertyRelative("<SleepCounter>k__BackingField").intValue}");
        else
            EditorGUILayout.LabelField("Asleep: False");

        EditorGUILayout.Space();
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Last Move Used");
        EditorStyles.label.fontStyle = FontStyle.Normal;
        EditorGUILayout.PropertyField(property.FindPropertyRelative("<LastMoveUsed>k__BackingField"));

        EditorGUILayout.Space();
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Mirror Move Target");
        EditorStyles.label.fontStyle = FontStyle.Normal;
        EditorGUILayout.PropertyField(property.FindPropertyRelative("<MirrorMove>k__BackingField"));


        EditorGUILayout.Space();
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Volatile Status Conditions");
        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUILayout.LabelField($"Flinched: {property.FindPropertyRelative("<Flinched>k__BackingField").boolValue}");
        EditorGUILayout.LabelField($"Recharging: {property.FindPropertyRelative("<Recharging>k__BackingField").boolValue}");
        EditorGUILayout.LabelField($"Bound: {property.FindPropertyRelative("<Bound>k__BackingField").boolValue}");
        EditorGUILayout.LabelField($"Seeded: {property.FindPropertyRelative("<Seeded>k__BackingField").boolValue}");

        if (property.FindPropertyRelative("<Confused>k__BackingField").boolValue)
            EditorGUILayout.LabelField($"Confused: True / Turns Left: {property.FindPropertyRelative("<ConfusionTimer>k__BackingField")}");
        else
            EditorGUILayout.LabelField($"Confused: False");

        if (property.FindPropertyRelative("<Disabled>k__BackingField").boolValue)
        {
            int turnsLeft = property.FindPropertyRelative("<DisableCounter>k__BackingField").intValue;
            int index = property.FindPropertyRelative("<DisableIndex>k__BackingField").intValue;
            EditorGUILayout.LabelField($"Disabled: True / Turns Left: {turnsLeft}");
            EditorGUILayout.PropertyField(movesArray.GetArrayElementAtIndex(index), new GUIContent("Disabled Move"));
        }
        else
            EditorGUILayout.LabelField("Disabled: False");
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 0;
    }
}