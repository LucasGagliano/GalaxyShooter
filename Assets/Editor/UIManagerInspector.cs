using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(UIManager))]
[CanEditMultipleObjects]
public class UIManagerInspector : Editor
{
    SerializedProperty _buttons;

    #region Unity Methods
    private void OnEnable() 
    {
        _buttons = serializedObject.FindProperty("buttons");
    }

    public override void OnInspectorGUI()
    {
        UIManager UIInstance = (UIManager)target;

        EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("UI Manager Variables", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("UI Manager References", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            UIInstance.screenReferenceSize = EditorGUILayout.Vector2Field("Reference Size", UIInstance.screenReferenceSize);
            UIInstance.screenOffset = EditorGUILayout.Vector2Field("Screen Offset", UIInstance.screenOffset);
            EditorGUILayout.Space();

            EditorGUILayout.Space();
            UIInstance.hasLifeIco = EditorGUILayout.Toggle("Has Life Icon", UIInstance.hasLifeIco);
            if (UIInstance.hasLifeIco)
            {
                UIInstance.livesPositionReference = (GameObject)EditorGUILayout.ObjectField("Reference", UIInstance.livesPositionReference, typeof(GameObject), true);
                UIInstance.lifeIcoPosOffset = EditorGUILayout.Vector2Field("Offset", UIInstance.lifeIcoPosOffset);
                EditorGUILayout.Space();

                UIInstance.lifeIco = (Image)EditorGUILayout.ObjectField("Reference", UIInstance.lifeIco, typeof(Image), true);          
                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();
            UIInstance.hasTxtScore = EditorGUILayout.Toggle("Has Score", UIInstance.hasTxtScore);
            if (UIInstance.hasTxtScore)
            {
                UIInstance.txtScore = (Text)EditorGUILayout.ObjectField("Reference", UIInstance.txtScore, typeof(Text), true);
                EditorGUILayout.Space();
            }

        EditorGUILayout.EndVertical();

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            Undo.RecordObject(target, "Modifications in the object " + target.name);
        }
    }
    #endregion
}
