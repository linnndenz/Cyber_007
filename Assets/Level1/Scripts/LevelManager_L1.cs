using BagDataManager;
using DG.Tweening;
using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class LevelManager_L1 : LevelManager
{
    public Transform startPos;
    public BoxCollider2D cook;
    //��-��ݮ
    public BoxCollider2D flower;
    public GameObject strawberry;
    public bool isGetStrawbbry = false;
    //��������-����
    public GameObject eggUI;
    public BoxCollider2D cock;
    public GameObject egg;
    public bool isGetEgg = false;
    //��
    public GameObject book;
    //Ժ���칫��
    public GameObject officeDoor_locked;
    public GameObject officeDoor_unlocked;
    //�����&�¶�Ժ����
    public GameObject lockedBoxUI;
    public GameObject corridor3;
    public GameObject garden;
    public Transform gardenLPos;
    public GameObject gateLock;


    //����
    public GameObject kidvva;
    public GameObject hurtvva;


    private void Awake()
    {
        Instance = this;
        player.transform.position = startPos.position;
    }

    public override void InitItems()
    {
        item_dict.Add("���µĹ���", new Item("���µĹ���", true, Resources.Load<Sprite>("Texture_L1/���µĹ���"), GetBottle, UseBottle, null));
        item_dict.Add("��ݮ", new Item("��ݮ", true, Resources.Load<Sprite>("Texture_L1/��ݮ"), GetStrawberry, null, null));
        item_dict.Add("����", new Item("����", false, Resources.Load<Sprite>("Texture_L1/����"), null, UseHammer, null));
        item_dict.Add("Ժ���칫��Կ��", new Item("Ժ���칫��Կ��", true, Resources.Load<Sprite>("Texture_L1/Ժ���칫��Կ��"), null, OpenOffice, null));
        item_dict.Add("����", new Item("����", true, Resources.Load<Sprite>("Texture_L1/����"), null, null, null));
        item_dict.Add("�¶�Ժ����Կ��", new Item("�¶�Ժ����Կ��", true, Resources.Load<Sprite>("Texture_L1/�¶�Ժ����Կ��"), null, OpenGate, null));
        item_dict.Add("������", new Item("������", true, Resources.Load<Sprite>("Texture_L1/������"), null, null, null));

        redQueen.GetComponent<BoxCollider2D>().enabled = false;
    }

    #region ����
    public GameObject mirrorImg;
    //bool isFirstOpenMirror = true;
    public void OpenMirror()
    {
        //if (!isFirstOpenMirror) return;

        mirrorImg.SetActive(true);
        player.Froze();
        kidvva.SetActive(false);
        hurtvva.SetActive(true);
        player.animator = player.GetComponentInChildren<Animator>();
        flowChart.ExecuteBlock("����");
    }
    public void CloseMirrorImg()
    {
        mirrorImg.SetActive(false);
    }
    #endregion

    #region ����
    public bool UseBottle(string toname)
    {
        if (toname != "��ݮ��") return false;

        flower.enabled = false;
        strawberry.SetActive(true);
        audioManager.PlaySE(1);

        return true;
    }

    public void GetBottle()
    {
        player.Froze();
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
        if (!eggUI.activeSelf) return false;
        audioManager.PlaySE(2);
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

    #region ��
    public void OpenBook()
    {
        book.SetActive(true);
        player.Froze();
    }
    #endregion

    #region ���Կ��
    public void GetOfficeKey()
    {
        bag.AddItem(item_dict["Ժ���칫��Կ��"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//����UI����
        cook.enabled = false;
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

    #region �����&�¶�Ժ����
    public void OpenLockedBoxUI()
    {
        lockedBoxUI.SetActive(true);
        player.Froze();
    }
    public void GetGateKey()
    {
        bag.AddItem(item_dict["�¶�Ժ����Կ��"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//����UI����
    }
    public bool OpenGate(string toname)
    {
        if (toname != "�¶�Ժ����") return false;
        gateLock.SetActive(false);
        player.Froze();
        Invoke(nameof(OnOpenGate), 1.5f);
        return true;
    }
    private void OnOpenGate()
    {
        corridor3.SetActive(false);
        garden.SetActive(true);
        player.transform.position = gardenLPos.position;
        player.DeFroze();

        Manager.Instance.ChangeBGM(0);
    }

    #endregion

    #region ����
    //������
    public BoxCollider2D tomato;
    public bool isGetTomato = false;
    public GameObject redRose;
    public GameObject ironRose;
    public Animator soldierAAnimator;
    public void GetTomato()
    {
        bag.AddItem(item_dict["������"]);//�������ݸ���
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);
        tomato.enabled = false;
        isGetTomato = true;
    }
    public void LoseTomato()
    {
        bag.RemoveItem(item_dict["������"]);
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//����UI����
        redRose.SetActive(true);
        redQueen.GetComponent<BoxCollider2D>().enabled = true;
        soldierAAnimator.SetBool("paint", true);
    }
    #endregion

    #region ��ʺ�
    public Transform redQueen;
    public Transform toRosePos;
    public GameObject soldier;
    public GameObject hospitalBox;
    public Animator redQueenAnimator;
    public void GotoRose()
    {
        soldierAAnimator.SetBool("paint", false);
        player.footstepAudio.Play();
        redQueen.GetComponent<BoxCollider2D>().enabled = false;
        player.transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        player.animator.SetBool("xMove", true);
        redQueenAnimator.SetBool("xMove", true);
        player.transform.DOMoveX(toRosePos.position.x, 4f);
        redQueen.DOMoveX(toRosePos.position.x + 3, 4f).OnComplete(() => {
            hospitalBox.SetActive(true);
            flowChart.ExecuteBlock("��ʺ�2");
            player.animator.SetBool("xMove", false);
            redQueenAnimator.SetBool("xMove", false);
            player.footstepAudio.Stop();
        });
    }
    public void StartChaseVva()
    {
        soldier.SetActive(true);
    }

    public Transform hospitalGate;
    public Image black;
    public GameObject hospitalUI;
    public void OpenHospitalGate()
    {
        player.Froze();
        soldier.SetActive(false);
        hospitalGate.DOMoveX(hospitalGate.transform.position.x + 3.5f, 2f).OnComplete(() => {
            black.color = new Color(0, 0, 0, 0);
            black.gameObject.SetActive(true);
            black.DOColor(new Color(0, 0, 0, 1), 1.5f).OnComplete(() => {
                player.gameObject.SetActive(false);
                garden.SetActive(false);
                hospitalUI.SetActive(true);
                black.DOColor(new Color(0, 0, 0, 0), 1.5f).OnComplete(() => {
                    black.gameObject.SetActive(false);
                    flowChart.ExecuteBlock("����ҽԺ");
                    Manager.Instance.ChangeBGM(-1);
                });
            });
        });
    }
    #endregion

    #region ҽԺ
    public GameObject carAccident;
    public void OpenTV()
    {
        audioManager.PlaySE(8);
        carAccident.SetActive(true);
    }
    public Transform lDoor;
    public Transform lDoorPos;
    public Transform rDoor;
    public Transform rDoorPos;
    public GameObject[] doctors;
    public void ContinueTV()
    {
        Manager.Instance.ChangeBGM(1);
        black.color = new Color(0, 0, 0, 0);
        black.gameObject.SetActive(true);
        black.DOColor(new Color(0, 0, 0, 1), 1.5f).OnComplete(() => {
            carAccident.SetActive(false);
            doctors[0].SetActive(true);
            doctors[1].SetActive(true);
            doctors[2].SetActive(true);
            black.DOColor(new Color(0, 0, 0, 0), 1.5f).OnComplete(() => {
                lDoor.DOMoveX(lDoorPos.position.x, 1.5f);
                rDoor.DOMoveX(rDoorPos.position.x, 1.5f);
                black.gameObject.SetActive(false);
                flowChart.ExecuteBlock("����");
            }).SetDelay(0.5f);
        });
    }




    #endregion

    #region ��̨
    public GameObject chooseHatUI;

    public void ChoseHat()
    {
        black.color = new Color(0, 0, 0, 0);
        black.gameObject.SetActive(true);
        black.DOColor(new Color(0, 0, 0, 1), 1.5f).OnComplete(() => {
            hospitalUI.SetActive(false);
            chooseHatUI.SetActive(true);

            black.DOColor(new Color(0, 0, 0, 0), 1.5f).OnComplete(() => {
                black.gameObject.SetActive(false);
                flowChart.ExecuteBlock("ѡ��ñ��ǰ");
            }).SetDelay(0.5f);
        });
    }

    public CanvasGroup hat;
    public void OpenHat()
    {
        hat.alpha = 0;
        hat.gameObject.SetActive(true);
        DOTween.To(() => hat.alpha, x => hat.alpha = x, 1, 1.5f);
    }
    public GameObject stage;
    public void End()
    {
        black.color = new Color(0, 0, 0, 0);
        black.gameObject.SetActive(true);
        black.DOColor(new Color(0, 0, 0, 1), 1.5f).OnComplete(() => {
            stage.SetActive(true);
            black.DOColor(new Color(0, 0, 0, 0), 1.5f).OnComplete(() => {
                black.gameObject.SetActive(false);
                flowChart.ExecuteBlock("End");
            }).SetDelay(0.5f);
        });
    }
    #endregion
}
