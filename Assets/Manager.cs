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

        bgms = new AudioClip[2];
        bgms[0] = Resources.Load<AudioClip>("BGM/0BGM_��԰");
        bgms[1] = Resources.Load<AudioClip>("BGM/1BGM_ҽԺ���������");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void ChangeBGM(int index)
    {
        if (index == -1) {
            bgmAudio.clip = null;
        } else if (index >= bgms.Length) {
            print("bgm��������");
            return;
        } else {
            bgmAudio.clip = bgms[index];
            bgmAudio.Play();
        }
    }


}
