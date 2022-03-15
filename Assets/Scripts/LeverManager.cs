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

    #region ����
    public Bag bag = new Bag();
    #endregion //����


    protected virtual void Start()
    {
        InitItems();
    }

    //��LevelManager�г�ʼ�����е�ǰ�ؿ�����Ʒ
    public abstract void InitItems();

    public static Bag GetBag()
    {
        return Instance.bag;
    }

}
