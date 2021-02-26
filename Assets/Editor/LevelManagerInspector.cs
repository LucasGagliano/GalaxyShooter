using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelManager))]
[CanEditMultipleObjects]
public class LevelManagerInspector : Editor
{
    #region Variables
    private SerializedProperty _dzlys;
    private bool isMenu, isLevel1;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        _dzlys = serializedObject.FindProperty("dzlys");
    }
    
    public override void OnInspectorGUI()
    {
        LevelManager level = (LevelManager)target;

        EditorGUILayout.BeginVertical();
        
            EditorGUILayout.LabelField("Scene Components", EditorStyles.boldLabel);

            isMenu = EditorGUILayout.Foldout(isMenu, "Menu");
            if(isMenu)
            {
                EditorGUILayout.Space();

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(_dzlys, true);
                EditorGUILayout.Space();
                    
                level.bg = (GameObject)EditorGUILayout.ObjectField("Background Reference", level.bg, typeof(GameObject), true);
            }

            isLevel1 = EditorGUILayout.Foldout(isLevel1, "Level 1");
            if(isLevel1)
            {
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Scene Spaces", EditorStyles.boldLabel);
                EditorGUILayout.Space();

                level.p_spawnPoint = EditorGUILayout.Vector2Field("Player Spawn Point", level.p_spawnPoint);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Scene Objects References", EditorStyles.boldLabel);
                EditorGUILayout.Space();

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(_dzlys, true);
                EditorGUILayout.Space();

                level.p = (PlayerBehaviour)EditorGUILayout.ObjectField("Player Reference", level.p, typeof(PlayerBehaviour), true);
                level.e = (EnemyBehaviour)EditorGUILayout.ObjectField("Enemy Reference", level.e, typeof(EnemyBehaviour), true);
                level.pU_controller = (PowerUpsController)EditorGUILayout.ObjectField("PowerUp Controller Reference", level.pU_controller, typeof(PowerUpsController), true);
                level.bg = (GameObject)EditorGUILayout.ObjectField("Background Reference", level.bg, typeof(GameObject), true);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Scene Objects Atributtes", EditorStyles.boldLabel);
                EditorGUILayout.Space();

                level.enemySpawnRatio = EditorGUILayout.FloatField("Enemy Spawn Ratio", level.enemySpawnRatio);
                level.playerSpawnRatio = EditorGUILayout.FloatField("Player Spawn Ratio", level.playerSpawnRatio);
                level.powerUpSpawnRatio = EditorGUILayout.FloatField("PowerUp Spawn Ratio", level.powerUpSpawnRatio);
                level.numberOfStartingLivesPlayer = EditorGUILayout.IntField("Number of Player's lives", level.numberOfStartingLivesPlayer);
                level.maxAmountOfEnemiesAtTheSameTime = EditorGUILayout.IntField("Max Amount of Enemies on Screen", level.maxAmountOfEnemiesAtTheSameTime);
                EditorGUILayout.Space();

                level.verticalScreenOffset = EditorGUILayout.FloatField("Screen Vertical Offset", level.verticalScreenOffset);
                level.horizontalScreenOffset = EditorGUILayout.FloatField("Screen Horizontal Offset", level.horizontalScreenOffset);
            }

        EditorGUILayout.EndVertical();
        
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();

        PrefabUtility.RecordPrefabInstancePropertyModifications(level);
    }
    #endregion
}
