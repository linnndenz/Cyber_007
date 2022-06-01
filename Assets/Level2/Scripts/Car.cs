using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public GameObject Passenger;
    public GameObject End;
    public GameObject StartPoint;
    public GameObject EndPoint;
    public GameObject StartNext;
    private bool isChat = false;
    private bool isBoom = false;
    private bool isGameover = false;
    private bool isStartmiss = false;
    private bool isEndmiss = false;
    public string StartmissName;
    public string EndmissName;
    public string GameoverName;
    public string BoomName;
    public string ChatName;

    public AudioClip W;
    public AudioClip S;
    AudioSource Station;

    //float speed = -4.0F;
    bool a=true;
    bool b = true;
    int i = 1;
    public int score=0;
    float faceangle ;
    private float origionZ;
    private Quaternion targetRotation;

   

    GameObject pingfen;

    public SpriteRenderer thisRenderer;
    float shankeTime = 0f;
    public bool isShake = false;
    // Start is called before the first frame update
    void Start()
    {
        Station = GetComponent<AudioSource>();
        score = 0;
        gameObject.transform.position = new Vector3(-1, -2, 15);
        pingfen = GameObject.Find("脸");
        origionZ = pingfen.transform.rotation.z;
        

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Car")
        {
            score = score +1;
            isBoom = true;
            Say1();
            Destroy(other.gameObject);
            isShake = true;
            faceangle = score * 36f;
            //targetRotation = Quaternion.Euler(0, 0, faceangle * score + origionZ) * Quaternion.identity;
        }

        if (other.tag == "Start"/*&& Input.GetKeyDown("e")*/)
        {
           /* GameObject */Passenger = other.gameObject;
            a = false;
            isChat = true;
            StartPoint.SetActive(false);
            
            //DestroyImmediate(StartPoint, true);
            //Destroy(StartPoint);
            //targetRotation = Quaternion.Euler(0, 0, faceangle * score + origionZ) * Quaternion.identity;
        }
        if (other.tag == "End" /*&& Input.GetKeyDown("e")*/)
        {
            Station.Play();

            End = other.gameObject;
            b = false;
            EndPoint.SetActive(false);
            StartNext.SetActive(true);
           
        }
        if (other.tag == "Startmiss" /*&& Input.GetKeyDown("e")*/)
        {
            isStartmiss = true;
            score = score + 1;
            Say3();
            faceangle = score * 36f;
        }
        if (other.tag == "Endmiss" /*&& Input.GetKeyDown("e")*/)
        {
            isEndmiss = true;
            score = score + 1;
            Say4();
            faceangle = score * 36f;
        }

    }
    void ChangeIsshake()
    {
        isShake = false;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.AngleAxis(faceangle-36, Vector3.back);

        pingfen.transform.rotation = rotation;
        //pingfen.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20);

        ToChangeColor();
        if (isShake)
        {
            ToChangeColor();
            Invoke("ChangeIsshake", 1.5f);
        }

        //float sqrLenght = (Passenger.transform.position - transform.position).sqrMagnitude;
        //float sqrLenght1 = (End.transform.position - transform.position).sqrMagnitude;
       
        if (score > 9)
        {
            isGameover = true;
            Say2();
            //SceneManager.LoadScene("Level2_1");
        }

        if (Input.GetKeyDown("w")&& transform.position.y<10) 
        {
            transform.position += new Vector3(0, 6, 0);
            AudioSource.PlayClipAtPoint(W, transform.position);
        }
        if (Input.GetKeyDown("s") && transform.position.y >-8)
        {
            transform.position += new Vector3(0, -6, 0);
            AudioSource.PlayClipAtPoint(S, transform.position);
        }

        //if (sqrLenght < 4 * 4 && Input.GetKeyDown("e"))
        //{
        //    a = false;

        //    isChat = true;
        //    //Destroy(Passenger);
        //    Debug.Log("与目标小于三米");
        //}




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
            //Passenger.SetActive(true);
        }


        //else
        //{
        //    Passenger.transform.Translate(Vector3.right * Time.deltaTime * speed);
        //}


    }
    void Say4()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(ChatName) && isEndmiss== true)
        {
            flowChart.ExecuteBlock(EndmissName);

        }
    }

    void Say3()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(ChatName) && isStartmiss == true)
        {
            flowChart.ExecuteBlock(StartmissName);

        }
    }

    void Say2()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(ChatName) && isGameover == true)
        {
            flowChart.ExecuteBlock(GameoverName);

        }
    }

    void Say1()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(ChatName) && isBoom == true)
        {
            flowChart.ExecuteBlock(BoomName);

        }
    }
    void Say()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(ChatName) && isChat == true && i <= 2)
        {
            flowChart.ExecuteBlock(ChatName);

        }
    }

 

    void ToChangeColor()
    {
        if (isShake)
        {
            shankeTime += Time.deltaTime;
            if (shankeTime % 1 > 0.5f)
            {
                thisRenderer.material.color = new Color32(255, 255, 255, 60);
            }
            else
            {
                thisRenderer.material.color = new Color32(255, 255, 255, 255);
            }

        }



    }
}
