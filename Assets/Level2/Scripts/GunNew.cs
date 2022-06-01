using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunNew : MonoBehaviour
{

    float timer = 0;
    int a = 9;
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
            GameObject carPrefab = Resources.Load<GameObject>("car" + Random.Range(0, 14));
            //Vector3 poiont = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, Random.value, -Camera.main.transform.position.z));
            //GameObject fish = Instantiate(carPrefab, poiont, carPrefab.transform.rotation) as GameObject;
            Vector3 poiont = transform.position;
            GameObject fish = Instantiate(carPrefab, poiont, carPrefab.transform.rotation) as GameObject;
            Destroy(fish, 25);
             a = Random.Range(7, 13);
        }
        
    }
}
