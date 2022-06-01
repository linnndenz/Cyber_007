using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarNew : MonoBehaviour
{
    public GameObject Passenger;
    public GameObject End;
    private bool isChat = false;
    public string ChatName;
    //float speed = -4.0F;
    bool a = true;
    bool b = true;
    int i = 1;
    public int score = 0;
    float faceangle;
    //private float origionZ;
    //private Quaternion targetRotation;

    float timer = 0;

    GameObject pingfen;

    public SpriteRenderer thisRenderer;
    float shankeTime = 0f;
    public bool isShake = false;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameObject.transform.position = new Vector3(-1, -2, 15);
        pingfen = GameObject.Find("脸");
        //origionZ = pingfen.transform.rotation.z;


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Car")
        {
            score = score + 1;
            Debug.Log("碰撞开始");
            Destroy(other.gameObject);
            isShake = true;
            faceangle = score * 36f;
            //targetRotation = Quaternion.Euler(0, 0, faceangle * score + origionZ) * Quaternion.identity;
        }
    }

    private void OntriggerEnter(Collider other)
    {
        if (other.tag == "Start" /*&& Input.GetKeyDown("e")*/)
        {
            a = false;

            isChat = true;
            //Destroy(Passenger);
            Debug.Log("与目标小于三米");

        }
        if (other.tag == "End" && Input.GetKeyDown("e"))
        {
            b = false;

        }


    }



    void ChangeIsshake()
    {
        isShake = false;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.AngleAxis(faceangle - 36, Vector3.back);

        pingfen.transform.rotation = rotation;
        //pingfen.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20);

        ToChangeColor();
        if (isShake)
        {
            ToChangeColor();
            Invoke("ChangeIsshake", 1.5f);
        }

        float sqrLenght = (Passenger.transform.position - transform.position).sqrMagnitude;
        float sqrLenght1 = (End.transform.position - transform.position).sqrMagnitude;

        if (score > 9)
        {
            SceneManager.LoadScene("Level2_1");
        }

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


        if (a == true)
        {
            timer += Time.deltaTime;
            if (timer >= 16)
            {
                timer = 0;

                Vector3 a = new Vector3(30, -10, 15);
                GameObject passenger = Instantiate(Passenger, a, Passenger.transform.rotation) as GameObject;
                Destroy(passenger, 25);

            }
        }

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
