using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseHat : MonoBehaviour
{
    public GameObject[] hats;
    int currChosen = -1;
    public Flowchart flowChart;
    public void Choose(int index)
    {
        currChosen = index;
        for (int i = 0; i < 3; i++) {
            if (i != index) {
                hats[i].SetActive(true);
            } else {
                hats[i].SetActive(false);
            }
        }
    }
    public void Bell()
    {
        if(currChosen == -1) {
            return;
        } else {
            flowChart.ExecuteBlock("Ñ¡ÔñÃ±×Óºó");
        }
    }
}
