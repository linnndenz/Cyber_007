using BagDataManager;
using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public static Player Instance { get; protected set; }

    #region Ԥ��
    [HideInInspector]public AudioSource footstepAudio;
    [SerializeField] protected LevelManager levelManager;
    public float speed;
    [HideInInspector] public bool froze;
    private bool frozeMove;
    private bool frozeInput;
    protected SpriteRenderer holdingSr;
    public Flowchart flowChart;
    #endregion

    #region Ԥ�躯��==================================================
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
        //����
        SwitchAnim();
        //���Froze����
        if (froze) return;

        //����
        if (coll && coll.CompareTag(TELEPORTER)) {
            coll.GetComponent<Teleporter>().Teleport(transform);
            return;
        }
        //������Ʒ
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(PICKEDITEM)) {
            PickItem(coll.gameObject);
            return;
        }
        //���⽻����Ʒ����������д
        //if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(INTERACTIVEITEM)){}
        //����
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(DOOR)) {
            coll.GetComponent<Door>().GetIn(transform);
            return;
        }

        //ʹ����Ʒ
        if (Input.GetKeyDown(KeyCode.R)) {
            holdingSr.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 1), 0.2f);
            if (coll && coll.CompareTag(INTERACTIVEITEM)) {
                UseItem(coll.name);
            } else {
                UseItem("�Լ�");
            }
        }

        //�Ի�
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

    #region �Զ��庯��==================================================

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
    #region �ƶ�
    float xMove = 0;
    private void Move()
    {
        if (frozeInput) return;
        if (froze || frozeMove) xMove = 0;
        else xMove = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        transform.position += new Vector3(xMove, 0, 0);

        //ת�� + ��Ч
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

    #region ����
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

    #region hold��getbag����BagUI����
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

    #region use/pick��Ʒ
    private void UseItem(string toname)
    {
        levelManager.bag.UseItem(toname);//�������ݸ���
        BagUI.Instance.Refresh_UseItem();//����UI����
        if (levelManager.bag.HoldingItemIndex == -1) {//����ͼƬ����
            holdingSr.sprite = null;
        }

    }
    private void PickItem(GameObject obj)
    {
        levelManager.item_dict.TryGetValue(obj.name, out Item it);
        obj.SetActive(false);
        levelManager.bag.AddItem(it);//�������ݸ���
        BagUI.Instance.Refresh_PickItem(levelManager.bag.GetItemList().Count - 1);//����UI����

    }
    #endregion

    #region �Ի�
    protected virtual void Talk()
    {
        Froze();
    }

    #endregion

    #endregion
}
