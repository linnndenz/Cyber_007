using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player_L1 : Player
{

    #region ��������
    #endregion

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

}

