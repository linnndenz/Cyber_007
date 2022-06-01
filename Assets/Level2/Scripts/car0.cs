using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car0 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("w") && transform.position.y < 10)
        {
            transform.position += new Vector3(0, 6, 0);
        }
        if (Input.GetKeyDown("s") && transform.position.y > -8)
        {
            transform.position += new Vector3(0, -6, 0);
        }
    }
}
