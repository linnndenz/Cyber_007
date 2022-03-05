using BagDataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LevelManager_L1 : LevelManager
{
    
    void Start()
    {
        InitItems();
    }

    public override void InitItems()
    {
        item_dict.Add("��Сҩˮ",new Item("��Сҩˮ",true,Resources.Load<Sprite>("Texture_L1/��Сҩˮ"),null,null));
        item_dict.Add("���µĹ���",new Item("���µĹ���", true,Resources.Load<Sprite>("Texture_L1/���µĹ���"),null,null));
        item_dict.Add("����֭",new Item("����֭", true,Resources.Load<Sprite>("Texture_L1/����֭"),null,null));
        item_dict.Add("��ݮ",new Item("��ݮ", true,Resources.Load<Sprite>("Texture_L1/��ݮ"),null,null));
        item_dict.Add("����",new Item("����", true,Resources.Load<Sprite>("Texture_L1/����"),null,null));
        item_dict.Add("Ժ���칫��Կ��",new Item("Ժ���칫��Կ��", true,Resources.Load<Sprite>("Texture_L1/Ժ���칫��Կ��"),null,null));
        item_dict.Add("����",new Item("����", true,Resources.Load<Sprite>("Texture_L1/����"),null,null));
    }

}
