using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseEvidence : MonoBehaviour
{
    public Flowchart flowChart;
    public Color chosenColor;
    public List<Image> btns;
    List<bool> res;


    void OnEnable()
    {
        res = new List<bool>() { false, false, false, false };
        for (int i = 0; i < btns.Count; i++) {
            btns[i].color = Color.white;
        }
    }


    public void Choose(int index)
    {
        res[index] = !res[index];
        if (res[index]) {
            btns[index].color = chosenColor;
        } else {
            btns[index].color = Color.white;
        }
    }

    public void CheckRes()
    {
        if (res[0] && !res[1] && res[2] && !res[3]) {
            flowChart.ExecuteBlock("选择正确");
        } else {
            flowChart.ExecuteBlock("选择错误");
        }
        gameObject.SetActive(false);
    }
}
