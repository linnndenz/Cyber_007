using BagDataManager;
using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LevelManager_L1 : LevelManager
{
    //花-草莓
    public GameObject flower;
    public GameObject strawberry;
    public bool isGetStrawbbry = false;
    //公鸡雕像-鸡蛋
    public GameObject egg;
    public bool isGetEgg = false;
    //书
    public GameObject book;
    //院长办公室
    public GameObject officeDoor_locked;
    public GameObject officeDoor_unlocked;
    //密码盒
    public GameObject lockedBoxUI;

    private void Awake()
    {
        Instance = this;
    }

    public override void InitItems()
    {
        item_dict.Add("床下的罐子", new Item("床下的罐子", true, Resources.Load<Sprite>("Texture_L1/床下的罐子"), GetBottle, UseBottle, null));
        item_dict.Add("番茄汁", new Item("番茄汁", true, Resources.Load<Sprite>("Texture_L1/番茄汁"), null, null, null));
        item_dict.Add("草莓", new Item("草莓", true, Resources.Load<Sprite>("Texture_L1/草莓"), GetStrawberry, null, null));
        item_dict.Add("锤子", new Item("锤子", false, Resources.Load<Sprite>("Texture_L1/锤子"), null, UseHammer, null));
        item_dict.Add("院长办公室钥匙", new Item("院长办公室钥匙", true, Resources.Load<Sprite>("Texture_L1/院长办公室钥匙"), null, OpenOffice, null));
        item_dict.Add("鸡蛋", new Item("鸡蛋", true, Resources.Load<Sprite>("Texture_L1/鸡蛋"), null, null, null));
        item_dict.Add("孤儿院大门钥匙", new Item("孤儿院大门钥匙", true, Resources.Load<Sprite>("Texture_L1/孤儿院大门钥匙"), null, OpenGate, null));
    }

    #region 罐子
    public bool UseBottle(string toname)
    {
        if (toname != "草莓花") return false;

        flower.SetActive(false);
        strawberry.SetActive(true);
        return true;
    }

    public void GetBottle()
    {
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
        egg.SetActive(true);
        isGetEgg = true;
        return true;
    }
    #endregion

    #region 书
    public void OpenBook()
    {
        book.SetActive(true);
    }
    #endregion

    #region 获得钥匙
    public void GetOfficeKey()
    {
        bag.AddItem(item_dict["院长办公室钥匙"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//背包UI更新
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

    #region 密码盒
    public void OpenLockedBoxUI()
    {
        lockedBoxUI.SetActive(true);
    }
    public void GetGateKey()
    {
        bag.AddItem(item_dict["孤儿院大门钥匙"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//背包UI更新
    }
    public bool OpenGate(string toname)
    {
        if (toname != "孤儿院大门") return false;

        SceneManager.LoadScene("Scene_Level2");

        return true;
    }

    #endregion

}
