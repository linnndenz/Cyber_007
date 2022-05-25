using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerGame : MonoBehaviour
{
    public int hp;
    public GameObject[] levels = new GameObject[4];
    public int level;//关卡
    public int cnt;//关卡内的点数
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

    //得分
    public void GetPoint()
    {
        cnt++;
        if (cnt >= 3) {
            NextLevel();
        }
    }

    //碰到文字受伤
    public void GetHurt()
    {
        hp--;
        if (hp <= 0) {
            ResetLevel();
        }
    }

    //下一关
    public void NextLevel()
    {
        level++;
        hp = 3;
        cnt = 0;

        //游戏结束
        if (level == 4) {
            if (!l3Manager.isGetTable) {
                flowChart.ExecuteBlock("V植入_没登记表");
            } else if (!l3Manager.isGetDiary1) {
                flowChart.ExecuteBlock("V植入_登记表错误");
            } else {
                flowChart.ExecuteBlock("V植入");
            }
            l3Manager.CloseMinerGame();
            return;
        }
        if (level == 5) {
            FinishMinerGame();
            return;
        }

        //下一关
        levels[level - 2].SetActive(false);
        levels[level - 1].SetActive(true);
    }

    //关卡失败重置关卡
    public void ResetLevel()
    {
        cnt = 0;
        hp = 3;
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
        flowChart.ExecuteBlock("植入完成");
    }

}
