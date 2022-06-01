using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_L3 : AudioManager
{
    protected override void InitSE()
    {
        ses = Resources.LoadAll<AudioClip>("Audios_L3");
    }
}
