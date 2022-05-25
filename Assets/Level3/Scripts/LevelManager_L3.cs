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
        item_dict.Add("�ǼǱ�", new Item("�ǼǱ�", false, Resources.Load<Sprite>("Texture_L3/�ǼǱ�"), null, null, null, true, OpenTable));
        item_dict.Add("�ռ��ϰ�", new Item("�ռ��ϰ�", false, Resources.Load<Sprite>("Texture_L3/�ռ��ϰ�"), null, null, null, true, OpenDiary1));
        item_dict.Add("�ռ��°�", new Item("�ռ��°�", false, Resources.Load<Sprite>("Texture_L3/�ռ��°�"), null, null, null, true, OpenDiary2));
        item_dict.Add("����", new Item("����", true, Resources.Load<Sprite>("Texture_L3/����"), null, UseBrain, null));
        //item_dict.Add("ս����������", new Item("ս����������", true, Resources.Load<Sprite>("Texture_L3/ս����������"), null, null, OpenWarReport));
    }

    //�Թ������Ʒ
    public void GetItemInMaze(string nname)
    {
        item_dict.TryGetValue(nname, out Item it);
        bag.AddItem(it);//�������ݸ���
        BagUI.Instance.Refresh_UseItem();
    }

    #region ����ǰ
    [Header("����ǰ")]
    public GameObject queuePeople;
    [HideInInspector] public bool isGetQueue;//����Ŷ��ˣ�
    public GameObject vTable;
    [HideInInspector] public bool isGetTable;//��õǼǱ�

    //�Թ�����Ŷ���
    public void GetQueue()
    {
        isGetSceneItemInMaze = true;
        isGetQueue = true;
        queuePeople.SetActive(true);
    }

    //��õǼǱ�
    public void GetTable()
    {
        isGetBagItemInMaze = true;
        GetItemInMaze("�ǼǱ�");
        isGetTable = true;

    }

    //�򿪵ǼǱ�
    public bool OpenTable()
    {
        //����Ĺر�
        diary1.SetActive(false);
        diary2.SetActive(false);

        player.Froze();
        vTable.SetActive(true);
        return true;
    }
    #endregion

    #region �ռ�
    [Header("�ռ�")]
    public GameObject diary1;
    public GameObject diary2;
    public bool isGetDiary1;//����ռ��ϰ룿
    public bool isGetDiary2;//����ռ��°룿

    public void GetDiary1()
    {
        isGetBagItemInMaze = true;
        GetItemInMaze("�ռ��ϰ�");
        isGetDiary1 = true;
    }
    public void GetDiary2()
    {
        isGetBagItemInMaze = true;
        GetItemInMaze("�ռ��°�");
        isGetDiary2 = true;
    }

    public bool OpenDiary1()
    {
        //����Ĺر�
        vTable.SetActive(false);
        diary2.SetActive(false);

        player.Froze();
        diary1.SetActive(true);
        return true;
    }

    public bool OpenDiary2()
    {
        //����Ĺر�
        vTable.SetActive(false);
        diary1.SetActive(false);

        player.Froze();
        diary2.SetActive(true);
        return true;
    }


    #endregion

    #region �������󹤣�
    [Header("����/��")]
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

    #region �Թ�
    [Header("�Թ�")]
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
        flowChart.ExecuteBlock("�Թ�");
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
            flowChart.ExecuteBlock("�Թ�_�����");
        } else if (isGetBagItemInMaze) {
            flowChart.ExecuteBlock("�Թ�_��ñ�����Ʒ");
        } else if (isGetSceneItemInMaze) {
            flowChart.ExecuteBlock("�Թ�_��ó�����Ʒ");
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
            player.Froze();//���Ե��Թ����Ի�����froze״̬
        }
        print(mazeIndex);
    }
    //�Թ�ͨ�ؼ���
    [Header("�Թ�ͨ�����")]
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
                PassMaze();//ͨ��
                CloseMaze();//�ر��Թ�
                yearPages[mazeIndex].gameObject.SetActive(false);//�ر����ҳ
                //����ͨ�����������Թ�
                if (mazeIndex != 2) {
                    mazeInScene[mazeIndex].GetComponent<Collider2D>().enabled = false;
                } else {
                    mazeInScene[2].GetComponent<Button>().enabled = false;
                }
            }
        } catch { print("�����������"); }

    }

    #endregion

    #region ���������Խ�ˮ�ܣ�
    [Header("����")]
    public bool isPutBrain;
    public SpriteRenderer brainPlate;
    public Sprite nobrain;
    public Sprite brain;
    public bool UseBrain(string toname)
    {
        if (toname != "������") return false;
        brainPlate.sprite = brain;
        isPutBrain = true;
        return true;
    }

    public bool isScanBrain;
    //��ɴ���ɨ��
    public void FinishScanBrain()
    {
        brainPlate.sprite = nobrain;
        isScanBrain = true;
        flowChart.ExecuteBlock("���ɨ�����");
    }


    [Header("ˮ����Ϸ")]
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
            flowChart.ExecuteBlock("Illusion_���ô���");
        } else {
            flowChart.ExecuteBlock("Illusion_δ���ô���");
        }
    }

    #endregion

    #region ����
    [Header("����")]
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

        //����ȡ����
        if (filecnt == 4) {
            analyseFileBtn.SetActive(true);
            cantanalyseFileBtn.SetActive(false);
        }
    }
    [Header("�ļ�����")]
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
        analyseThings[0].SetActive(true);//�����ͼ��
        analyseThings[1].SetActive(true);//��������������
        yield return new WaitForSeconds(1);
        analyseThings[2].SetActive(true);//С����
        yield return new WaitForSeconds(2);
        analyseThings[3].SetActive(true);//�󾯸�
        yield return new WaitForSeconds(0.5f);
        analyseThings[4].SetActive(true);//���ӿ����ԏ���
        yield return new WaitForSeconds(2);

        analyseThings[5].SetActive(true);//���Ϲ�
        while (unFillbar.fillAmount < 1) {
            unFillbar.fillAmount += Time.deltaTime / 5;
            yield return null;
        }
        unAnim.SetActive(false);
        unText.SetActive(true);
        yield return new WaitForSeconds(3);

        //anaoverlayȫ���ر�
        for (int i = analyseThings.Length - 1; i >= 1; i--) {
            analyseThings[i].SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
        analyseThings[0].SetActive(false);
        analyseThings[0].transform.parent.gameObject.SetActive(false);
        analyseReport.gameObject.SetActive(false);
        analyseFileBtn.GetComponent<Button>().interactable = false;//���ɽ���
        anaSoftware.SetActive(false);
        anaButtomIco.SetActive(false);

        //��������
        getWarReportText.SetActive(true);
        //item_dict.TryGetValue("ս����������", out Item it);
        //bag.AddItem(it);//�������ݸ���
        //BagUI.Instance.Refresh_UseItem();


    }
    //[Header("ս����������")]
    //public GameObject warReport;
    //public bool OpenWarReport()
    //{
    //    player.Froze();
    //    warReport.SetActive(true);
    //    return true;
    //}
    #endregion

    #region ���
    [Header("���")]
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
        flowChart.ExecuteBlock("����ϴ");
    }

    [Header("��β�ݳ�")]
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
        //���һ��ʱ�����ر���һ��
        yield return new WaitForSeconds(1);
        countDownTexts.GetChild(countDownTexts.childCount - 1).gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        flowChart.ExecuteBlock("����ϴ_����ʱ��");
    }
    public void AfterVDown()
    {
        StartCoroutine(nameof(AfterVDown_Coroutine));
    }
    public IEnumerator AfterVDown_Coroutine()
    {
        //��һ�β����Ĺر�
        countDownTexts.GetChild(countDownTexts.childCount - 1).gameObject.SetActive(false);
        countDownTexts.GetChild(countDownTexts.childCount - 2).gameObject.SetActive(false);
        //�µĿ���
        afterVDownTexts.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        //��һ�Źرգ���פ�Ŀ������ٷֱȿ���
        afterVDownTexts.GetChild(0).gameObject.SetActive(false);
        afterVDownTexts.GetChild(1).gameObject.SetActive(true);
        afterVDownTexts.GetChild(2).gameObject.SetActive(true);

        for (int i = 3; i < afterVDownTexts.childCount; i++) {
            yield return new WaitForSeconds(2);
            afterVDownTexts.GetChild(i - 1).gameObject.SetActive(false);
            afterVDownTexts.GetChild(i).gameObject.SetActive(true);

            //���һ�䣬��פ�Ĺر�
            if (i == afterVDownTexts.childCount - 1) {
                afterVDownTexts.GetChild(1).gameObject.SetActive(false);//��פ��
            }
        }
        //afterVDownTexts.GetChild(afterVDownTexts.childCount - 1).gameObject.SetActive(false);
    }

    #endregion

    public void CloseGameObject(GameObject obj)
    {
        //�ر�����
        obj.SetActive(false);

        //����Թ���������
        for (int i = 0; i < mazes.Length; i++) {
            if (mazes[i].activeSelf) {
                return;
            }
        }

        //���û���Թ�ֱ�ӿ�������
        player.DeFroze();
    }
}
