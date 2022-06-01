using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_Start : AudioManager
{
    protected override void InitSE()
    {
        ses = Resources.LoadAll<AudioClip>("Audios_Start");
    }
}
