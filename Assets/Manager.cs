using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    private AudioClip[] bgms;
    private AudioSource bgmAudio;


    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        bgmAudio = GetComponent<AudioSource>();

        //bgms = new AudioClip[2];
        bgms = Resources.LoadAll<AudioClip>("BGM");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl)&&Input.GetKey(KeyCode.C)) {
            Application.Quit();
        }
    }

    public static void ChangeBGM(int index)
    {
        if (index == -1) {
            Instance.bgmAudio.clip = null;
        } else if (index >= Instance.bgms.Length) {
            print("bgm超出数组");
            return;
        } else {
            if(Instance.bgmAudio.clip == Instance.bgms[index]) {
                print("播放了相同的bgm");
                return;
            }
            Instance.bgmAudio.clip = Instance.bgms[index];
            Instance.bgmAudio.Play();
        }
    }


}
