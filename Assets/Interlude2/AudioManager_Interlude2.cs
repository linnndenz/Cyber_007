using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_Interlude2 : AudioManager
{
    protected override void InitSE()
    {
        ses = Resources.LoadAll<AudioClip>("Audios_Interlude2");

    }


}

