using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Player_L0 player;
    public GameObject startPanel;
    public GameObject startAnim;

    public Flowchart flowChart;
    public Image black;

    public void StartGame()
    {
        startPanel.SetActive(false);
        clinic.SetActive(true);
        //startAnim.SetActive(true);
        flowChart.ExecuteBlock("Start");
    }

    public void GoLevel1()
    {
        SceneManager.LoadScene("Scene_Level1_Pre");
    }

    public void Black()
    {
        print("1");
        black.gameObject.SetActive(true);
        black.color = new Color(0, 0, 0, 0);
        black.DOColor(new Color(0, 0, 0, 1), 2).OnComplete(GoLevel1);
    }

    #region ����
    [Header("����")]
    public GameObject newsUI;
    public GameObject newsNewIco;
    public void ReadNews()
    {
        newsUI.SetActive(true);
        newsNewIco.SetActive(false);
    }
    public void CloseNews()
    {
        newsUI.SetActive(false);
        player.DeFroze();
    }
    #endregion

    #region ޱޱ��
    [Header("ޱޱ��")]
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
                clinic.SetActive(false);
                black.DOColor(new Color(0, 0, 0, 0), 2).OnComplete(() => {
                    black.gameObject.SetActive(false);
                    flowChart.ExecuteBlock("׹��");
                });
            });
        });
    }

    #endregion
}
