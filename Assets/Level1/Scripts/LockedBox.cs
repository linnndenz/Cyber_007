using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LockedBox : MonoBehaviour
{
    public LevelManager_L1 levelManager;
    public Color[] colors;
    private int[] btnColors = { 0, 0, 0 };

    public bool bOpen = false;

    public void ChangeColor(Image img)
    {
        if (bOpen) return;

        int index = img.transform.GetSiblingIndex();
        btnColors[index] = (btnColors[index] + 1) % colors.Length;
        img.color = colors[btnColors[index]];

        if (btnColors[0] == 0 && btnColors[1] == 4 && btnColors[2] == 3) {
            bOpen = true;
            levelManager.GetGateKey();
            Invoke(nameof(CloseIt), 1f);
        }
    }

    void CloseIt()
    {
        levelManager.player.DeFroze();
        gameObject.SetActive(false);
    }
}
