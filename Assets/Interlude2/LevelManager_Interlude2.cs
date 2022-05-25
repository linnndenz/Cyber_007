using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager_Interlude2 : MonoBehaviour
{
    public Player_Interlude2 player;
    public Flowchart flowChart;

    #region 薇薇安
    [Header("薇薇安")]
    public Transform vva;
    public Transform doorPos;
    public void VvaGoOutside()
    {
        vva.GetComponent<AudioSource>().Play();
        vva.GetComponentInChildren<Animator>().SetBool("xMove", true);
        vva.DOMoveX(doorPos.position.x, 5).OnComplete(() => {
            vva.GetComponent<AudioSource>().Stop();
            vva.GetComponentInChildren<Animator>().SetBool("xMove", false);
            StartCoroutine(VVA_Delay());
        });
    }
    IEnumerator VVA_Delay()
    {
        yield return new WaitForSeconds(1);
        vva.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        flowChart.ExecuteBlock("VVA3");
    }
    #endregion

    #region 相片
    [Header("相片")]
    public GameObject photoGame;
    public void OpenPhotoGame()
    {
        photoGame.SetActive(true);
    }
    #endregion

    #region 维克多
    [Header("维克多")]
    public Transform victor;
    public Transform illusionPos;
    public void VictorIn()
    {
        victor.gameObject.SetActive(true);
        flowChart.ExecuteBlock("维克多");
    }

    public void VictorGotoIllusion()
    {
        victor.GetComponent<AudioSource>().Play();
        victor.GetComponentInChildren<Animator>().SetBool("xMove", true);
        victor.DOMoveX(illusionPos.position.x, 5f).OnComplete(() => {
            victor.GetComponent<AudioSource>().Stop();
            victor.GetComponentInChildren<Animator>().SetBool("xMove", false);
            flowChart.ExecuteBlock("维克多2");
        });
    }

    #endregion

    public Image black;
    public void GoLevel2()
    {
        black.color = new Color(0, 0, 0, 0);
        black.gameObject.SetActive(true);
        black.DOColor(new Color(0, 0, 0, 1), 2).OnComplete(() => {
            SceneManager.LoadScene("Interlude3");
        });
    }
}
