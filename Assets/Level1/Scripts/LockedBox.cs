using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LockedBox : MonoBehaviour
{
    public LevelManager_L1 levelManager;
    public Sprite[] sprites;
    private int[] btnColors = { 0, 0, 0 };
    public Flowchart flowChart;


    public bool bOpen = false;

    public void ChangeColor(Image img)
    {
        if (bOpen) return;
        
        levelManager.audioManager.PlaySE(6);

        int index = img.transform.GetSiblingIndex();
        btnColors[index] = (btnColors[index] + 1) % sprites.Length;
        img.sprite = sprites[btnColors[index]];

        if (btnColors[0] == 0 && btnColors[1] == 1 && btnColors[2] == 2) {
            levelManager.audioManager.PlaySE(7);
            bOpen = true;
            levelManager.GetGateKey();
            levelManager.GetPhoto();
            flowChart.ExecuteBlock("ROBO_ÃÜÂëºÐ´ò¿ª");
        }
    }

    public void CloseIt()
    {
        levelManager.player.DeFroze();
        gameObject.SetActive(false);
    }
}
