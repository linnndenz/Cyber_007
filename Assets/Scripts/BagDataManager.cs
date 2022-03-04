using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagDataManager
{
    public class Bag
    {
        //private static Bag instance = new Bag();
        public ArrayList item_list = new ArrayList();
        public void AddItem<T>()
        {
            T newitem = System.Activator.CreateInstance<T>();
            item_list.Add(newitem);
        }

        public void HoldItem(int index)
        {
            if (index >= item_list.Count) {

            }
            ((Item)item_list[index]).Hold();
        }

        public void UseItem(int index)
        {
            if (index >= item_list.Count) {
                return;
            }
            ((Item)item_list[index]).Use();
            if (((Item)item_list[index]).IsOnce){
                item_list.RemoveAt(index);
            }
        }
    }

    //***Item经由Bag管理，不单独管理***
    public abstract class Item
    {
        public abstract string Name { get; }
        public abstract bool IsOnce { get; }
        //public abstract Sprite Ico { get; }
        public abstract void Use();
        public abstract void Hold();

    }

}

