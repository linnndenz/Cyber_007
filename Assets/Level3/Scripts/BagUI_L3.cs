using BagDataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUI_L3 : BagUI
{
    protected override void Awake()
    {
        //��������
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    protected override void Start()
    {
        //���ݳ�ʼ��
        bag = LevelManager.GetBag();
        slots = slotParent.GetComponentsInChildren<Slot>();
        ReFresh();
    }

    public override void ClickSlot(int index)
    {
        //�Ķ�����Ʒ
        if (index >= 0 && index < bag.GetItemList().Count && bag.GetItemList()[index].isRead) {
            bag.GetItemList()[index].Read();
        } else {
            if (index >= 0 && slots[index].IsChosen) {
                //����bag��HoldItem����
                Player.Instance.HoldItem(index);
                //����slot��IsChosen
                for (int i = 0; i < slots.Length; i++) {
                    if (i != index) {
                        slots[i].IsChosen = false;
                        slots[i].GetComponent<Image>().color = Color.white;
                    } else {
                        slots[i].GetComponent<Image>().color = chosenColor;
                    }
                }

            } else {
                Player.Instance.HoldItem(-1);

                for (int i = 0; i < slots.Length; i++) {
                    slots[i].IsChosen = false;
                    slots[i].GetComponent<Image>().color = Color.white;
                }

            }
        }
    }

   
}
