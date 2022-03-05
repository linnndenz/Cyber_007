using BagDataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//假定物品交互都经由UI发起，因此物品获取、hold、使用都由此检测，再传递数据给Player
public class BagUI : MonoBehaviour
{
    public static BagUI instance;

    [Header("面板")]
    public Transform slotParent;
    private Slot[] slots;
    public Transform chosenImg;
    public Player player;

    protected virtual void Init()
    {
        //数据初始化
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
            //调用bag的HoldItem函数
            instance.player.HoldItem(index);
            //重置slot的Index
            for (int i = 0; i < instance.slots.Length; i++) {
                if (i != index) instance.slots[i].IsChosen = false;
            }
            //选择图标
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
        if(items.Count>slots.Length) print("持有物长度超过背包长度");
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
