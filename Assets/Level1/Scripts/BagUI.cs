using BagDataManager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//��Ʒhold���ɴ˼�⣬�ٴ������ݸ�Player
public class BagUI : MonoBehaviour
{
    public static BagUI Instance { get; protected set; }

    [Header("��ʾ")]
    [SerializeField] protected Color chosenColor;
    [SerializeField] protected Transform slotParent;
    protected Slot[] slots;
    [SerializeField] protected Transform bagPanel;

    [Header("����")]
    protected Bag bag;

    protected virtual void Awake()
    {
        //��������
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    protected virtual void Start()
    {
        //���ݳ�ʼ��
        bag = LevelManager.GetBag();
        slots = slotParent.GetComponentsInChildren<Slot>();
        ReFresh();
    }


    #region hold/use/pick
    public virtual void ClickSlot(int index)
    {
        //�Ķ�����Ʒ
        if (index >= 0 && index < bag.GetItemList().Count && bag.GetItemList()[index].isRead) {
            bag.GetItemList()[index].Read();
        } else {
            if (index >= 0 && slots[index].IsChosen) {
                //����bag��HoldItem����
                Player.Instance.HoldItem(index);
                //����slot��IsChosen
                for (int i = 0; i < slots.Length; i++) {
                    if (i != index) {
                        slots[i].IsChosen = false;
                        slots[i].GetComponent<Image>().color = Color.white;
                    } else {
                        slots[i].GetComponent<Image>().color = chosenColor;
                    }
                }

            } else {
                Player.Instance.HoldItem(-1);

                for (int i = 0; i < slots.Length; i++) {
                    slots[i].IsChosen = false;
                    slots[i].GetComponent<Image>().color = Color.white;
                }

            }
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
    protected void ReFresh()
    {
        List<Item> items = bag.GetItemList();
        if (items.Count > slots.Length) print("�����ﳤ�ȳ�����������");
        for (int i = 0; i < slots.Length; i++) {
            if (i < items.Count) {
                slots[i].Ico.sprite = items[i].ico;
                slots[i].Ico.gameObject.SetActive(true);
            } else {
                slots[i].Ico.sprite = null;
                slots[i].Ico.gameObject.SetActive(false);
            }
        }
        //����slot��IsChosen
        for (int i = 0; i < Instance.slots.Length; i++) {
            if (i != bag.HoldingItemIndex) {
                Instance.slots[i].IsChosen = false;
                Instance.slots[i].GetComponent<Image>().color = Color.white;
            } else {
                Instance.slots[i].GetComponent<Image>().color = Instance.chosenColor;
            }
        }
    }
    #endregion

}
