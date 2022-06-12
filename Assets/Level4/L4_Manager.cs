using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L4_Manager : MonoBehaviour
{
   public void ToEnd()
    {
        SceneManager.LoadScene("Scene_End");
    }
}
