using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PipeButton : MonoBehaviour, IPointerClickHandler
{
    public int currIndex = 0;//当前转动序号，初始可变
    public int[] correctIndexs = new int[] { 0 };//正确拼合的序号，通常的是0，11型的可以0和2

    PipeGame pipeGame;

    void Start()
    {
        pipeGame = GetComponentInParent<PipeGame>();
        //不规则按钮
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        //打乱
        transform.Rotate(0, 0, -60 * currIndex);
    }

    //点击
    public void OnPointerClick(PointerEventData eventData)
    {
        if (pipeGame.isFinish) {
            return;
        }

        transform.Rotate(0, 0, -60);
        currIndex = (currIndex + 1) % 6;
        pipeGame.CheckFinish();
    }
}
