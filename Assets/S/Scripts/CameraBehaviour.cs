using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    GameObject objectToFollow;
    public string objectToFollowTag;
    public GameObject[] cameraPoints;
    public float speed;

    private void Awake() 
    {
        objectToFollow = GameObject.FindGameObjectWithTag(objectToFollowTag);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 direction = (h > 0) ? Vector3.right : Vector3.left;

        if(Input.GetButton("Horizontal"))
            transform.Translate(direction * Vector3.Distance(cameraPoints[0].transform.position, cameraPoints[1].transform.position) * speed * Time.deltaTime);
    }
}
