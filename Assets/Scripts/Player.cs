using BagDataManager;
using DG.Tweening;
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
    public bool IsInPickItem { get; private set; }
    public SpriteRenderer holdingSr;
    #endregion

    #region ����
    private Bag bag = new Bag();
    #endregion //����


    #region Ԥ�躯��==================================================
    string PICKEDITEM = "PickedItem";
    PickedItem pickedItem = null;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //����ɼ�����Ʒ��Χ����������
        if (collision.CompareTag(PICKEDITEM)) {
            IsInPickItem = true;
            pickedItem = collision.transform.GetComponent<PickedItem>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�뿪�ɼ�����Ʒ��Χ
        if (collision.CompareTag(PICKEDITEM)) {
            IsInPickItem = false;
            pickedItem = null;
        }
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region �Զ��庯��==================================================

    //�ƶ�
    float xMove = 0;
    private void Move()
    {
        if (Froze) xMove = 0;
        else xMove = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        transform.position += new Vector3(xMove, 0, 0);

        //ת��
        if (Mathf.Abs(xMove) > 0.001f) {
            if (xMove > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    //hold/use/pick��Ʒ����BagUI����
    public void HoldItem(int index)
    {
        bag.HoldItem(index);
        if (index >= 0 && index < bag.GetItemList().Count) {
            holdingSr.sprite = bag.GetItemList()[index].ico;
        } else {
            holdingSr.sprite = null;
        }
    }

    public void UseItem()
    {
        holdingSr.transform.DOPunchScale(new Vector3(0.7f, 0.2f, 1), 0.2f);
        bag.UseItem();
    }

    public Bag GetBag()
    {
        return bag;
    }

    public void PickItem()
    {
        if(pickedItem == null) {
            print("ʰȡ��ƷΪ��");
            return;
        }
        levelManager.item_dict.TryGetValue(pickedItem.itemName,out Item it);
        pickedItem.PickIt();
        bag.AddItem(it);
    }
    #endregion
}
