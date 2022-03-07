using BagDataManager;
using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; protected set; }

    #region 预设
    [SerializeField] LevelManager levelManager;
    [SerializeField] float speed;
    public bool Froze { get; set; }
    public SpriteRenderer holdingSr;
    #endregion

    #region 数据
    private Bag bag = new Bag();
    #endregion //数据

    #region 预设函数==================================================
    Collider2D coll = null;
    readonly string PLAYER = "Player";
    readonly string PICKEDITEM = "PickedItem";
    readonly string DOOR = "Door";
    readonly string INTERACTIVEITEM = "InteractiveItem";
    readonly string TELEPORTER = "Teleporter";

    protected virtual void Update()
    {
        //传送
        if (coll && coll.CompareTag(TELEPORTER)) {
            coll.GetComponent<Teleporter>().Teleport(transform);
            return;
        }

        //捡起物品
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(PICKEDITEM)) {
            PickItem(coll.GetComponent<PickedItem>());
            return;
        }
        //进门
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(DOOR)) {
            coll.GetComponent<Door>().GetIn(transform);
            return;
        }

        //使用物品
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

    #region 自定义函数==================================================

    #region 移动
    float xMove = 0;
    private void Move()
    {
        if (Froze) xMove = 0;
        else xMove = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        transform.position += new Vector3(xMove, 0, 0);

        //转向
        if (Mathf.Abs(xMove) > 0.001f) {
            if (xMove > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    #endregion

    #region hold、getbag，由BagUI调用
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

    #region use/pick物品
    private void UseItem()
    {
        bag.UseItem();//背包数据更新
        BagUI.Instance.Refresh_UseItem();//背包UI更新
        if (bag.HoldingItemIndex == -1) {//持有图片更新
            holdingSr.sprite = null;
        }

    }
    private void PickItem(PickedItem pi)
    {
        if (pi == null) {
            print("拾取物品为空");
            return;
        }
        levelManager.item_dict.TryGetValue(pi.itemName, out Item it);
        pi.PickIt();
        bag.AddItem(it);//背包数据更新
        BagUI.Instance.Refresh_PickItem(bag.GetItemList().Count - 1);//背包UI更新

    }
    #endregion

    #endregion
}
