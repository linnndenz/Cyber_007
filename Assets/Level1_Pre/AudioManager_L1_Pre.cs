using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_L1_Pre : AudioManager
{
    protected override void InitSE()
    {
        ses = Resources.LoadAll<AudioClip>("Audios_L1Pre");
    }
}
