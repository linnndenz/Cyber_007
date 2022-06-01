using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Carlevel2 : MonoBehaviour
{
    //public GameObject Passenger;
    //public GameObject End;
    //public GameObject StartPoint;
    //public GameObject EndPoint;
    //public GameObject StartNext;
    //private bool isChat = false;
    //private bool isBoom = false;
    //private bool isGameover = false;
    //private bool isStartmiss = false;
    //private bool isEndmiss = false;
    public string oneaName;
    public string onebName;
    public string twoaName;
    public string twobName;
    public string twoaaName;
    public string twoabName;
    public string twobaName;
    public string twobcName;
    //float speed = -4.0F;
    //bool a = true;
    //bool b = true;
    //int i = 1;
    public int score = 0;
    float faceangle;
    //private float origionZ;
    //private Quaternion targetRotation;



    GameObject pingfen;

   
    void Start()
    {
        score = 0;
        gameObject.transform.position = new Vector3(-1, -8, 15);
        pingfen = GameObject.Find("脸");
        //origionZ = pingfen.transform.rotation.z;


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "1a")
        {
            score = score + 1;
           
            Say1a();
            Destroy(other.gameObject);
          
            faceangle = score * 36f;
            //targetRotation = Quaternion.Euler(0, 0, faceangle * score + origionZ) * Quaternion.identity;
        }

        if (other.tag == "1b"/*&& Input.GetKeyDown("e")*/)
        {
            Say1b();
        }
        if (other.tag == "2a" /*&& Input.GetKeyDown("e")*/)
        {
            Say2a();

        }
        if (other.tag == "2b" /*&& Input.GetKeyDown("e")*/)
        {
            Say2b();
        }
        if (other.tag == "2aa" /*&& Input.GetKeyDown("e")*/)
        {
            Say2aa();
        }
        if (other.tag == "2ab" /*&& Input.GetKeyDown("e")*/)
        {
            Say2ab();
        }
        if (other.tag == "2ba" /*&& Input.GetKeyDown("e")*/)
        {
            Say2ba();
        }
        if (other.tag == "2bc" /*&& Input.GetKeyDown("e")*/)
        {
            Say2bc();
        }




    }
   

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.AngleAxis(faceangle - 36, Vector3.back);

        pingfen.transform.rotation = rotation;
        //pingfen.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20);


        //float sqrLenght = (Passenger.transform.position - transform.position).sqrMagnitude;
        //float sqrLenght1 = (End.transform.position - transform.position).sqrMagnitude;


        if (Input.GetKeyDown("w") && transform.position.y < 10)
        {
            transform.position += new Vector3(0, 6, 0);
        }
        if (Input.GetKeyDown("s") && transform.position.y > -8)
        {
            transform.position += new Vector3(0, -6, 0);
        }

        //if (sqrLenght < 4 * 4 && Input.GetKeyDown("e"))
        //{
        //    a = false;

        //    isChat = true;
        //    //Destroy(Passenger);
        //    Debug.Log("与目标小于三米");
        //}




      

        //else
        //{
        //    Passenger.transform.Translate(Vector3.right * Time.deltaTime * speed);
        //}


    }
    void Say1a()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(oneaName) )
        {
            flowChart.ExecuteBlock(oneaName);

        }
    }

    void Say1b()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(onebName) )
        {
            flowChart.ExecuteBlock(onebName);

        }
    }

    void Say2a()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(twoaName))
        {
            flowChart.ExecuteBlock(twoaName);

        }
    }

    void Say2b()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(twobName) )
        {
            flowChart.ExecuteBlock(twobName);

        }
    }
    void Say2aa()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(twoaaName) )
        {
            flowChart.ExecuteBlock(twoaaName);

        }
    }
    void Say2ab()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(twoabName))
        {
            flowChart.ExecuteBlock(twoabName);

        }
    }
    void Say2ba()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(twobaName))
        {
            flowChart.ExecuteBlock(twobaName);

        }
    }
    void Say2bc()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(twobcName))
        {
            flowChart.ExecuteBlock(twobcName);

        }
    }



}

