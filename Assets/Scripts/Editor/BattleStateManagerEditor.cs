using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BattleStateManager))]
public class BattleStateManagerEditor : Editor
{
    bool showUIProperties = false;
    bool showPlayerUIProperties = false;
    bool showOpponentUIProperties = false;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<Player>k__BackingField"), new GUIContent("Player"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<Opponent>k__BackingField"), new GUIContent("Opponent"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<Tilemap>k__BackingField"), new GUIContent("Transition Tilemap"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<BattleFlash>k__BackingField"), new GUIContent("Transition Flash Image"));
        
        

        EditorGUILayout.Space();
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        showUIProperties = EditorGUILayout.Foldout(showUIProperties, "Battle UI");
        EditorStyles.foldout.fontStyle = FontStyle.Normal;
        if (showUIProperties)
        {
            ShowUIProperties();
        }

        EditorGUILayout.Space();
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Battle Info");
        EditorStyles.label.fontStyle = FontStyle.Normal;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<TransitionID>k__BackingField"), new GUIContent("Transition ID"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<BattleType>k__BackingField"), new GUIContent("Battle Type"));

        EditorGUILayout.Space();
        bool inBattle = (BattleType)serializedObject.FindProperty("<BattleType>k__BackingField").enumValueIndex != BattleType.None;

        if(inBattle)
        {
            ShowBattleInfo();
        }
        else
        {
            EditorStyles.label.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField("Not in battle...");
            EditorStyles.label.fontStyle = FontStyle.Normal;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowUIProperties()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<BattleUI>k__BackingField"), new GUIContent("UI Container"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<Background>k__BackingField"), new GUIContent("UI Background"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<SelectionBox>k__BackingField"), new GUIContent("Selection Box"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<SelectionArrow>k__BackingField"), new GUIContent("Selection Arrow"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<MoveInfoBox>k__BackingField"), new GUIContent("Move Info Box"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<MoveInfoText>k__BackingField"), new GUIContent("Move Info Textbox"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<MovesBox>k__BackingField"), new GUIContent("Moves Box"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<MoveArrow>k__BackingField"), new GUIContent("Move Selection Arrow"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<MoveSelection>k__BackingField"), new GUIContent("Move Selection Index"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<MoveNames>k__BackingField"), new GUIContent("Move Name Textboxes"), true);

        EditorGUILayout.Space();
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Team Pokeball Icons");
        EditorStyles.label.fontStyle = FontStyle.Normal;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("EmptyPokeball"), new GUIContent("Empty"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("NormalPokeball"), new GUIContent("Normal"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("StatusPokeball"), new GUIContent("Status Afflicted"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("FaintedPokeball"), new GUIContent("Fainted"));

        EditorGUILayout.Space();
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        showPlayerUIProperties = EditorGUILayout.Foldout(showPlayerUIProperties, "Player Side UI");
        EditorStyles.foldout.fontStyle = FontStyle.Normal;
        if (showPlayerUIProperties)
            ShowPlayerUIProperties();

        EditorGUILayout.Space();
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        showOpponentUIProperties = EditorGUILayout.Foldout(showOpponentUIProperties, "Opponent Side UI");
        EditorStyles.foldout.fontStyle = FontStyle.Normal;
        if (showOpponentUIProperties)
            ShowOpponentUIProperties();

    }

    private void ShowPlayerUIProperties()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerImage>k__BackingField"), new GUIContent("Image"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerIconsFrame>k__BackingField"), new GUIContent("Icons Frame"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerTeamIcons>k__BackingField"), new GUIContent("Team Icons"), true);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerInfoFrame>k__BackingField"), new GUIContent("Info Frame"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerInfoBackground>k__BackingField"), new GUIContent("Info Background"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerName>k__BackingField"), new GUIContent("Name Textbox"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerLevel>k__BackingField"), new GUIContent("Level Textbox"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerHPBar>k__BackingField"), new GUIContent("HP Bar"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerHPText>k__BackingField"), new GUIContent("HP Textbox"));
        EditorGUI.indentLevel--;
    }

    private void ShowOpponentUIProperties()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<OpponentImage>k__BackingField"), new GUIContent("Image"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<OpponentIconsFrame>k__BackingField"), new GUIContent("Icons Frame"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<OpponentTeamIcons>k__BackingField"), new GUIContent("Team Icons"), true);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<OpponentInfoFrame>k__BackingField"), new GUIContent("Info Frame"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<OpponentName>k__BackingField"), new GUIContent("Name Textbox"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<OpponentLevel>k__BackingField"), new GUIContent("Level Textbox"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<OpponentHPBar>k__BackingField"), new GUIContent("HP Bar"));
        EditorGUI.indentLevel--;
    }

    private void ShowBattleInfo()
    {
        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField("Turn Order:");
        if (serializedObject.FindProperty("<TurnOrderDecided>k__BackingField").boolValue)
        {
            bool playerFirst = serializedObject.FindProperty("<FirstSide>k__BackingField").FindPropertyRelative("IsPlayerSide").boolValue;
            if (playerFirst)
            {
                EditorGUILayout.LabelField("1. Player");
                EditorGUILayout.LabelField("2. Opponent");
            }
            else
            {
                EditorGUILayout.LabelField("1. Opponent");
                EditorGUILayout.LabelField("2. Player");
            }
        }
        else
        {
            EditorGUILayout.LabelField("Not Determined Yet");
        }
        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<Participants>k__BackingField"), new GUIContent("Participants"), true);

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<PlayerSide>k__BackingField"));

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("<OpponentSide>k__BackingField"));

        
    }
}