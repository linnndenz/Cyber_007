using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartVideo : MonoBehaviour
{
    public RawImage img;
    void Start()
    {
        img = GetComponent<RawImage>();
    }
    void Update()
    {
        if (Input.anyKeyDown) {
            img.DOColor(new Color(0, 0, 0, 0), 1f);
            transform.DOScale(new Vector3(1.5f, 1.5f, 1), 1f).OnComplete(() => { 
                gameObject.SetActive(false);
            });
        }
    }
}
