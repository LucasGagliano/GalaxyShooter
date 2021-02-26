using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class PlayerBehaviour : MonoBehaviour
{
    #region Variables
    [Header("Player Atributtes")]
    public float verticalMovimentForce, shootRatio, dyingAnimationSpeed;

    /*This variable(s) will not appear in the inspector*/ private bool _canShoot, _fourShots, _shield;
    /*This variable(s) will not appear in the inspector*/ private Animator _animator;
    /*This variable(s) will not appear in the inspector*/ public int numberOfLives;
    /*This variable(s) will not appear in the inspector*/ public bool affectedByPowerUp;
    /*This variable(s) will not appear in the inspector*/ public bool damage;

    [Header("Player Controlled Objects")]
    public GameObject shieldObj, leftEngine, rightEngine;
    public GameObject[] frontGuns, backGuns;
    public BulletBehaviour laserBullet;

    /*This variable(s) will not appear in the inspector*/ private AudioSource _laserBulletFX;
    /*This variable(s) will not appear in the inspector*/ private AudioSource _engineFX;
    /*This variable(s) will not appear in the inspector*/ private LevelManager _thisLevel;
    /*This variable(s) will not appear in the inspector*/ private UIManager _thisUI;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _canShoot = true;
        numberOfLives = PlayerPrefs.GetInt("_p_currentNumberOfLives");

        if (GameObject.Find("fxLaserShot") != null)
            _laserBulletFX = GameObject.Find("fxLaserShot").GetComponent<AudioSource>();
        else
            Debug.LogError("Could not find an object named 'fxLaserShot'. The ships's shooting sound will not happen");

        if (GameObject.Find("fxEngine") != null)
            _engineFX = GameObject.Find("fxEngine").GetComponent<AudioSource>();
        else
            Debug.LogError("Could not find an object named 'fxEngine'. The ships's engine sound will not happen");

        _animator = GetComponent<Animator>();
        _thisLevel = FindObjectOfType<LevelManager>();
        _thisUI = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        if (_engineFX != null)
            _engineFX.Play();

        if (numberOfLives > 0)
            PlayerPrefs.SetInt("_p_alive", 1);

        if (verticalMovimentForce == 0)
            Debug.LogWarning("The vertical movement force is currently 0. Notice that the character will not move.");
    }

    void Update()
    {
        if (!_thisLevel.isCutSceneHappening)
            if (PlayerPrefs.GetInt("_p_alive") == 1)
                if (_canShoot)
                    Shoot();
    }

    private void FixedUpdate()
    {
        if (!_thisLevel.isCutSceneHappening)
            if (PlayerPrefs.GetInt("_p_alive") == 1)
                Move();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.CompareTag("Bullet") && collider.gameObject.GetComponent<BulletBehaviour>().shotBy == "Enemy" && !damage) || collider.CompareTag("Enemy"))
            StartCoroutine(Damage());

        if (collider.CompareTag("PowerUp"))
            StartCoroutine(IsAffectedByPowerUp(collider.gameObject.transform.GetChild(0).tag));

    }
    #endregion

    #region Personalized Methods
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 horizontalDirection, verticalDirection;

        horizontalDirection = (h > 0) ? Vector3.right : Vector3.left;
        verticalDirection = (v > 0) ? Vector3.up : Vector3.down;

        if (Input.GetButton("Horizontal"))
            transform.Translate(horizontalDirection * verticalMovimentForce * Time.deltaTime * 1);

        if (Input.GetButton("Vertical"))
            transform.Translate(verticalDirection * verticalMovimentForce * Time.deltaTime * 1);
    }

    private void Shoot()
    {
        if (Input.GetButton("Shoot"))
        {
            if (laserBullet != null)
            {
                if (_fourShots)
                {
                    if (frontGuns.Length != 0 && backGuns.Length != 0)
                    {
                        foreach (GameObject gun in frontGuns)
                        {
                            if (gun != null)
                            {
                                _canShoot = false;

                                BulletBehaviour _laserBulletInstance = Instantiate(laserBullet, gun.transform.position, Quaternion.identity);
                                _laserBulletInstance.GetComponent<BulletBehaviour>().shotBy = "Player";
                                _laserBulletInstance.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z);
                                _laserBulletInstance.direction = 1;

                                if (_laserBulletFX != null)
                                    _laserBulletFX.Play();

                                StartCoroutine(CanShoot());
                            }
                            else
                                Debug.LogError("Null Pointer Exception: you did not assign an object to some of the 'Front Guns'");
                        }
                        foreach (GameObject gun in backGuns)
                        {
                            if (gun != null)
                            {
                                BulletBehaviour _laserBulletInstance = Instantiate(laserBullet, gun.transform.position, Quaternion.identity);
                                _laserBulletInstance.GetComponent<BulletBehaviour>().shotBy = "Player";
                                _laserBulletInstance.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z);
                                _laserBulletInstance.direction = 1;
                            }
                            else
                                Debug.LogError("Null Pointer Exception: you did not assign an object to some of the 'Back Guns'");
                        }

                    }
                    else
                        Debug.LogError("Null Pointer Exception: you did not assign a size to the 'Front Guns' or to the 'Back Guns'");

                }
                else
                {
                    if (frontGuns.Length != 0)
                    {
                        foreach (GameObject gun in frontGuns)
                        {
                            if (gun != null)
                            {
                                _canShoot = false;

                                BulletBehaviour _laserBulletInstance = Instantiate(laserBullet, gun.transform.position, Quaternion.identity);
                                _laserBulletInstance.GetComponent<BulletBehaviour>().shotBy = "Player";
                                _laserBulletInstance.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z);
                                _laserBulletInstance.direction = 1;

                                if (_laserBulletFX != null)
                                    _laserBulletFX.Play();

                                StartCoroutine(CanShoot());
                            }
                            else
                                Debug.LogError("Null Pointer Exception: you did not assign an object to some of the 'Front Guns'");
                        }
                    }
                    else
                        Debug.LogError("Null Pointer Exception: you did not assign a size to the 'Front Guns'");
                }

            }
            else
                Debug.LogError("Null Pointer Exception: you did not assign an object to the variable 'Laser Bullet'");
        }
    }

    private IEnumerator CanShoot()
    {
        yield return new WaitForSeconds(shootRatio);
        _canShoot = true;
        StopCoroutine(CanShoot());
    }

    private IEnumerator IsAffectedByPowerUp(string objectID)
    {
        affectedByPowerUp = true;

        switch (objectID)
        {
            case "powerup_fourshots":
                _fourShots = true;
                yield return new WaitForSeconds(15f);
                _fourShots = false;
                break;

            case "powerup_shield":
                shieldObj.SetActive(true);
                _shield = true;
                yield return new WaitForSeconds(15f);
                shieldObj.SetActive(false);
                _shield = false;
                break;
        }

        StopCoroutine(IsAffectedByPowerUp(null));
        affectedByPowerUp = false;
    }

    IEnumerator Damage()
    {
        damage = true;

        if (_shield)
        {
            shieldObj.SetActive(false);
            _shield = false;
            StopCoroutine(IsAffectedByPowerUp(null));
            affectedByPowerUp = false;
            damage = false;
        }
        else
        {
            numberOfLives -= 1;
            PlayerPrefs.SetInt("_p_currentNumberOfLives", numberOfLives);
            _thisUI.canDrawLives = true;
            PlayerPrefs.SetInt("_p_alive", 0);
            _animator.SetTrigger("Die");
            yield return new WaitForSeconds(_animator.speed * dyingAnimationSpeed);
            Destroy(this.gameObject);
        }

    }
    #endregion
}
