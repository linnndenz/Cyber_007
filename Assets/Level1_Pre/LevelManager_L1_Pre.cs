using BagDataManager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager_L1_Pre : LevelManager
{
    public Player_L1_Pre player;
    public Image black;

    public override void InitItems()
    {
        item_dict.Add("��Сҩˮ", new Item("��Сҩˮ", true, Resources.Load<Sprite>("Texture_L1/��Сҩˮ"), null, UseShrinkLiquid, null));
    }

    private void Awake()
    {
        Instance = this;
        player.Froze();
    }


    public bool UseShrinkLiquid(string toname)
    {
        if (toname != "�Լ�") return false;
        player.Froze();
        Vector3 initScale = player.transform.localScale;
        black.color = new Color(0, 0, 0, 0);
        Sequence anims = DOTween.Sequence();
        anims.Append(player.transform.DOScale(initScale * 0.5f, 0.5f));
        anims.Append(player.transform.DOScale(initScale * 0.7f, 0.5f));
        anims.Append(player.transform.DOScale(initScale * 0.3f, 0.5f));
        anims.Append(player.transform.DOScale(initScale * 0.5f, 0.5f));
        anims.Append(player.transform.DOScale(initScale * 0.3f, 0.5f).OnComplete(()=>black.gameObject.SetActive(true)));
        anims.Append(black.DOColor(new Color(0, 0, 0, 1), 1f));
        anims.OnComplete(()=>SceneManager.LoadScene("Scene_Level1"));
        anims.Play();
        return true;
    }
}
