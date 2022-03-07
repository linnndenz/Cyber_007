using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BagDataManager
{
    public class Bag
    {
        private List<Item> item_list = new List<Item>();

        public int HoldingItemIndex { get; private set; }

        public void AddItem(Item item)
        {
            item_list.Add(item);
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

        public void UseItem()
        {
            if (HoldingItemIndex >= item_list.Count || HoldingItemIndex < 0) return;

            if (item_list[HoldingItemIndex].Use != null && item_list[HoldingItemIndex].Use()) {
                if (item_list[HoldingItemIndex].isOnce) {
                    item_list.RemoveAt(HoldingItemIndex);
                    HoldingItemIndex = -1;
                }
            } else {
                Debug.Log("��Ʒʹ��ʧ�ܣ��Ƿ�������or�Ƿ�û�з����ж�");
            }
        }

        public List<Item> GetItemList()
        {
            return item_list;
        }

    }

    //***Item����Bag��������������***
    [System.Serializable]
    public class Item
    {
        public string name;
        public bool isOnce;
        public Sprite ico;
        //public abstract Sprite Ico { get; }
        public Func<bool> Use;
        public Func<bool> Hold;

        public Item(string nname, bool iisOnce, Sprite iico, Func<bool> uuse, Func<bool> hhold)
        {
            name = nname;
            isOnce = iisOnce;
            ico = iico;
            Use = uuse;
            Hold = hhold;
        }

    }

}

