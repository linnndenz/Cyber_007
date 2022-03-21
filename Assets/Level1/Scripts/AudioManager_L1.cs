using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_L1 : AudioManager
{
    protected override void InitSE()
    {
        ses = new AudioClip[9];
        ses[0] = Resources.Load<AudioClip>("Audios_L1/0翻页");
        ses[1] = Resources.Load<AudioClip>("Audios_L1/1草莓浇水");
        ses[2] = Resources.Load<AudioClip>("Audios_L1/2敲鸡蛋");
        ses[3] = Resources.Load<AudioClip>("Audios_L1/3开门");
        ses[4] = Resources.Load<AudioClip>("Audios_L1/4无法开门");
        ses[5] = Resources.Load<AudioClip>("Audios_L1/5院长办公室未获得钥匙时");
        ses[6] = Resources.Load<AudioClip>("Audios_L1/6密码盒开启");
        ses[7] = Resources.Load<AudioClip>("Audios_L1/7密码盒选择颜色");
        ses[8] = Resources.Load<AudioClip>("Audios_L1/8撞车");
    }
}
