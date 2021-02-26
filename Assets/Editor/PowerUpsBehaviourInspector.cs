using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PowerUpsBehaviour))]
[CanEditMultipleObjects]
public class PowerUpsBehaviourInspector : Editor
{
    #region Unity Methods
    public override void OnInspectorGUI()
    {
        PowerUpsBehaviour powerUp = (PowerUpsBehaviour)target;

        EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("PowerUp Variables", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("PowerUp Atributtes", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            powerUp.verticalMovementForce = EditorGUILayout.FloatField("Vertical Movement Force", powerUp.verticalMovementForce);
            
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
        PrefabUtility.RecordPrefabInstancePropertyModifications(powerUp);
    }
    #endregion
}
