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
}
