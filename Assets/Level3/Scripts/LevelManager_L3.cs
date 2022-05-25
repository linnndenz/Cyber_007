using BagDataManager;
using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelManager_L3 : LevelManager
{
    public Transform playerStartPos;
    private void Awake()
    {
        Instance = this;
        player.transform.position = playerStartPos.position;
        isFirstGetInMaze = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C)) {
            Application.Quit();
        }
    }

    public override void InitItems()
    {
        item_dict.Add("登记表", new Item("登记表", false, Resources.Load<Sprite>("Texture_L3/登记表"), null, null, null, true, OpenTable));
        item_dict.Add("日记上半", new Item("日记上半", false, Resources.Load<Sprite>("Texture_L3/日记上半"), null, null, null, true, OpenDiary1));
        item_dict.Add("日记下半", new Item("日记下半", false, Resources.Load<Sprite>("Texture_L3/日记下半"), null, null, null, true, OpenDiary2));
        item_dict.Add("大脑", new Item("大脑", true, Resources.Load<Sprite>("Texture_L3/大脑"), null, UseBrain, null));
        //item_dict.Add("战争分析报告", new Item("战争分析报告", true, Resources.Load<Sprite>("Texture_L3/战争分析报告"), null, null, OpenWarReport));
    }

    //迷宫获得物品
    public void GetItemInMaze(string nname)
    {
        item_dict.TryGetValue(nname, out Item it);
        bag.AddItem(it);//背包数据更新
        BagUI.Instance.Refresh_UseItem();
    }

    #region 仪器前
    [Header("仪器前")]
    public GameObject queuePeople;
    [HideInInspector] public bool isGetQueue;//获得排队人？
    public GameObject vTable;
    [HideInInspector] public bool isGetTable;//获得登记表？

    //迷宫获得排队人
    public void GetQueue()
    {
        isGetSceneItemInMaze = true;
        isGetQueue = true;
        queuePeople.SetActive(true);
    }

    //获得登记表
    public void GetTable()
    {
        isGetBagItemInMaze = true;
        GetItemInMaze("登记表");
        isGetTable = true;

    }

    //打开登记表
    public bool OpenTable()
    {
        //互斥的关闭
        diary1.SetActive(false);
        diary2.SetActive(false);

        player.Froze();
        vTable.SetActive(true);
        return true;
    }
    #endregion

    #region 日记
    [Header("日记")]
    public GameObject diary1;
    public GameObject diary2;
    public bool isGetDiary1;//获得日记上半？
    public bool isGetDiary2;//获得日记下半？

    public void GetDiary1()
    {
        isGetBagItemInMaze = true;
        GetItemInMaze("日记上半");
        isGetDiary1 = true;
    }
    public void GetDiary2()
    {
        isGetBagItemInMaze = true;
        GetItemInMaze("日记下半");
        isGetDiary2 = true;
    }

    public bool OpenDiary1()
    {
        //互斥的关闭
        vTable.SetActive(false);
        diary2.SetActive(false);

        player.Froze();
        diary1.SetActive(true);
        return true;
    }

    public bool OpenDiary2()
    {
        //互斥的关闭
        vTable.SetActive(false);
        diary1.SetActive(false);

        player.Froze();
        diary2.SetActive(true);
        return true;
    }


    #endregion

    #region 仪器（矿工）
    [Header("仪器/矿工")]
    public GameObject questionOpenMinerGame;
    public MinerGame minerGame;
    public GameObject bagUI;
    public void OpenMinerGame()
    {
        questionOpenMinerGame.SetActive(false);
        minerGame.gameObject.SetActive(true);
        bagUI.SetActive(false);
    }
    public void CloseMinerGame()
    {
        minerGame.gameObject.SetActive(false);
        bagUI.SetActive(true);
    }

    #endregion

    #region 迷宫
    [Header("迷宫")]
    public GameObject[] mazes;
    public GameObject[] mazeInScene;
    public GameObject[] yearPages;
    public bool isGetBagItemInMaze;
    public bool isGetSceneItemInMaze;
    public bool isFirstGetInMaze;
    int firstMaze = 0;
    public void FirstInteractMaze(int index)
    {
        firstMaze = index;
        isFirstGetInMaze = false;
        flowChart.ExecuteBlock("迷宫");
    }
    public void FirstOpenMaze()
    {
        OpenMaze(firstMaze);
    }

    public void OpenMaze(int index)
    {
        mazeIndex = index;
        isGetBagItemInMaze = false;
        isGetSceneItemInMaze = false;
        mazes[index].gameObject.SetActive(true);
        player.Froze();
    }
    int mazeIndex;//012
    public void CloseMaze()
    {
        if (isGetBagItemInMaze && isGetSceneItemInMaze) {
            flowChart.ExecuteBlock("迷宫_都获得");
        } else if (isGetBagItemInMaze) {
            flowChart.ExecuteBlock("迷宫_获得背包物品");
        } else if (isGetSceneItemInMaze) {
            flowChart.ExecuteBlock("迷宫_获得场景物品");
        } else if (mazeIndex != 2) {
            player.DeFroze();
        }
        mazes[mazeIndex].gameObject.SetActive(false);
        if (mazeIndex == 2) {
            computer.SetActive(true);
        }
    }
    public void JudgeMazeIndex()
    {
        if (mazeIndex == 2) {
            player.Froze();//电脑的迷宫，对话后还是froze状态
        }
        print(mazeIndex);
    }
    //迷宫通关计数
    [Header("迷宫通关年份")]
    public int passMazeCount = 0;
    public GameObject endDoorCloth;
    public GameObject robo;
    public void PassMaze()
    {
        passMazeCount++;
        if (passMazeCount == 3) {
            robo.SetActive(true);
            endDoorCloth.SetActive(false);
        }
    }

    public void CheckYearIsRight(string yead)
    {
        try {
            if (mazeIndex == 0 && int.Parse(yead) == 2050
            || mazeIndex == 1 && int.Parse(yead) == 2063
            || mazeIndex == 2 && int.Parse(yead) == 2071) {
                PassMaze();//通关
                CloseMaze();//关闭迷宫
                yearPages[mazeIndex].gameObject.SetActive(false);//关闭年份页
                //不再通过场景开启迷宫
                if (mazeIndex != 2) {
                    mazeInScene[mazeIndex].GetComponent<Collider2D>().enabled = false;
                } else {
                    mazeInScene[2].GetComponent<Button>().enabled = false;
                }
            }
        } catch { print("年份输入有误"); }

    }

    #endregion

    #region 仪器（大脑接水管）
    [Header("大脑")]
    public bool isPutBrain;
    public SpriteRenderer brainPlate;
    public Sprite nobrain;
    public Sprite brain;
    public bool UseBrain(string toname)
    {
        if (toname != "放置盘") return false;
        brainPlate.sprite = brain;
        isPutBrain = true;
        return true;
    }

    public bool isScanBrain;
    //完成大脑扫描
    public void FinishScanBrain()
    {
        brainPlate.sprite = nobrain;
        isScanBrain = true;
        flowChart.ExecuteBlock("完成扫描大脑");
    }


    [Header("水管游戏")]
    public GameObject pipeGame;
    public bool isFinishPipeGame;
    public void OpenPipeGame()
    {
        if (isFinishPipeGame) return;
        player.Froze();
        pipeGame.SetActive(true);
    }

    public void FinishPipeGame()
    {
        isFinishPipeGame = true;

        if (isPutBrain) {
            flowChart.ExecuteBlock("Illusion_放置大脑");
        } else {
            flowChart.ExecuteBlock("Illusion_未放置大脑");
        }
    }

    #endregion

    #region 电脑
    [Header("电脑")]
    public GameObject computer;
    public GameObject[] brokenFile;
    public GameObject[] repairFile;
    public int filecnt;
    public GameObject analyseFileBtn;
    public GameObject cantanalyseFileBtn;

    public void OpenComputer()
    {
        computer.SetActive(true);
    }

    public void GetFile(int n)
    {
        isGetSceneItemInMaze = true;

        brokenFile[n].SetActive(false);
        repairFile[n].SetActive(true);
        filecnt++;

        //都获取到了
        if (filecnt == 4) {
            analyseFileBtn.SetActive(true);
            cantanalyseFileBtn.SetActive(false);
        }
    }
    [Header("文件分析")]
    public GameObject getWarReportText;
    public GameObject anaSoftware;
    public GameObject anaButtomIco;
    public Image analyseReport;
    public Scrollbar anaScrollbar;
    public Image analyseFillbar;
    public GameObject[] analyseThings;
    public Image unFillbar;
    public GameObject unAnim;
    public GameObject unText;
    public void AnalyseFiles()
    {
        StartCoroutine(nameof(Analysing));
    }

    private IEnumerator Analysing()
    {
        analyseReport.gameObject.SetActive(true);
        while (analyseFillbar.fillAmount < 1) {
            analyseFillbar.fillAmount += Time.deltaTime / 3;
            if (analyseFillbar.fillAmount < 0.2f) {
                analyseReport.fillAmount = 0.2f;
            } else if (analyseFillbar.fillAmount < 0.5f) {
                analyseReport.fillAmount = 0.5f;
            } else if (analyseFillbar.fillAmount < 0.7f) {
                analyseReport.fillAmount = 0.7f;
            } else {
                analyseReport.fillAmount = 1f;
                anaScrollbar.value = 0;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        analyseThings[0].SetActive(true);//警告底图标
        analyseThings[1].SetActive(true);//市民负面情绪比率
        yield return new WaitForSeconds(1);
        analyseThings[2].SetActive(true);//小警告
        yield return new WaitForSeconds(2);
        analyseThings[3].SetActive(true);//大警告
        yield return new WaitForSeconds(0.5f);
        analyseThings[4].SetActive(true);//暴動可能性彈窗
        yield return new WaitForSeconds(2);

        analyseThings[5].SetActive(true);//联合国
        while (unFillbar.fillAmount < 1) {
            unFillbar.fillAmount += Time.deltaTime / 5;
            yield return null;
        }
        unAnim.SetActive(false);
        unText.SetActive(true);
        yield return new WaitForSeconds(3);

        //anaoverlay全部关闭
        for (int i = analyseThings.Length - 1; i >= 1; i--) {
            analyseThings[i].SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
        analyseThings[0].SetActive(false);
        analyseThings[0].transform.parent.gameObject.SetActive(false);
        analyseReport.gameObject.SetActive(false);
        analyseFileBtn.GetComponent<Button>().interactable = false;//不可交互
        anaSoftware.SetActive(false);
        anaButtomIco.SetActive(false);

        //分析报告
        getWarReportText.SetActive(true);
        //item_dict.TryGetValue("战争分析报告", out Item it);
        //bag.AddItem(it);//背包数据更新
        //BagUI.Instance.Refresh_UseItem();


    }
    //[Header("战争分析报告")]
    //public GameObject warReport;
    //public bool OpenWarReport()
    //{
    //    player.Froze();
    //    warReport.SetActive(true);
    //    return true;
    //}
    #endregion

    #region 结局
    [Header("结局")]
    public bool isPassRobo;
    public GameObject EndDoor;
    public void ShowEndDoor()
    {
        isPassRobo = true;
        EndDoor.SetActive(true);
        robo.GetComponent<Collider2D>().enabled = false;
    }
    public GameObject input2079;
    public void OpenInput2079()
    {
        player.Froze();
        input2079.SetActive(true);
    }
    public GameObject endScene;
    public GameObject scene2071;
    public GameObject bagui;
    public void Right2079(string s)
    {
        if (s.Trim() != "2079") return;
        player.gameObject.SetActive(false);
        bagui.SetActive(false);
        scene2071.SetActive(false);

        endScene.SetActive(true);
        input2079.SetActive(false);
        flowChart.ExecuteBlock("大清洗");
    }

    [Header("结尾演出")]
    public Transform countDownTexts;
    public Transform afterVDownTexts;
    public void CountDown()
    {
        StartCoroutine(nameof(CountDown_Coroutine));
    }
    public IEnumerator CountDown_Coroutine()
    {
        countDownTexts.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < countDownTexts.childCount - 1; i++) {
            yield return new WaitForSeconds(1);
            countDownTexts.GetChild(i - 1).gameObject.SetActive(false);
            countDownTexts.GetChild(i).gameObject.SetActive(true);
        }
        //最后一张时，不关闭上一张
        yield return new WaitForSeconds(1);
        countDownTexts.GetChild(countDownTexts.childCount - 1).gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        flowChart.ExecuteBlock("大清洗_倒计时后");
    }
    public void AfterVDown()
    {
        StartCoroutine(nameof(AfterVDown_Coroutine));
    }
    public IEnumerator AfterVDown_Coroutine()
    {
        //上一段残留的关闭
        countDownTexts.GetChild(countDownTexts.childCount - 1).gameObject.SetActive(false);
        countDownTexts.GetChild(countDownTexts.childCount - 2).gameObject.SetActive(false);
        //新的开启
        afterVDownTexts.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        //上一张关闭，常驻的开启，百分比开启
        afterVDownTexts.GetChild(0).gameObject.SetActive(false);
        afterVDownTexts.GetChild(1).gameObject.SetActive(true);
        afterVDownTexts.GetChild(2).gameObject.SetActive(true);

        for (int i = 3; i < afterVDownTexts.childCount; i++) {
            yield return new WaitForSeconds(2);
            afterVDownTexts.GetChild(i - 1).gameObject.SetActive(false);
            afterVDownTexts.GetChild(i).gameObject.SetActive(true);

            //最后一句，常驻的关闭
            if (i == afterVDownTexts.childCount - 1) {
                afterVDownTexts.GetChild(1).gameObject.SetActive(false);//常驻的
            }
        }
        //afterVDownTexts.GetChild(afterVDownTexts.childCount - 1).gameObject.SetActive(false);
    }

    #endregion

    public void CloseGameObject(GameObject obj)
    {
        //关闭物体
        obj.SetActive(false);

        //如果迷宫开启返回
        for (int i = 0; i < mazes.Length; i++) {
            if (mazes[i].activeSelf) {
                return;
            }
        }

        //如果没有迷宫直接可以行走
        player.DeFroze();
    }
}
