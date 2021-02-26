using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region Variables
    [Header("Enemy Atributtes")]
    public float dyingAnimationSpeed, verticalMovimentForce, shootRatio, viewField;

    /*This variable(s) does not appear in the inspector*/ private Animator _animator;
    /*This variable(s) does not appear in the inspector*/ private bool _canShoot, _alive;

    [Header("Enemy Controlled Objects")]
    public BulletBehaviour laserBullet;
    public GameObject eyes;
    public GameObject[] frontGuns;

    /*This variable(s) does not appear in the inspector*/ private AudioSource _laserBulletFX;
    /*This variable(s) does not appear in the inspector*/private LevelManager _thisLevelManager;
    /*This variable(s) does not appear in the inspector*/ private PlayerBehaviour _playerInstance;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _canShoot = true;
        _alive = true;

        _laserBulletFX = GameObject.Find("fxLaserShot").GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        _thisLevelManager = FindObjectOfType<LevelManager>();
        _playerInstance = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update()
    {
        if (!_thisLevelManager.isCutSceneHappening)
            if(_alive)
                Shoot();

        Debug.DrawLine(eyes.transform.position, eyes.transform.position + Vector3.down * viewField, Color.green);
    }

    private void FixedUpdate()
    {
        if (!_thisLevelManager.isCutSceneHappening)
            if(_alive)
                Move();
    }

    private void OnBecameInvisible()
    {
        if (this.gameObject != null)
        {
            if (PlayerPrefs.GetInt("Alive") == 1)
                transform.position = new Vector3((Random.Range(-Screen.width + _thisLevelManager.horizontalScreenOffset, Screen.width - _thisLevelManager.horizontalScreenOffset)) / 120, (Screen.height + _thisLevelManager.verticalScreenOffset) / 120, transform.position.z);
            else
            {
                _thisLevelManager.numberOfEnemiesOnScreen = 0;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet") && collider.gameObject.GetComponent<BulletBehaviour>().shotBy == "Player" && _alive == true)
            StartCoroutine(Die());
    }
    #endregion

    #region Personalized Methods
    void Move()
    {
        transform.Translate(Vector3.down * verticalMovimentForce * Time.deltaTime);
    }

    void Shoot()
    {
        RaycastHit2D hit = Physics2D.Linecast(eyes.transform.position, eyes.transform.position + Vector3.down * viewField, 1 << LayerMask.NameToLayer("Characters"));

        if (hit.collider != null && _canShoot)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (PlayerPrefs.GetInt("_p_alive") == 1)
                {
                    _canShoot = false;

                    foreach (GameObject gun in frontGuns)
                    {
                        BulletBehaviour _laserBulletInstance = Instantiate(laserBullet, gun.transform.position, Quaternion.identity);
                        _laserBulletInstance.GetComponent<BulletBehaviour>().shotBy = "Enemy";
                        _laserBulletInstance.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.parent.position.z);
                        _laserBulletInstance.direction = -1;
                    }

                    if (_laserBulletFX != null)
                        _laserBulletFX.Play();

                    StartCoroutine(CanShoot());
                }
            }
        }
    }

    IEnumerator CanShoot()
    {
        yield return new WaitForSeconds(shootRatio);
        _canShoot = true;
        StopCoroutine(CanShoot());
    }

    IEnumerator Die()
    {
        _thisLevelManager.numberOfEnemiesOnScreen -= 1;
        PlayerPrefs.SetInt("_p_score", PlayerPrefs.GetInt("_p_score") + 10);
        _alive = false;
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(_animator.speed * dyingAnimationSpeed);
        Destroy(this.gameObject);
        StopCoroutine(Die());
    }
    #endregion
}
