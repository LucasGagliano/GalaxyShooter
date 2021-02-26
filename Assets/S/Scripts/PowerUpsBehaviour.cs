using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsBehaviour : MonoBehaviour
{
    #region Variables
    public float verticalMovementForce;
    #endregion

    #region Unity Methods
    private void Update()
    {
        Move();
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            Destroy(this.gameObject);
    }
    #endregion

    #region Personalized Methods
    void Move()
    {
        transform.Translate(Vector3.down * verticalMovementForce * Time.deltaTime);
    }
    #endregion
}

