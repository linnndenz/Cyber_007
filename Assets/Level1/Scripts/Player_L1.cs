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
                case "�����ĵ���":
                    l1Manager.OpenEggUI();
                    break;

            }
        }

    }

    bool bFirstToCook = true;
    bool bFirstToSoldier = true;
    protected override void Talk()
    {
        base.Talk();
        switch (coll.name) {
            case "С��A":
                flowChart.ExecuteBlock("С��A");
                break;
            case "��ʦ":
                if (bFirstToCook) {
                    flowChart.ExecuteBlock("Cook_First");
                    bFirstToCook = false;
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
            case "ʿ��":
                if (bFirstToSoldier) {
                    flowChart.ExecuteBlock("ʿ��_First");
                    bFirstToSoldier = false;
                } else {
                    if (l1Manager.isGetTomato) {
                        flowChart.ExecuteBlock("ʿ��_��ú�����");

                    } else {
                        flowChart.ExecuteBlock("ʿ��_Repeat");
                    }
                }
                break;
            case "����":
                flowChart.ExecuteBlock("����");
                break;
        }
    }
}

