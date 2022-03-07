using BagDataManager;
using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; protected set; }

    #region Ԥ��
    [SerializeField] LevelManager levelManager;
    [SerializeField] float speed;
    public bool Froze { get; set; }
    public SpriteRenderer holdingSr;
    #endregion

    #region ����
    private Bag bag = new Bag();
    #endregion //����

    #region Ԥ�躯��==================================================
    Collider2D coll = null;
    readonly string PLAYER = "Player";
    readonly string PICKEDITEM = "PickedItem";
    readonly string DOOR = "Door";
    readonly string INTERACTIVEITEM = "InteractiveItem";
    readonly string TELEPORTER = "Teleporter";

    protected virtual void Update()
    {
        //����
        if (coll && coll.CompareTag(TELEPORTER)) {
            coll.GetComponent<Teleporter>().Teleport(transform);
            return;
        }

        //������Ʒ
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(PICKEDITEM)) {
            PickItem(coll.GetComponent<PickedItem>());
            return;
        }
        //����
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(DOOR)) {
            coll.GetComponent<Door>().GetIn(transform);
            return;
        }

        //ʹ����Ʒ
        if (Input.GetKeyDown(KeyCode.R)) {
            holdingSr.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 1), 0.2f);
            if (coll && coll.CompareTag(INTERACTIVEITEM)) {
                UseItem();
            }
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER)) {
            coll = collision;
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER)) {
            coll = null;
        }
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region �Զ��庯��==================================================

    #region �ƶ�
    float xMove = 0;
    private void Move()
    {
        if (Froze) xMove = 0;
        else xMove = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        transform.position += new Vector3(xMove, 0, 0);

        //ת��
        if (Mathf.Abs(xMove) > 0.001f) {
            if (xMove > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    #endregion

    #region hold��getbag����BagUI����
    public void HoldItem(int index)
    {
        bag.HoldItem(index);
        if (index >= 0 && index < bag.GetItemList().Count) {
            holdingSr.sprite = bag.GetItemList()[index].ico;
        } else {
            holdingSr.sprite = null;
        }
    }
    public Bag GetBag()
    {
        return Instance.bag;
    }

    #endregion

    #region use/pick��Ʒ
    private void UseItem()
    {
        bag.UseItem();//�������ݸ���
        BagUI.Instance.Refresh_UseItem();//����UI����
        if (bag.HoldingItemIndex == -1) {//����ͼƬ����
            holdingSr.sprite = null;
        }

    }
    private void PickItem(PickedItem pi)
    {
        if (pi == null) {
            print("ʰȡ��ƷΪ��");
            return;
        }
        levelManager.item_dict.TryGetValue(pi.itemName, out Item it);
        pi.PickIt();
        bag.AddItem(it);//�������ݸ���
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//����UI����

    }
    #endregion

    #endregion
}
