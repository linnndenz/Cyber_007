using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuckUChina : MonoBehaviour
{
    InputField input;

    private void Start()
    {
        input = GetComponent<InputField>();
    }

    public void FuckYou(string fuck)
    {
        try {
            int.Parse(fuck);
        } catch (FormatException) {
            input.text = string.Empty;
            print("�������ɵ�ƹ�������ƶ�");
        }
    }
}