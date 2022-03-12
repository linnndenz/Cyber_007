using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player_L1 : Player
{
    #region 流程数据
    LevelManager_L1 l1Manager;
    #endregion

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        l1Manager = (LevelManager_L1)levelManager;
    }


    protected override void Update()
    {
        base.Update();
        //特殊交互物品，在子类中写
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(INTERACTIVEITEM)) {
            switch (coll.name) {
                case "镜子":

                    break;
                case "书":
                    l1Manager.OpenBook();
                    break;
                case "草莓花":
                    Talk();
                    break;
                case "密码盒":
                    l1Manager.OpenLockedBoxUI();
                    break;

            }
        }

    }

    bool bFirstToCool = true;
    protected override void Talk()
    {
        switch (coll.name) {
            case "小孩A":
                flowChart.ExecuteBlock("小孩A");
                break;
            case "厨师":
                if (bFirstToCool) {
                    flowChart.ExecuteBlock("Cook_First");
                    bFirstToCool = false;
                } else {
                    if (l1Manager.isGetEgg && l1Manager.isGetStrawbbry) {
                        flowChart.ExecuteBlock("Cook_Both");

                    } else {
                        flowChart.ExecuteBlock("Cook_Repeat");
                    }
                }
                break;
            case "草莓花":
                flowChart.ExecuteBlock("ROBO_草莓花");
                break;
        }
    }
}

