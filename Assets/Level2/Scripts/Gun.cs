using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject rocket;
    public float speed;
    // Use this for initialization

   

   public IEnumerator Func()
    {
        while (true)// or for(i;i;i)
        {
            yield return new WaitForSeconds(15.0f); // first
                                                   //Specific functions put here 
            GameObject projectile_bl = Instantiate(rocket, transform.position, transform.rotation) as GameObject;  // then
                                                                                                                   // Note the order of codes above.  Different order shows different outcome.
        }
    }
    void Start()
    {
        StartCoroutine(Func());

    }

    // Update is called once per frame
    /* void Update()
     {
         if (Input.GetButtonDown("Fire1"))
         {
             GameObject projectile_bl = Instantiate(rocket, transform.position, transform.rotation) as GameObject;
             // projectile_bl.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
           //  projectile_bl.transform.Translate(Vector3.right * Time.deltaTime * speed);
         }
     }*/
}
