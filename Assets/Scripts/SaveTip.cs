using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTip : MonoBehaviour
{
    CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).OnComplete(() => {
            canvasGroup.DOFade(0, 0.5f).OnComplete(() => {
                gameObject.SetActive(false);
            }).SetDelay(1.5f);
        });
    }

   
}
