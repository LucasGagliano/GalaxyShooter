using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Variables
    [Header("UI References")]
    public Vector2 screenReferenceSize;
    public Vector2 screenOffset;
    public GameObject canvasReference;

    public bool hasLifeIco;
    public GameObject livesPositionReference;
    public Vector2 lifeIcoPosOffset;
    public Image lifeIco;

    public bool hasTxtScore;
    public Text txtScore;

    /*This variable(s) does not appear in the inspector*/ private Image[] _lifeIcos;
    /*This variable(s) does not appear in the inspector*/ public bool canDrawLives = true;
    /*This variable(s) does not appear in the inspector*/ public bool canLoadGameOver = true;
    #endregion

    #region Unity Methods
    private void Update() 
    {
        if(hasLifeIco)
            if (canDrawLives)
                DrawLives();

        GameOver();

        if(hasTxtScore)
            DrawScore();
    }
    #endregion

    #region Personalized Methods
    void DrawLives()
    {
        if (hasLifeIco) 
        {
            if (lifeIco != null)
            {
                _lifeIcos = new Image[PlayerPrefs.GetInt("_p_currentNumberOfLives")];

                for (int i = 0; i < livesPositionReference.transform.childCount; i++)
                {
                    Destroy(livesPositionReference.transform.GetChild(i).gameObject);
                }

                for (int i = 0; i < _lifeIcos.Length; i++)
                {
                _lifeIcos[i] = Instantiate(lifeIco, livesPositionReference.transform);
                _lifeIcos[i].transform.localPosition = new Vector3(livesPositionReference.transform.position.x + (i * lifeIcoPosOffset.x), livesPositionReference.transform.position.y + lifeIcoPosOffset.y, livesPositionReference.transform.position.z);
                }

                canDrawLives = false;
            }
            else
                Debug.LogWarning("Null Pointer Exception: you did not assign a value to 'lifeIco'");
        }
    }
    
    void DrawScore()
    {
        if (hasTxtScore)
        {
            if (txtScore != null)
                txtScore.text = PlayerPrefs.GetInt("_p_score").ToString();
            else
                Debug.LogWarning("Null Pointer Exception: you did not assign a value to 'txtScore'");
        }
    }
    
    void GameOver()
    {
        if(PlayerPrefs.GetInt("_p_currentNumberOfLives") == 0 && SceneManager.GetActiveScene().buildIndex != 0 && canLoadGameOver)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
            canLoadGameOver = false;
        }
    }
    #endregion
}
