using BagDataManager;
using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public static Player Instance { get; protected set; }

    #region 预设
    [HideInInspector]public AudioSource footstepAudio;
    [SerializeField] protected LevelManager levelManager;
    public float speed;
    [HideInInspector] public bool froze;
    private bool frozeMove;
    private bool frozeInput;
    protected SpriteRenderer holdingSr;
    public Flowchart flowChart;
    #endregion

    #region 预设函数==================================================
    protected Collider2D coll = null;
    protected readonly string PLAYER = "Player";
    protected readonly string PICKEDITEM = "PickedItem";
    protected readonly string DOOR = "Door";
    protected readonly string INTERACTIVEITEM = "InteractiveItem";
    protected readonly string TELEPORTER = "Teleporter";
    protected readonly string CHARACTER = "Character";

    protected virtual void Start()
    {
        holdingSr = transform.Find("Hold").GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        footstepAudio = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        //动画
        SwitchAnim();
        //玩家Froze！！
        if (froze) return;

        //传送
        if (coll && coll.CompareTag(TELEPORTER)) {
            coll.GetComponent<Teleporter>().Teleport(transform);
            return;
        }
        //捡起物品
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(PICKEDITEM)) {
            PickItem(coll.gameObject);
            return;
        }
        //特殊交互物品，在子类中写
        //if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(INTERACTIVEITEM)){}
        //进门
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(DOOR)) {
            coll.GetComponent<Door>().GetIn(transform);
            return;
        }

        //使用物品
        if (Input.GetKeyDown(KeyCode.R)) {
            holdingSr.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 1), 0.2f);
            if (coll && coll.CompareTag(INTERACTIVEITEM)) {
                UseItem(coll.name);
            } else {
                UseItem("自己");
            }
        }

        //对话
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(CHARACTER)) {
            Talk();
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
        if (!collision.CompareTag(PLAYER) && collision == coll) {
            coll = null;
        }
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region 自定义函数==================================================

    public void Froze()
    {
        froze = true;
    }


    public void DeFroze()
    {
        froze = false;
    }

    public void FrozeMove()
    {
        frozeMove = true;
    }
    public void DeFrozeMove()
    {
        frozeMove = false;
    }

    public void FrozeInput()
    {
        frozeInput = true;
    }

    public void DeFrozeInput()
    {
        frozeInput = false;
    }
    #region 移动
    float xMove = 0;
    private void Move()
    {
        if (frozeInput) return;
        if (froze || frozeMove) xMove = 0;
        else xMove = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        transform.position += new Vector3(xMove, 0, 0);

        //转向 + 音效
        if (Mathf.Abs(xMove) > 0.001f) {
            if (xMove > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            if (!footstepAudio.isPlaying) footstepAudio.Play();
        } else {
            footstepAudio.Stop();
        }


    }
    #endregion

    #region 动画
    public Animator animator;
    readonly string XMOVE = "xMove";
    private void SwitchAnim()
    {
        if (frozeInput) return;
        if (Mathf.Abs(xMove) > 0.01f) {
            animator.SetBool(XMOVE, true);
        } else {
            animator.SetBool(XMOVE, false);
        }
    }
    #endregion

    #region hold、getbag，由BagUI调用
    public void HoldItem(int index)
    {
        levelManager.bag.HoldItem(index);
        if (index >= 0 && index < levelManager.bag.GetItemList().Count) {
            holdingSr.sprite = levelManager.bag.GetItemList()[index].ico;
        } else {
            holdingSr.sprite = null;
        }
    }


    #endregion

    #region use/pick物品
    private void UseItem(string toname)
    {
        levelManager.bag.UseItem(toname);//背包数据更新
        BagUI.Instance.Refresh_UseItem();//背包UI更新
        if (levelManager.bag.HoldingItemIndex == -1) {//持有图片更新
            holdingSr.sprite = null;
        }

    }
    private void PickItem(GameObject obj)
    {
        levelManager.item_dict.TryGetValue(obj.name, out Item it);
        obj.SetActive(false);
        levelManager.bag.AddItem(it);//背包数据更新
        BagUI.Instance.Refresh_PickItem(levelManager.bag.GetItemList().Count - 1);//背包UI更新

    }
    #endregion

    #region 对话
    protected virtual void Talk()
    {
        Froze();
    }

    #endregion

    #endregion
}
