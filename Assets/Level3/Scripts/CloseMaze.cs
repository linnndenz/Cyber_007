using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseMaze : MonoBehaviour
{
    public int mazeIndex;
    public LevelManager_L3 levelManager;
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            levelManager.CloseMaze();
        } else {
            print("µã»÷ÔÚuiÉÏ");
        }
    }
}
