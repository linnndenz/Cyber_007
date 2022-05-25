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


    public bool isHos;
    protected override void Update()
    {
        base.Update();
        //���⽻����Ʒ����������д
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(INTERACTIVEITEM)) {
            switch (coll.name) {
                case "����":
                    l1Manager.OpenMirror();
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
                case "����":
                    l1Manager.audioManager.PlaySE(4);
                    break;
                case "�¶�Ժ����":
                    if (!froze) l1Manager.audioManager.PlaySE(4);
                    break;
                case "�칫�Ҵ���":
                    if (!froze) l1Manager.audioManager.PlaySE(5);
                    break;

            }
        }


        if (coll != null && coll.name == "ҽԺ��" &&!isHos) {
            l1Manager.OpenHospitalGate();
            isHos = true;
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
            case "С��B":
                flowChart.ExecuteBlock("С��B");
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
            case "��ʺ�":
                flowChart.ExecuteBlock("��ʺ�");
                break;
        }
    }
}

