using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player_L1 : Player
{
    #region ��������
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
        //���⽻����Ʒ����������д
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(INTERACTIVEITEM)) {
            switch (coll.name) {
                case "����":

                    break;
                case "��":
                    l1Manager.OpenBook();
                    break;
                case "��ݮ��":
                    Talk();
                    break;
                case "�����":
                    l1Manager.OpenLockedBoxUI();
                    break;

            }
        }

    }

    bool bFirstToCool = true;
    protected override void Talk()
    {
        switch (coll.name) {
            case "С��A":
                flowChart.ExecuteBlock("С��A");
                break;
            case "��ʦ":
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
            case "��ݮ��":
                flowChart.ExecuteBlock("ROBO_��ݮ��");
                break;
        }
    }
}

