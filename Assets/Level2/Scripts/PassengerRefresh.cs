using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerRefresh : MonoBehaviour
{
    public GameObject passenger;
    public float speed;
    // Use this for initialization



    public IEnumerator Func()
    {
        while (true)// or for(i;i;i)
        {
            yield return new WaitForSeconds(20.0f); // first
                                                   //Specific functions put here 
            GameObject projectile_bl = Instantiate(passenger, transform.position, transform.rotation) as GameObject;  // then
                                                                                                                   // Note the order of codes above.  Different order shows different outcome.
        }
    }
    void Start()
    {
        StartCoroutine(Func());

    }
}
