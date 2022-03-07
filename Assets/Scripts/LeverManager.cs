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

    //在LevelManager中初始化所有当前关卡的物品
    public abstract void InitItems();
}
