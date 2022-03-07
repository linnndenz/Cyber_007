using BagDataManager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//物品hold都由此检测，再传递数据给Player
public class BagUI : MonoBehaviour
{
    public static BagUI Instance { get; protected set; }

    [Header("显示")]
    [SerializeField] private Transform chosenImg;
    [SerializeField] private Transform slotParent;
    private Slot[] slots;
    [SerializeField] private Transform bagPanel;
    [SerializeField] private Transform upPos;
    [SerializeField] private Transform downPos;

    [Header("数据")]
    private Bag bag;
    protected virtual void Start()
    {
        //数据初始化
        bag = Player.Instance.GetBag();
        slots = slotParent.GetComponentsInChildren<Slot>();
        ReFresh();
    }


    #region hold/use/pick
    public void ClickSlot(int index)
    {
        if (slots[index].IsChosen) {
            //调用bag的HoldItem函数
            Player.Instance.HoldItem(index);
            //重置slot的IsChosen
            for (int i = 0; i < slots.Length; i++) {
                if (i != index) slots[i].IsChosen = false;
            }
            //选择图标
            chosenImg.gameObject.SetActive(true);
            chosenImg.position = slots[index].transform.position;
        } else {
            Player.Instance.HoldItem(-1);

            for (int i = 0; i < slots.Length; i++) {
                slots[i].IsChosen = false;
            }

            chosenImg.gameObject.SetActive(false);
        }
    }
    //use，player调用
    public void Refresh_UseItem()
    {
        ReFresh();
    }
    //Pick，player调用
    public void Refresh_PickItem(int lashindex)
    {
        slots[lashindex].IsChosen = true;
        ClickSlot(lashindex);
        ReFresh();
    }
    #endregion

    #region 刷新
    public void ReFresh()
    {
        List<Item> items = bag.GetItemList();
        if(items.Count>slots.Length) print("持有物长度超过背包长度");
        for (int i = 0; i < slots.Length; i++) {
            if (i < items.Count) {
                slots[i].Ico.sprite = items[i].ico;
            } else {
                slots[i].Ico.sprite = null;
            }
        }
        //重置slot的IsChosen
        for (int i = 0; i < Instance.slots.Length; i++) {
            if (i != bag.HoldingItemIndex) Instance.slots[i].IsChosen = false;
        }
        if (bag.HoldingItemIndex == -1) {
            chosenImg.gameObject.SetActive(false);
        }
    }
    #endregion


    #region 背包收起放下
    bool isDown = true;
    public void Drawer()
    {
        isDown = !isDown;
        if (isDown) {
            BagDown();
        } else {
            BagUp();
        }
    }
    public static void BagDown()
    {
        Instance.bagPanel.DOMoveY(Instance.downPos.position.y, 0.3f);
    }
    public static void BagUp()
    {
        Instance.bagPanel.DOMoveY(Instance.upPos.position.y, 0.3f);
    }
    #endregion
}
