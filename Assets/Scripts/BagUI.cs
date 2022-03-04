using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUI : MonoBehaviour
{
    public static BagUI instance;

    [Header("面板")]
    public Transform slotParent;
    private Slot[] slots;
    public Transform chosenImg;
    public Player player;

    //数据
    public int ChosenSlotIndex { get; private set; }

    protected virtual void Init()
    {
        //创建单例
        if (instance != null) Destroy(gameObject);
        instance = this;
        //数据初始化
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    public static void ClickSlot(int index)
    {
        if (instance.slots[index].IsChosen) {
            //设置选中物品index
            instance.ChosenSlotIndex = index;
            //重置slot的Index
            for (int i = 0; i < instance.slots.Length; i++) {
                if(i!=index) instance.slots[i].IsChosen = false;
            }
            //选择图标
            instance.chosenImg.gameObject.SetActive(true);
            instance.chosenImg.position = instance.slots[index].transform.position;
            //调用bag的HoldItem函数
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
