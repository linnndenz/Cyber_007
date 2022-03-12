using BagDataManager;
using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LevelManager_L1 : LevelManager
{
    //��-��ݮ
    public GameObject flower;
    public GameObject strawberry;
    public bool isGetStrawbbry = false;
    //��������-����
    public GameObject egg;
    public bool isGetEgg = false;
    //��
    public GameObject book;
    //Ժ���칫��
    public GameObject officeDoor_locked;
    public GameObject officeDoor_unlocked;
    //�����
    public GameObject lockedBoxUI;

    private void Awake()
    {
        Instance = this;
    }

    public override void InitItems()
    {
        item_dict.Add("���µĹ���", new Item("���µĹ���", true, Resources.Load<Sprite>("Texture_L1/���µĹ���"), GetBottle, UseBottle, null));
        item_dict.Add("����֭", new Item("����֭", true, Resources.Load<Sprite>("Texture_L1/����֭"), null, null, null));
        item_dict.Add("��ݮ", new Item("��ݮ", true, Resources.Load<Sprite>("Texture_L1/��ݮ"), GetStrawberry, null, null));
        item_dict.Add("����", new Item("����", false, Resources.Load<Sprite>("Texture_L1/����"), null, UseHammer, null));
        item_dict.Add("Ժ���칫��Կ��", new Item("Ժ���칫��Կ��", true, Resources.Load<Sprite>("Texture_L1/Ժ���칫��Կ��"), null, OpenOffice, null));
        item_dict.Add("����", new Item("����", true, Resources.Load<Sprite>("Texture_L1/����"), null, null, null));
        item_dict.Add("�¶�Ժ����Կ��", new Item("�¶�Ժ����Կ��", true, Resources.Load<Sprite>("Texture_L1/�¶�Ժ����Կ��"), null, OpenGate, null));
    }

    #region ����
    public bool UseBottle(string toname)
    {
        if (toname != "��ݮ��") return false;

        flower.SetActive(false);
        strawberry.SetActive(true);
        return true;
    }

    public void GetBottle()
    {
        flowChart.ExecuteBlock("ROBO_���µĹ���");
    }

    public void GetStrawberry()
    {
        isGetStrawbbry = true;
    }
    #endregion

    #region ����
    public bool UseHammer(string toname)
    {
        if (toname != "�����ĵ���" || isGetEgg) return false;
        egg.SetActive(true);
        isGetEgg = true;
        return true;
    }
    #endregion

    #region ��
    public void OpenBook()
    {
        book.SetActive(true);
    }
    #endregion

    #region ���Կ��
    public void GetOfficeKey()
    {
        bag.AddItem(item_dict["Ժ���칫��Կ��"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//����UI����
    }
    #endregion

    #region ʧȥ��ݮ����
    public void LoseStrawberryAndEgg()
    {
        bag.RemoveItem(item_dict["��ݮ"]);
        bag.RemoveItem(item_dict["����"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//����UI����
    }
    #endregion

    #region Ժ���칫��
    public bool OpenOffice(string toname)
    {
        if (toname != "�칫�Ҵ���") return false;
        officeDoor_locked.SetActive(false);
        officeDoor_unlocked.SetActive(true);
        return true;
    }
    #endregion

    #region �����
    public void OpenLockedBoxUI()
    {
        lockedBoxUI.SetActive(true);
    }
    public void GetGateKey()
    {
        bag.AddItem(item_dict["�¶�Ժ����Կ��"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//����UI����
    }
    public bool OpenGate(string toname)
    {
        if (toname != "�¶�Ժ����") return false;

        SceneManager.LoadScene("Scene_Level2");

        return true;
    }

    #endregion

}
