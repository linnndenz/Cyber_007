using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioManager : MonoBehaviour
{

    [Header("音效片段")]
    protected AudioClip[] ses;

    //播放器
    private AudioSource[] sePlayers;//音效播放器，数量固定，循环使用

    private int sePlayerIndex = 0;//音效播放器序号

    //进行一些资源加载和初始化
    protected virtual void Start()
    {
        sePlayers = new AudioSource[3];
        for (int i = 0; i < sePlayers.Length; i++) {
            sePlayers[i] = gameObject.AddComponent<AudioSource>();
            //sePlayers[i].volume = instance.seVolume;
        }

        InitSE();
       
    }

    //se加载，子类重写
    //ses = new AudioClip[3];
    //ses[0] = Resources.Load<AudioClip>("Audios/0Click");
    protected abstract void InitSE();

    //音效播放
    public void PlaySE(int index)
    {
        if (ses == null || index < 0 || index >= ses.Length) return;

        print("播放音效" + index.ToString());
        sePlayers[sePlayerIndex].clip = ses[index];
        sePlayers[sePlayerIndex].Play();

        sePlayerIndex = (sePlayerIndex + 1) % sePlayers.Length;
    }
    public void PlaySE(AudioClip clip)
    {
        if (clip == null) return;

        print("播放音效" + clip);
        sePlayers[sePlayerIndex].clip = clip;
        sePlayers[sePlayerIndex].Play();

        sePlayerIndex = (sePlayerIndex + 1) % sePlayers.Length;
    }

}

