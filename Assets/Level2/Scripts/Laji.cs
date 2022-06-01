using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laji : MonoBehaviour
{
    float speed = -18.0F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            speed = -36.0f;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            speed = -18.0f;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

    }
}
