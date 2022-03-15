using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBook : MonoBehaviour
{
    public GameObject[] pages;
    int index = 0;

    void OnEnable()
    {
        index = 0;
        pages[0].SetActive(true);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
    }
    public void TurnPage()
    {
        index = (index + 1) % 3;

        for (int i = 0; i < 3; i++) {
            if (index == i) pages[i].SetActive(true);
            else pages[i].SetActive(false);
        }
    }
}
