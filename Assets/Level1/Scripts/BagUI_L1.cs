using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUI_L1 : BagUI
{
    void Awake()
    {
        //´´½¨µ¥Àý
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }


}
