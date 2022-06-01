using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunpassenger : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 0;
    int a = 8;
    public GameObject Passengerfresh; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= a)
        {
            timer = 0;

            Vector3 a = new Vector3(30, -10, 15);
            GameObject passenger = Instantiate(Passengerfresh, a, Passengerfresh.transform.rotation) as GameObject;
            Destroy(passenger, 25);
            
        }

    }
}
