using BagDataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ٶ���Ʒ����������UI���������Ʒ��ȡ��hold��ʹ�ö��ɴ˼�⣬�ٴ������ݸ�Player
public class BagUI : MonoBehaviour
{
    public static BagUI instance;

    [Header("���")]
    public Transform slotParent;
    private Slot[] slots;
    public Transform chosenImg;
    public Player player;

    protected virtual void Init()
    {
        //���ݳ�ʼ��
        slots = slotParent.GetComponentsInChildren<Slot>();
        ReFresh();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player.IsInPickItem) {
            ClickEToPickItem();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            ClickRToUseItem();
        }

    }

    //hold
    public static void ClickSlot(int index)
    {
        if (instance.slots[index].IsChosen) {
            //����bag��HoldItem����
            instance.player.HoldItem(index);
            //����slot��Index
            for (int i = 0; i < instance.slots.Length; i++) {
                if (i != index) instance.slots[i].IsChosen = false;
            }
            //ѡ��ͼ��
            instance.chosenImg.gameObject.SetActive(true);
            instance.chosenImg.position = instance.slots[index].transform.position;
        } else {
            instance.player.HoldItem(-1);

            for (int i = 0; i < instance.slots.Length; i++) {
                instance.slots[i].IsChosen = false;
            }

            instance.chosenImg.gameObject.SetActive(false);
        }
    }
    //use
    public void ClickRToUseItem()
    {
        player.UseItem();
        ReFresh();
    }
    //Pick
    public void ClickEToPickItem()
    {
        player.PickItem();
        int index = player.GetBag().GetItemList().Count - 1;
        slots[index].IsChosen = true;
        ClickSlot(index);
        ReFresh();
    }

    public void ReFresh()
    {
        List<Item> items = player.GetBag().GetItemList();
        if(items.Count>slots.Length) print("�����ﳤ�ȳ�����������");
        for (int i = 0; i < slots.Length; i++) {
            if (i < items.Count) {
                slots[i].Ico.sprite = items[i].ico;
            } else {
                slots[i].Ico.sprite = null;
            }
        }
        if(player.GetBag().HoldingItemIndex == -1) {
            chosenImg.gameObject.SetActive(false);
        }
    }
}
