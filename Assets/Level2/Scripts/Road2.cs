using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road2 : MonoBehaviour
{

    float speed = 36.0F;

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(25, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            speed = 72.0f;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            speed = 36.0f;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

    }
}
