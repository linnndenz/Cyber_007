using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    //GameObject Driver = GameObject.Find("car");
    //private Rigidbody rigidbody;
    float speed = -13.0F;
    public GameObject StartObject;
    void Awake()
    {
        //rigidbody = this.GetComponent<Rigidbody>(); //��ȡ����ű���rigidbody 2D
    }
    private void OnTriggerEnter(Collider other)
    {
      
        //DestroyImmediate(StartObject, true);
    }
    private void Start()
    {

       // Destroy(gameObject, 13); //�����ݻ��������
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            speed = -18.0f;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            speed = -13.0f;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }



    }



    //public void Lauch(Vector2 direction, float force)
    //{
    //    rigidbody.AddForce(direction * force);  //direction��һ��ʸ��

    //}
}
