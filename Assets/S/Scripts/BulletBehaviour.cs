using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    #region Variables
    [Header("Bullet Atributtes")]
    public float horizontalMovimentForce;
    
    /*This variable(s) does not appear in the inspector*/ public int direction;
    /*This variable(s) does not appear in the inspector*/ public string shotBy;
    #endregion

    #region Unity Methods
    void Update()
    {
        Move();
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (((collider.CompareTag("Player") && shotBy == "Enemy") || (collider.CompareTag("Enemy") && shotBy == "Player")) || collider.gameObject.transform.GetChild(0).CompareTag("powerup_shield"))
            Destroy(this.gameObject);
    }
    #endregion

    #region Personalized Methods
    private void Move()
    {
        transform.Translate(new Vector3(0, direction, 0) * horizontalMovimentForce * Time.deltaTime);
    }
    #endregion
}

