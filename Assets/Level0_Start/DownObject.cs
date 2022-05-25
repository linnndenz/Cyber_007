using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownObject : MonoBehaviour
{

    [Header("ƽ��")]
    [Tooltip("���")] public float amplitude = 3f;
    public float moveSpeed = 0.5f;
    public float moveOffset = 0;

    //ƽ�Ʋ���
    Vector3 pos0;//initpos
    Vector3 pos1;//movepos
    void Start()
    {
        pos0 = transform.position;
    }

    void Update()
    {
        pos1 = pos0;
        pos1.y = Mathf.Sin(Time.fixedTime * Mathf.PI * moveSpeed + moveOffset) * amplitude + pos0.y;
        transform.position = pos1;
    }
}
