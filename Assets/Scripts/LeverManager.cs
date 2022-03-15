using BagDataManager;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; protected set; }

    public Dictionary<string, Item> item_dict = new Dictionary<string, Item>();

    public Flowchart flowChart;

    public Player player;

    #region 数据
    public Bag bag = new Bag();
    #endregion //数据


    protected virtual void Start()
    {
        InitItems();
    }

    //在LevelManager中初始化所有当前关卡的物品
    public abstract void InitItems();

    public static Bag GetBag()
    {
        return Instance.bag;
    }

}
