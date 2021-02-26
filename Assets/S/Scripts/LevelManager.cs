using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Variables
    [Header("Scene Spaces")]
    public Vector2 p_spawnPoint;

    [Header("Scene Objects")]
    public PlayerBehaviour p;
    public EnemyBehaviour e;
    public PowerUpsController pU_controller;
    public GameObject bg;
    public GameObject[] dzlys;

    [Header("Scene Objects Properties")]
    public float enemySpawnRatio, playerSpawnRatio, powerUpSpawnRatio, verticalScreenOffset, horizontalScreenOffset;
    public int numberOfStartingLivesPlayer, maxAmountOfEnemiesAtTheSameTime;

    /*This variable(s) does not appear in the inspector*/ private Vector2 _enemySpawnPoint;
    /*This variable(s) does not appear in the inspector*/ private Vector2 _powerUpSpawnPoint;

    /*This variable(s) does not appear in the inspector*/ private PlayerBehaviour _playerInstance;
    /*This variable(s) does not appear in the inspector*/ private EnemyBehaviour _enemyInstance;
    /*This variable(s) does not appear in the inspector*/ private PowerUpsBehaviour _powerUpInstance;
    /*This variable(s) does not appear in the inspector*/ private GameObject _backgroundInstance;


    /*This variable(s) does not appear in the inspector*/ private bool _canStartCanSpawnPowerUp = true;
    /*This variable(s) does not appear in the inspector*/ private bool _canStartCanSpawnEnemy = true;
    /*This variable(s) does not appear in the inspector*/ private bool _canStartCanSpawnPlayer = true;
    /*This variable(s) does not appear in the inspector*/ private bool _canSpawnEnemy, _canSpawnPowerUp;

    /*This variable(s) does not appear in the inspector*/ public int numberOfEnemiesOnScreen;
    /*This variable(s) does not appear in the inspector*/ public bool isCutSceneHappening;
    #endregion

    #region Unity Methods
    private void Start()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                StartMenu();
                break;
                
            case 1:
                StartLevel1();
                break;
        }
    }

    private void Update()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                if (!isCutSceneHappening)
                {
                    if (_playerInstance != null)
                    {
                        Bounds();
                        SpawnEnemy();
                        SpawnPowerUp();
                    }
                    else if (PlayerPrefs.GetInt("_p_currentNumberOfLives") > 0 && PlayerPrefs.GetInt("_p_alive") == 1)
                        SpawnPlayer();
                    else if (_canStartCanSpawnPlayer)
                        StartCoroutine(CanSpawnPlayer());
                }
            break;
        }
    }
    #endregion

    #region Personalized Methods
    void StartMenu()
    {
        SpawnBackground();
    }
    
    void StartLevel1()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("_p_score", 0);
        PlayerPrefs.SetInt("_p_startingNumberOfLives", numberOfStartingLivesPlayer);
        PlayerPrefs.SetInt("_p_currentNumberOfLives", PlayerPrefs.GetInt("_p_startingNumberOfLives"));

        SpawnBackground();
    }

    void SpawnPlayer()
    {
        _playerInstance = Instantiate(p, dzlys[1].transform);
        _playerInstance.transform.position = new Vector3(p_spawnPoint.x, p_spawnPoint.y, dzlys[1].transform.parent.position.z);
        _playerInstance.transform.localPosition = new Vector3(_playerInstance.transform.localPosition.x, _playerInstance.transform.localPosition.y, 1);

        _playerInstance.damage = false;
    }

    void SpawnEnemy()
    {
        if (numberOfEnemiesOnScreen < maxAmountOfEnemiesAtTheSameTime)
        {
            if (_canSpawnEnemy)
            {
                numberOfEnemiesOnScreen += 1;
                _canSpawnEnemy = false;

                _enemySpawnPoint = new Vector2(Random.Range(0.0f, 1.0f), 1);
                _enemySpawnPoint = new Vector2(Camera.main.ViewportToWorldPoint(_enemySpawnPoint).x, Camera.main.ViewportToWorldPoint(_enemySpawnPoint).y);

                _enemyInstance = Instantiate(e, dzlys[1].transform);
                _enemyInstance.transform.position = new Vector3(_enemySpawnPoint.x, _enemySpawnPoint.y, dzlys[1].transform.position.z);
                _enemyInstance.transform.localPosition = new Vector3(_enemyInstance.transform.localPosition.x, _enemyInstance.transform.localPosition.y, 1);
            }
            else
                if (_canStartCanSpawnEnemy)
            {
                _canStartCanSpawnEnemy = false;
                StartCoroutine(CanSpawnEnemy());
            }
        }
    }

    void SpawnPowerUp()
    {
        if (_canSpawnPowerUp)
        {
            if (!_playerInstance.affectedByPowerUp)
            {
                _canSpawnPowerUp = false;

                _powerUpSpawnPoint = new Vector2(Random.Range(0.0f, 1.0f), 1);
                _powerUpSpawnPoint = new Vector2(Camera.main.ViewportToWorldPoint(_powerUpSpawnPoint).x, Camera.main.ViewportToWorldPoint(_powerUpSpawnPoint).y);

                _powerUpInstance = Instantiate(pU_controller.powerUps[Random.Range(0, pU_controller.powerUps.Length)], dzlys[1].transform);
                _powerUpInstance.transform.position = new Vector3(_powerUpSpawnPoint.x, _powerUpSpawnPoint.y, dzlys[1].transform.position.z);
                _powerUpInstance.transform.localPosition = new Vector3(_powerUpInstance.transform.localPosition.x, _powerUpInstance.transform.localPosition.y, 2);
            }
        }
        else
        {
            if (!_playerInstance.affectedByPowerUp && _canStartCanSpawnPowerUp && _powerUpInstance == null)
            {
                _canStartCanSpawnPowerUp = false;
                StartCoroutine(CanSpawnPowerUp());
            }
        }
    }

    void SpawnBackground()
    {
        _backgroundInstance = Instantiate(bg, dzlys[8].transform);
        _backgroundInstance.transform.position = new Vector3(_backgroundInstance.transform.position.x, _backgroundInstance.transform.position.y, dzlys[8].transform.parent.position.z);
        _backgroundInstance.transform.localPosition = new Vector3(_backgroundInstance.transform.localPosition.x, _backgroundInstance.transform.localPosition.y, 10);
    }

    void Bounds()
    {
        if (_playerInstance.transform.position.x < -8.45)
            _playerInstance.transform.position = new Vector3(-8.45f, _playerInstance.transform.position.y, _playerInstance.transform.position.z);
        else if (_playerInstance.transform.position.x > 8.45)
            _playerInstance.transform.position = new Vector3(8.45f, _playerInstance.transform.position.y, _playerInstance.transform.position.z);

        if (_playerInstance.transform.position.y < -4.305)
            _playerInstance.transform.position = new Vector3(_playerInstance.transform.position.x, -4.305f, _playerInstance.transform.position.z);
        else if (_playerInstance.transform.position.y > 0)
            _playerInstance.transform.position = new Vector3(_playerInstance.transform.position.x, 0, _playerInstance.transform.position.z);
    }

    IEnumerator CanSpawnPlayer()
    {
        _canStartCanSpawnPlayer = false;
        yield return new WaitForSeconds(playerSpawnRatio);
        PlayerPrefs.SetInt("_p_alive", 1);
        _canStartCanSpawnPlayer = true;
        StopCoroutine(CanSpawnPlayer());
    }

    IEnumerator CanSpawnEnemy()
    {
        yield return new WaitForSeconds(enemySpawnRatio);
        _canSpawnEnemy = true;
        _canStartCanSpawnEnemy = true;
        StopCoroutine(CanSpawnEnemy());
    }

    IEnumerator CanSpawnPowerUp()
    {
        yield return new WaitForSeconds(powerUpSpawnRatio);
        _canSpawnPowerUp = true;
        _canStartCanSpawnPowerUp = true;
        StopCoroutine(CanSpawnPowerUp());
    }
    #endregion
}

