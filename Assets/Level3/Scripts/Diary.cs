using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    public Sprite[] sprites;
    public Image img;
    int cnt = 0;

  
    public void NextDiary()
    {
        cnt = (cnt + 1) % sprites.Length;
        img.sprite = sprites[cnt];
    }
}
