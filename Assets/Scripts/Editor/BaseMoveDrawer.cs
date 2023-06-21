using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(BaseMove))]
public class BaseMoveDrawer : PropertyDrawer
{
    bool moveFoldout = false;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        BaseMove move = (BaseMove)property.managedReferenceValue;
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
                EditorGUI.indentLevel--;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 0;
    }
}