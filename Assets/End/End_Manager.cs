using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class End_Manager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.loopPointReached += ToStart;
    }

    private void ToStart(VideoPlayer video)
    {
        Manager.ChangeBGM(-1);
        SceneManager.LoadScene("Scene_Start");
    }
}
