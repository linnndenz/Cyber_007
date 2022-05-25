using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PipeButton : MonoBehaviour, IPointerClickHandler
{
    public int currIndex = 0;//��ǰת����ţ���ʼ�ɱ�
    public int[] correctIndexs = new int[] { 0 };//��ȷƴ�ϵ���ţ�ͨ������0��11�͵Ŀ���0��2

    PipeGame pipeGame;

    void Start()
    {
        pipeGame = GetComponentInParent<PipeGame>();
        //������ť
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        //����
        transform.Rotate(0, 0, -60 * currIndex);
    }

    //���
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
