using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Car1 : MonoBehaviour
{
    //public GameObject Car;
    public GameObject Passenger;
    public GameObject End;
    public GameObject StartPoint;
    public GameObject EndPoint;
    public GameObject StartNext;
    private bool isChat = false;
    
   
    public string ChatName;
    //float speed = -4.0F;
    bool a = true;
    bool b = true;
    int i = 1;
    //public int score;
   
    // Start is called before the first frame update
    void Start()
    {
        //score = 10;
        gameObject.transform.position = new Vector3(-1, -2, 15);
    }
    private void OnTriggerEnter(Collider other)
    {



        if (other.tag == "Start1"/*&& Input.GetKeyDown("e")*/)
        {
            /* GameObject */
            Passenger = other.gameObject;
            a = false;
            isChat = true;
            StartPoint.SetActive(false);
            //targetRotation = Quaternion.Euler(0, 0, faceangle * score + origionZ) * Quaternion.identity;
        }
        if (other.tag == "End1" /*&& Input.GetKeyDown("e")*/)
        {
            End = other.gameObject;
            b = false;
            EndPoint.SetActive(false);
            StartNext.SetActive(true);
        }
    }
        // Update is called once per frame
        void Update()
    {
        if (/*sqrLenght < 4 * 4 &&*/ a == false)
        {
            Say();
            i = i + 1;
            Passenger.transform.position = transform.position;
        }

        //if (sqrLenght1 < 4 * 4 && Input.GetKeyDown("e"))
        //{
        //    b = false;

        //}
        if (/*sqrLenght1 < 4 * 4 &&*/ b == false)
        {

            Passenger.transform.position = End.transform.position;
        }

        //else
        //{
        //    Passenger.transform.Translate(Vector3.right * Time.deltaTime * speed);
        //}


    }
    void Say()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(ChatName) && isChat == true && i <= 2)
        {
            flowChart.ExecuteBlock(ChatName);

        }
    }



}
