using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager_L2 : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene("Interlude3");
    }
}
