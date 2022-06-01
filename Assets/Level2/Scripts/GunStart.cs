using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStart : MonoBehaviour
{
    float timer = 0;
    int a = 9;
    public GameObject StartPoint;
    public GameObject StartMiss;
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
          
            //Vector3 poiont = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value, -Camera.main.transform.position.z));
            //GameObject fish = Instantiate(carPrefab, poiont, carPrefab.transform.rotation) as GameObject;
            Vector3 poiont = transform.position;
            GameObject startplace = Instantiate(StartPoint, poiont, StartPoint.transform.rotation) as GameObject;
            GameObject startmissing = Instantiate(StartMiss, poiont, StartPoint.transform.rotation) as GameObject;
            a = 15;
        }

    }
}
