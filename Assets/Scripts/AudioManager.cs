using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioManager : MonoBehaviour
{

    [Header("��ЧƬ��")]
    protected AudioClip[] ses;

    //������
    private AudioSource[] sePlayers;//��Ч�������������̶���ѭ��ʹ��

    private int sePlayerIndex = 0;//��Ч���������

    //����һЩ��Դ���غͳ�ʼ��
    protected virtual void Start()
    {
        sePlayers = new AudioSource[3];
        for (int i = 0; i < sePlayers.Length; i++) {
            sePlayers[i] = gameObject.AddComponent<AudioSource>();
            //sePlayers[i].volume = instance.seVolume;
        }

        InitSE();
       
    }

    //se���أ�������д
    //ses = new AudioClip[3];
    //ses[0] = Resources.Load<AudioClip>("Audios/0Click");
    protected abstract void InitSE();

    //��Ч����
    public void PlaySE(int index)
    {
        if (ses == null || index < 0 || index >= ses.Length) return;

        print("������Ч" + index.ToString());
        sePlayers[sePlayerIndex].clip = ses[index];
        sePlayers[sePlayerIndex].Play();

        sePlayerIndex = (sePlayerIndex + 1) % sePlayers.Length;
    }
    public void PlaySE(AudioClip clip)
    {
        if (clip == null) return;

        print("������Ч" + clip);
        sePlayers[sePlayerIndex].clip = clip;
        sePlayers[sePlayerIndex].Play();

        sePlayerIndex = (sePlayerIndex + 1) % sePlayers.Length;
    }

}

