using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerGame : MonoBehaviour
{
    [Header("Ѫ��")]
    public GameObject[] hpSrs;
    public int hp;
    [Header("�ؿ�")]
    public GameObject[] levels = new GameObject[4];
    public int level;//�ؿ�
    public int cnt;//�ؿ��ڵĵ���
    public LevelManager_L3 l3Manager;
    public GameObject brain;
    public Flowchart flowChart;

    private Claw claw;
    void Start()
    {
        hp = 3;
        level = 1;
        claw = GetComponentInChildren<Claw>();
    }

    void OnEnable()
    {
        for (int i = 0; i < levels.Length; i++) {
            if (i == level - 1) {
                levels[i].SetActive(true);
            } else {
                levels[i].SetActive(false);
            }
        }
    }

    //�÷�
    public void GetPoint()
    {
        LevelManager.Instance.audioManager.PlaySE(0);
        cnt++;
        if (cnt >= 3) {
            NextLevel();
        }
    }

    //������������
    public void GetHurt()
    {
        hp--;
        hpSrs[hp].SetActive(false);
        if (hp <= 0) {
            ResetLevel();
        }
    }

    //��һ��
    public void NextLevel()
    {
        level++;
        hp = 3;
        for (int i = 0; i < 3; i++) {
            hpSrs[i].SetActive(true);
        }
        cnt = 0;
        

        //��Ϸ����
        if (level == 4) {
            if (!l3Manager.isGetTable) {
                flowChart.ExecuteBlock("Vֲ��_û�ǼǱ�");
            } else if (!l3Manager.isFinTable) {
                flowChart.ExecuteBlock("Vֲ��_�ǼǱ����");
            } else {
                flowChart.ExecuteBlock("Vֲ��");
            }
            l3Manager.CloseMinerGame();
            return;
        }
        if (level == 5) {
            FinishMinerGame();
            return;
        }

        //��һ��
        levels[level - 2].SetActive(false);
        levels[level - 1].SetActive(true);
    }

    //�ؿ�ʧ�����ùؿ�
    public void ResetLevel()
    {
        cnt = 0;
        hp = 3;
        for (int i = 0; i < 3; i++) {
            hpSrs[i].SetActive(true);
        }
        Transform tmp = levels[level - 1].transform.Find("Points");
        for (int i = 0; i < tmp.childCount; i++) {
            tmp.GetChild(i).gameObject.SetActive(true);
        }
        claw.ResetClaw();
    }

    public void FinishMinerGame()
    {
        l3Manager.CloseMinerGame();
        brain.SetActive(true);
        flowChart.ExecuteBlock("ֲ�����");
    }

}
