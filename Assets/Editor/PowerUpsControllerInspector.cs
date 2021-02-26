using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PowerUpsController))]
[CanEditMultipleObjects]
public class PowerUpsControllerInspector : Editor
{
    #region Variables
    private SerializedProperty _powerUps;
    #endregion

    #region Unity Methods
    private void OnEnable() 
    {
        _powerUps = serializedObject.FindProperty("powerUps");
    }

    public override void OnInspectorGUI()
    {
        PowerUpsController powerUp = (PowerUpsController)target;

        EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Types of PowerUps", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_powerUps, true);
            
        EditorGUILayout.EndVertical();

        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        
        PrefabUtility.RecordPrefabInstancePropertyModifications(powerUp);
    }
    #endregion
}
