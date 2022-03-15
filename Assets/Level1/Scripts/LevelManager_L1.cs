using BagDataManager;
using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LevelManager_L1 : LevelManager
{
    public BoxCollider2D cook;
    //花-草莓
    public BoxCollider2D flower;
    public GameObject strawberry;
    public bool isGetStrawbbry = false;
    //公鸡雕像-鸡蛋
    public GameObject eggUI;
    public BoxCollider2D cock;
    public GameObject egg;
    public bool isGetEgg = false;
    //书
    public GameObject book;
    //院长办公室
    public GameObject officeDoor_locked;
    public GameObject officeDoor_unlocked;
    //密码盒&孤儿院大门
    public GameObject lockedBoxUI;
    public GameObject corridor3;
    public GameObject garden;
    public Transform gardenLPos;
    public GameObject gateLock;

    //红颜料
    public BoxCollider2D tomato;
    public bool isGetTomato = false;

    //镜子
    public GameObject kidvva;
    public GameObject hurtvva;

    private void Awake()
    {
        Instance = this;
    }

    public override void InitItems()
    {
        item_dict.Add("床下的罐子", new Item("床下的罐子", true, Resources.Load<Sprite>("Texture_L1/床下的罐子"), GetBottle, UseBottle, null));
        item_dict.Add("草莓", new Item("草莓", true, Resources.Load<Sprite>("Texture_L1/草莓"), GetStrawberry, null, null));
        item_dict.Add("锤子", new Item("锤子", false, Resources.Load<Sprite>("Texture_L1/锤子"), null, UseHammer, null));
        item_dict.Add("院长办公室钥匙", new Item("院长办公室钥匙", true, Resources.Load<Sprite>("Texture_L1/院长办公室钥匙"), null, OpenOffice, null));
        item_dict.Add("鸡蛋", new Item("鸡蛋", true, Resources.Load<Sprite>("Texture_L1/鸡蛋"), null, null, null));
        item_dict.Add("孤儿院大门钥匙", new Item("孤儿院大门钥匙", true, Resources.Load<Sprite>("Texture_L1/孤儿院大门钥匙"), null, OpenGate, null));
        item_dict.Add("红颜料", new Item("红颜料", true, Resources.Load<Sprite>("Texture_L1/红颜料"), null, null, null));
    }

    #region 罐子
    public bool UseBottle(string toname)
    {
        if (toname != "草莓花") return false;

        flower.enabled = false;
        strawberry.SetActive(true);
        return true;
    }

    public void GetBottle()
    {
        player.Froze();
        flowChart.ExecuteBlock("ROBO_床下的罐子");
    }

    public void GetStrawberry()
    {
        isGetStrawbbry = true;
    }
    #endregion

    #region 锤子
    public bool UseHammer(string toname)
    {
        if (toname != "公鸡的雕像" || isGetEgg) return false;
        if (!eggUI.activeSelf) return false;
        egg.SetActive(true);
        isGetEgg = true;
        cock.enabled = false;
        eggUI.SetActive(false);
        player.DeFrozeMove();
        return true;
    }

    public void OpenEggUI()
    {
        player.FrozeMove();
        eggUI.SetActive(true);
    }
    #endregion

    #region 书
    public void OpenBook()
    {
        book.SetActive(true);
        player.Froze();
    }
    #endregion

    #region 获得钥匙
    public void GetOfficeKey()
    {
        bag.AddItem(item_dict["院长办公室钥匙"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//背包UI更新
        cook.enabled = false;
    }
    #endregion

    #region 失去草莓鸡蛋
    public void LoseStrawberryAndEgg()
    {
        bag.RemoveItem(item_dict["草莓"]);
        bag.RemoveItem(item_dict["鸡蛋"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//背包UI更新
    }
    #endregion

    #region 院长办公室
    public bool OpenOffice(string toname)
    {
        if (toname != "办公室大门") return false;
        officeDoor_locked.SetActive(false);
        officeDoor_unlocked.SetActive(true);
        return true;
    }
    #endregion

    #region 密码盒&孤儿院大门
    public void OpenLockedBoxUI()
    {
        lockedBoxUI.SetActive(true);
        player.Froze();
    }
    public void GetGateKey()
    {
        bag.AddItem(item_dict["孤儿院大门钥匙"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//背包UI更新
    }
    public bool OpenGate(string toname)
    {
        if (toname != "孤儿院大门") return false;
        gateLock.SetActive(false);
        Invoke(nameof(OnOpenGate), 2f);
        return true;
    }
    private void OnOpenGate()
    {
        corridor3.SetActive(false);
        garden.SetActive(true);
        player.transform.position = gardenLPos.position;
        player.DeFroze();
    }

    #endregion

    #region 番茄
    public void GetTomato()
    {
        bag.AddItem(item_dict["红颜料"]);//背包数据更新
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);
        tomato.enabled = false;
        isGetTomato = true;
    }
    public void LoseTomato()
    {
        bag.RemoveItem(item_dict["红颜料"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//背包UI更新
    }
    #endregion
}
