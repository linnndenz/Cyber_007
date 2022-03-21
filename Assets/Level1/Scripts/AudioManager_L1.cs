using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_L1 : AudioManager
{
    protected override void InitSE()
    {
        ses = new AudioClip[9];
        ses[0] = Resources.Load<AudioClip>("Audios_L1/0��ҳ");
        ses[1] = Resources.Load<AudioClip>("Audios_L1/1��ݮ��ˮ");
        ses[2] = Resources.Load<AudioClip>("Audios_L1/2�ü���");
        ses[3] = Resources.Load<AudioClip>("Audios_L1/3����");
        ses[4] = Resources.Load<AudioClip>("Audios_L1/4�޷�����");
        ses[5] = Resources.Load<AudioClip>("Audios_L1/5Ժ���칫��δ���Կ��ʱ");
        ses[6] = Resources.Load<AudioClip>("Audios_L1/6����п���");
        ses[7] = Resources.Load<AudioClip>("Audios_L1/7�����ѡ����ɫ");
        ses[8] = Resources.Load<AudioClip>("Audios_L1/8ײ��");
    }
}
