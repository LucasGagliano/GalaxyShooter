using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BulletBehaviour))]
[CanEditMultipleObjects]
public class BulletBehaviourInspector : Editor
{
    #region Unity Methods
    public override void OnInspectorGUI()
    {
        BulletBehaviour bullet = (BulletBehaviour)target;

        EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Bullet Components", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Bullet Atributtes", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            bullet.horizontalMovimentForce = EditorGUILayout.FloatField("Horizontal Moviment Force", bullet.horizontalMovimentForce);

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
        PrefabUtility.RecordPrefabInstancePropertyModifications(bullet);
    }
    #endregion
}
