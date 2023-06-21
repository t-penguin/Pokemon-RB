using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BattleSide))]
public class BattleSideDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField(property.displayName);
        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUI.indentLevel++;

        EditorGUILayout.PropertyField(property.FindPropertyRelative("Action"));
        EditorGUILayout.LabelField($"Locked Into Action: {property.FindPropertyRelative("LockedIntoAction").boolValue}");

        switch ((BattleAction)property.FindPropertyRelative("Action").enumValueIndex)
        {
            case BattleAction.UseMove:
                EditorGUILayout.PropertyField(property.FindPropertyRelative("Move"));
                EditorGUILayout.LabelField($"Locked Into Move: {property.FindPropertyRelative("LockedIntoMove").boolValue}");
                break;
            case BattleAction.UseItem:
                EditorGUILayout.PropertyField(property.FindPropertyRelative("Item"));
                break;
            case BattleAction.SwitchPokemon:
                EditorGUILayout.PropertyField(property.FindPropertyRelative("SwitchTarget"), new GUIContent("Switch Target Pokemon"));
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(property.FindPropertyRelative("Team"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("Bag"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("ActivePokemon"));

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 0;
    }
}