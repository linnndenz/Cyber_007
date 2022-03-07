using BagDataManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LevelManager_L1 : LevelManager
{
    public GameObject flower;
    public GameObject strawberry;
    
    public override void InitItems()
    {
        item_dict.Add("变小药水", new Item("变小药水", true, Resources.Load<Sprite>("Texture_L1/变小药水"), null, null));
        item_dict.Add("床下的罐子", new Item("床下的罐子", true, Resources.Load<Sprite>("Texture_L1/床下的罐子"), UseWater, null));
        item_dict.Add("番茄汁", new Item("番茄汁", true, Resources.Load<Sprite>("Texture_L1/番茄汁"), null, null));
        item_dict.Add("草莓", new Item("草莓", true, Resources.Load<Sprite>("Texture_L1/草莓"), null, null));
        item_dict.Add("锤子", new Item("锤子", false, Resources.Load<Sprite>("Texture_L1/锤子"), null, null));
        item_dict.Add("院长办公室钥匙", new Item("院长办公室钥匙", true, Resources.Load<Sprite>("Texture_L1/院长办公室钥匙"), null, null));
        item_dict.Add("鸡蛋", new Item("鸡蛋", true, Resources.Load<Sprite>("Texture_L1/鸡蛋"), null, null));
    }

    public bool UseWater()
    {
        flower.SetActive(false);
        strawberry.SetActive(true);
        return true;
    }


}
