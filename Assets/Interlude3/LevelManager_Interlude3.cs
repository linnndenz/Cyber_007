using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager_Interlude3 : MonoBehaviour
{
    public AudioManager audioManager;
    void Start()
    {
        Manager.ChangeBGM(5);
    }

    public Flowchart flowChart;
    public Player_Interlude3 player;

    [Header("维克多")]
    public Transform victor;
    public Transform doorPos;
    public void VictorLeave()
    {
        victor.GetComponentInChildren<Animator>().SetBool("xMove", true);
        victor.GetComponent<AudioSource>().Play();
        victor.DOMoveX(doorPos.position.x, 5f).OnComplete(() => {
            victor.GetComponent<AudioSource>().Stop();
            victor.GetComponentInChildren<Animator>().SetBool("xMove", false);
            StartCoroutine(vic());
        });

        IEnumerator vic()
        {
            yield return new WaitForSeconds(2);
            victor.gameObject.SetActive(false);
            yield return new WaitForSeconds(2);
            flowChart.ExecuteBlock("维克多离开后");
        }
    }

    [Header("选择证据")]
    public GameObject chooseLEvidence;
    public void OpenChooseEvidence()
    {
        chooseLEvidence.SetActive(true);
    }

    [Header("ROBO")]
    public Transform robo;
    public void ROBOGotoIllusion()
    {
        robo.localScale = new Vector3(-1, 1, 1);
        player.DeFroze();
    }

    public Image black;
    public void Black()
    {
        black.color = new Color(0, 0, 0, 0);
        black.gameObject.SetActive(true);
        black.DOColor(new Color(0, 0, 0, 1), 2f).OnComplete(() => {
            SceneManager.LoadScene("Scene_Level3");
        });
    }

}
