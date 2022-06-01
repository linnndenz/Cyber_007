using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunbullet : MonoBehaviour
{
    public GameObject Car;
    bool b = true;
    private Rigidbody m_rigidbody;
    float speed = -4.0F;
    public int c;
    public SpriteRenderer thisRenderer;
    float shankeTime = 0f;
    public bool isShake = false;


    void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody>(); //获取这个脚本的rigidbody 2D
    }

     void Start()
    {
       
    }


    void Update()
    {

        float sqrLenght = (Car.transform.position - transform.position).sqrMagnitude;
        if (sqrLenght < 3 * 3 && b == true /*&& Car.transform.position.y==-2*/)
        {
            b = false;
            //Destroy(gameObject);

            //Car.GetComponent<Car>().score-=1;
            //Destroy(Passenger);
            Debug.Log("与目标小于三米");
            //五秒后摧毁这个物体
        }


        if (b == false)
        {

            //        //Destroy(gameObject);
            ToChangeColor();
            //        //Car.GetComponent<Car>().score-=1;
            //        //Destroy(Passenger);

        }
        Destroy(gameObject, 13);
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    void ToChangeColor()
    {
        if (isShake)
        {
            shankeTime += Time.deltaTime;
            if (shankeTime % 1 > 0.5f)
            {
                thisRenderer.material.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                thisRenderer.material.color = new Color32(255, 255, 255, 0);
            }
        }
    }





    public void Lauch(Vector2 direction, float force)
    {
        m_rigidbody.AddForce(direction * force);  //direction是一个矢量

    }
}
