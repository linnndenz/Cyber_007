using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_L1 : AudioManager
{
    protected override void InitSE()
    {
        ses = new AudioClip[10];
        ses = Resources.LoadAll<AudioClip>("Audios_L1");
    }
}
