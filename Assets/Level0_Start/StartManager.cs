using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public AudioManager audioManager;
    public Player_L0 player;
    public GameObject startPanel;
    public GameObject startAnim;

    public Flowchart flowChart;
    public Image black;

    private void Start()
    {
        player.Froze();
    }

    [Header("存档")]
    public Button[] saves;
    //存档初始加载
    public void Init()
    {
        //存档到哪一关
        if (SaveData.Instance.passedLevel == 1) {
            saves[0].interactable = true;
        } else if (SaveData.Instance.passedLevel == 2) {
            saves[0].interactable = true;
            saves[1].interactable = true;
        } else if (SaveData.Instance.passedLevel == 3) {
            saves[0].interactable = true;
            saves[2].interactable = true;
            saves[3].interactable = true;
        }
    }
    public void LoadSave(int i)
    {
        if(i == 1) {
            SceneManager.LoadScene("Interlude2");
        }else if(i == 2) {
            SceneManager.LoadScene("Interlude3");
        } else {
            SceneManager.LoadScene("Scene_End");
        }
    }

    [Header("开始游戏")]
    public Transform titlePosition;
    public void StartGame()
    {
        clinic.transform.DOScale(Vector3.one, 1.5f);
        clinic.transform.DOMove(Vector3.zero, 1.5f);
        startPanel.transform.DOScale(titlePosition.localScale, 1.5f);
        startPanel.transform.DOMove(titlePosition.position, 1.5f).OnComplete(() => {
            startPanel.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() => startPanel.SetActive(false));
            flowChart.ExecuteBlock("Start");
        });

        //clinic.SetActive(true);
        //startAnim.SetActive(true);
        Manager.ChangeBGM(5);
    }
    //退出游戏
    public void QuitGame()
    {
        SaveData.Instance.SaveGame();//先存档后退出
        Application.Quit();
    }

    public IEnumerator GoLevel1()
    {
        audioManager.PlaySE(0);
        Manager.ChangeBGM(-1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Scene_Level1_Pre");
    }

    public void Black()
    {
        black.color = new Color(0, 0, 0, 1);
        black.gameObject.SetActive(true);
        StartCoroutine(GoLevel1());
    }

    #region 新闻
    [Header("新闻")]
    public GameObject newsUI;
    public GameObject newsNewIco;
    public void ReadNews()
    {
        audioManager.PlaySE(2);
        newsUI.SetActive(true);
        newsNewIco.SetActive(false);
    }
    public void CloseNews()
    {
        newsUI.SetActive(false);
        player.DeFroze();
    }
    #endregion

    #region 薇薇安
    [Header("薇薇安")]
    public GameObject vva;
    public Transform illusionPos;
    public GameObject clinic;

    public void VvaCome()
    {
        StartCoroutine(VvaComeCoroutine());
    }
    IEnumerator VvaComeCoroutine()
    {
        player.Froze();
        yield return new WaitForSeconds(1.5f);
        vva.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        flowChart.ExecuteBlock("VVA");
    }

    public void GotoIllusion()
    {
        vva.GetComponentInChildren<Animator>().SetBool("xMove", true);
        vva.GetComponent<AudioSource>().Play();

        vva.transform.DOMoveX(illusionPos.position.x, 5f).OnComplete(() => {
            vva.GetComponentInChildren<Animator>().SetBool("xMove", false);
            vva.GetComponent<AudioSource>().Stop();

            black.gameObject.SetActive(true);
            black.color = new Color(0, 0, 0, 0);
            black.DOColor(new Color(0, 0, 0, 1), 2).OnComplete(() => {
                startAnim.SetActive(true);
                Manager.ChangeBGM(4);
                clinic.SetActive(false);
                black.DOColor(new Color(0, 0, 0, 0), 2).OnComplete(() => {
                    black.gameObject.SetActive(false);
                    flowChart.ExecuteBlock("坠落");
                });
            });
        });
    }

    #endregion
}
