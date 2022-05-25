using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHat : MonoBehaviour
{
    public GameObject[] hats;
    public GameObject[] chosenHats;
    int currChosen = -1;
    public Flowchart flowChart;
    public void Choose(int index)
    {
        currChosen = index;
        for (int i = 0; i < 3; i++) {
            if (i != index) {
                hats[i].SetActive(true);
                chosenHats[i].SetActive(false);
            } else {
                hats[i].SetActive(false);
                chosenHats[i].SetActive(true);
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
