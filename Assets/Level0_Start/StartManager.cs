using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject startAnim;
    public void StartGame()
    {
        canvas.SetActive(false);
        startAnim.SetActive(true);
    }

    public void GoLevel1()
    {
        SceneManager.LoadScene("Scene_Level1");
    }
}
