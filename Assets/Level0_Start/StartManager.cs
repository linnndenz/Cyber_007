using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject startAnim;

    public Flowchart flowChart;
    public Image black;
    public void StartGame()
    {
        startPanel.SetActive(false);
        startAnim.SetActive(true);
        flowChart.ExecuteBlock("Start");
    }

    public void GoLevel1()
    {
        SceneManager.LoadScene("Scene_Level1_Pre");
    }

    public void Black()
    {
        black.gameObject.SetActive(true);
        black.color = new Color(0, 0, 0, 0);
        black.DOColor(new Color(0, 0, 0, 1), 2).OnComplete(GoLevel1);
    }
}
