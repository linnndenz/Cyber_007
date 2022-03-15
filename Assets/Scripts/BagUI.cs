using BagDataManager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ʒhold���ɴ˼�⣬�ٴ������ݸ�Player
public class BagUI : MonoBehaviour
{
    public static BagUI Instance { get; protected set; }

    [Header("��ʾ")]
    [SerializeField] private Transform chosenImg;
    [SerializeField] private Transform slotParent;
    private Slot[] slots;
    [SerializeField] private Transform bagPanel;
    [SerializeField] private Transform upPos;
    [SerializeField] private Transform downPos;

    [Header("����")]
    private Bag bag;

    private void Awake()
    {
        //��������
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    void Start()
    {
        //���ݳ�ʼ��
        bag = LevelManager.GetBag();
        slots = slotParent.GetComponentsInChildren<Slot>();
        ReFresh();
    }


    #region hold/use/pick
    public void ClickSlot(int index)
    {
        if (index >= 0 && slots[index].IsChosen) {
            //����bag��HoldItem����
            Player.Instance.HoldItem(index);
            //����slot��IsChosen
            for (int i = 0; i < slots.Length; i++) {
                if (i != index) slots[i].IsChosen = false;
            }
            //ѡ��ͼ��
            chosenImg.gameObject.SetActive(true);
            chosenImg.position = slots[index].transform.position;
        } else {
            Player.Instance.HoldItem(-1);

            for (int i = 0; i < slots.Length; i++) {
                slots[i].IsChosen = false;
            }

            chosenImg.gameObject.SetActive(false);
        }
    }
    //use��player����
    public void Refresh_UseItem()
    {
        ReFresh();
    }
    //Pick��player����
    public void Refresh_PickItem(int lastindex)
    {
        if (lastindex >= 0) {
            slots[lastindex].IsChosen = true;
        }
        ClickSlot(lastindex);
        ReFresh();
    }
    #endregion

    #region ˢ��
    private void ReFresh()
    {
        List<Item> items = bag.GetItemList();
        if (items.Count > slots.Length) print("�����ﳤ�ȳ�����������");
        for (int i = 0; i < slots.Length; i++) {
            if (i < items.Count) {
                slots[i].Ico.sprite = items[i].ico;
            } else {
                slots[i].Ico.sprite = null;
            }
        }
        //����slot��IsChosen
        for (int i = 0; i < Instance.slots.Length; i++) {
            if (i != bag.HoldingItemIndex) Instance.slots[i].IsChosen = false;
        }
        if (bag.HoldingItemIndex == -1) {
            chosenImg.gameObject.SetActive(false);
        }
    }
    #endregion


    #region �����������
    bool isDown = true;
    public void Drawer()
    {
        isDown = !isDown;
        if (isDown) {
            BagDown();
        } else {
            BagUp();
        }
    }
    public static void BagDown()
    {
        Instance.bagPanel.DOMoveY(Instance.downPos.position.y, 0.3f);
    }
    public static void BagUp()
    {
        Instance.bagPanel.DOMoveY(Instance.upPos.position.y, 0.3f);
    }
    #endregion
}
