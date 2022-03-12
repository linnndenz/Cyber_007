using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_L1_Pre : Player
{
    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.R)) {
            
        }

    }

    protected override void Talk(){}
}
