using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyBehaviour))]
[CanEditMultipleObjects]
public class EnemyBehaviourInspector : Editor
{
    #region Variables
    SerializedProperty _frontGuns;
    #endregion

    #region Unity Methods
    private void OnEnable() 
    {
        _frontGuns = serializedObject.FindProperty("frontGuns");
    }
    
    public override void OnInspectorGUI()
    {
        EnemyBehaviour enemy = (EnemyBehaviour)target;

        EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Enemy Variables", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Enemy Atributtes", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            enemy.verticalMovimentForce = EditorGUILayout.FloatField("Vertical Moviment Force", enemy.verticalMovimentForce);
            enemy.shootRatio = EditorGUILayout.FloatField("Shoot Ratio", enemy.shootRatio);
            enemy.viewField = EditorGUILayout.FloatField("View Field", enemy.viewField);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Enemy Animations", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            enemy.dyingAnimationSpeed = EditorGUILayout.FloatField("Dying Animation Speed", enemy.dyingAnimationSpeed);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Enemy Controlled Objects", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_frontGuns, true);
            EditorGUILayout.Space();

            enemy.laserBullet = (BulletBehaviour)EditorGUILayout.ObjectField("Laser Bullet Reference", enemy.laserBullet, typeof(BulletBehaviour), true);
            enemy.eyes = (GameObject)EditorGUILayout.ObjectField("Enemy Eyes Reference", enemy.eyes, typeof(GameObject), true);

        EditorGUILayout.EndVertical();

        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        
        PrefabUtility.RecordPrefabInstancePropertyModifications(enemy);
    }
    #endregion
}
