using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUI_L1 : BagUI
{
    void Awake()
    {
        //��������
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    void Start()
    {
        Init();
    }


}
