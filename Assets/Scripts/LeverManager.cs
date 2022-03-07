using BagDataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    public Dictionary<string, Item> item_dict = new Dictionary<string, Item>();

    protected virtual void Start()
    {
        InitItems();
    }

    //��LevelManager�г�ʼ�����е�ǰ�ؿ�����Ʒ
    public abstract void InitItems();
}
