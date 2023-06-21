using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AttackMove))]
public class AttackMoveDrawer : PropertyDrawer
{
    bool moveFoldout = false;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        AttackMove move = (AttackMove)property.managedReferenceValue;
        if (move == null)
        {
            EditorGUILayout.LabelField("No Move");
        }
        else
        {
            moveFoldout = EditorGUILayout.Foldout(moveFoldout, $"Move: {move.Name}");
            if (moveFoldout)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField($"Type: {move.Type}");
                EditorGUILayout.LabelField($"Category: {move.Category}");
                EditorGUILayout.LabelField($"Power: {move.Power}");
                EditorGUILayout.LabelField($"Accuracy: {move.Accuracy}");
                EditorGUI.indentLevel--;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 0;
    }
}