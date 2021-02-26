using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerBehaviour))]
[CanEditMultipleObjects]
public class PlayerBehaviourInspector : Editor
{
    #region Variables
    private Object _shieldObj, _leftEngine, _rightEngine, _laserBullet;
    private SerializedProperty _frontGuns, _backGuns;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        _frontGuns = serializedObject.FindProperty("frontGuns");
        _backGuns = serializedObject.FindProperty("backGuns");
    }
    
    public override void OnInspectorGUI()
    {
        PlayerBehaviour player = (PlayerBehaviour)target;

        EditorGUILayout.BeginVertical();

            serializedObject.Update();
            EditorGUILayout.LabelField("Player Components", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Player Attributes", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            player.verticalMovimentForce = EditorGUILayout.FloatField("Vertical Moviment Force", player.verticalMovimentForce);
            player.shootRatio = EditorGUILayout.FloatField("Shoot Ratio", player.shootRatio);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Player Animations", EditorStyles.boldLabel);
            player.dyingAnimationSpeed = EditorGUILayout.FloatField("Dying Animation Speed", player.dyingAnimationSpeed);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Player Controlled Objects", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_frontGuns, true);
            EditorGUILayout.PropertyField(_backGuns, true);
            EditorGUILayout.Space();

            player.shieldObj = (GameObject)EditorGUILayout.ObjectField("Shield", player.shieldObj, typeof(GameObject), true);
            player.leftEngine = (GameObject)EditorGUILayout.ObjectField("Left Engine", player.leftEngine, typeof(GameObject), true);
            player.rightEngine = (GameObject)EditorGUILayout.ObjectField("Right Engine", player.rightEngine, typeof(GameObject), true);
            player.laserBullet = (BulletBehaviour)EditorGUILayout.ObjectField("Laser Bullet", player.laserBullet, typeof(BulletBehaviour), false);
            
        EditorGUILayout.EndVertical();

        if(EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        
        PrefabUtility.RecordPrefabInstancePropertyModifications(player);
    }
    #endregion
}

