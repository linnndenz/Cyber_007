using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagDataManager
{
    public class Bag
    {
        private List<Item> item_list = new List<Item>();

        public int HoldingItemIndex { get; private set; } = -1;

        public void AddItem(Item item)
        {
            BagUI.Instance.outline.SetActive(true);
            item_list.Add(item);
            item.Get?.Invoke();
        }
        public void RemoveItem(Item item)
        {
            item_list.Remove(item);
        }

        public void HoldItem(int index)
        {
            if (index < 0) {
                HoldingItemIndex = index;
                return;
            }
            if (index >= item_list.Count) return;

            HoldingItemIndex = index;
            if (item_list[index].Hold != null) item_list[index].Hold();
        }

        public void UseItem(string toname)
        {
            if (HoldingItemIndex >= item_list.Count || HoldingItemIndex < 0) return;

            if (item_list[HoldingItemIndex].Use != null && item_list[HoldingItemIndex].Use(toname)) {
                if (item_list[HoldingItemIndex].isOnce) {
                    item_list.RemoveAt(HoldingItemIndex);
                    HoldingItemIndex = -1;
                }
            } else {
                Debug.Log("物品使用失败，是否达成条件or是否没有返回判定");
            }
        }

        public List<Item> GetItemList()
        {
            return item_list;
        }

    }

    //***Item经由Bag管理，不单独管理***
    [System.Serializable]
    public class Item
    {
        public string name;
        public bool isOnce;
        public Sprite ico;
        //public abstract Sprite Ico { get; }
        public Func<bool> Get;
        public Func<string, bool> Use;
        public Func<bool> Hold;

        public bool isRead;//第三关，是否直接阅读
        public Func<bool> Read;

        public Item(string nname, bool iisOnce, Sprite iico, Func<bool> gget, Func<string, bool> uuse, Func<bool> hhold, bool iisRead = false, Func<bool> rread = null)
        {
            name = nname;
            isOnce = iisOnce;
            ico = iico;

            Get = gget;
            Use = uuse;
            Hold = hhold;

            isRead = iisRead;
            Read = rread;
        }

    }

}

