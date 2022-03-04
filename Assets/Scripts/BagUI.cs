using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUI : MonoBehaviour
{
    public static BagUI instance;

    [Header("���")]
    public Transform slotParent;
    private Slot[] slots;
    public Transform chosenImg;
    public Player player;

    //����
    public int ChosenSlotIndex { get; private set; }

    protected virtual void Init()
    {
        //��������
        if (instance != null) Destroy(gameObject);
        instance = this;
        //���ݳ�ʼ��
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    public static void ClickSlot(int index)
    {
        if (instance.slots[index].IsChosen) {
            //����ѡ����Ʒindex
            instance.ChosenSlotIndex = index;
            //����slot��Index
            for (int i = 0; i < instance.slots.Length; i++) {
                if(i!=index) instance.slots[i].IsChosen = false;
            }
            //ѡ��ͼ��
            instance.chosenImg.gameObject.SetActive(true);
            instance.chosenImg.position = instance.slots[index].transform.position;
            //����bag��HoldItem����
            instance.player.bag.HoldItem(index);
        } else {
            instance.ChosenSlotIndex = -1;

            for (int i = 0; i < instance.slots.Length; i++) {
                instance.slots[i].IsChosen = false;
            }

            instance.chosenImg.gameObject.SetActive(false);
        }
    }

    public void ReFresh()
    {

    }
}
