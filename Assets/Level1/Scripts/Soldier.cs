using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    public bool isFin;
    public bool missQteBlock;//qte失败
    public GameObject qte;

    public Image black;
    public float initPosX;
    float speed;
    public Transform playerInitPos;
    public Flowchart flowChart;
    public Animator[] animators;
    public Transform player;

    void Start()
    {
        speed = Player.Instance.speed * 0.8f;
    }

    void OnEnable()
    {
        qte.SetActive(true);
        missQteBlock = false;
        once = false;
        once = false;
        once = false;
        isChased = false;
        Player.Instance.animator.SetBool("xMove", true);
        Player.Instance.footstepAudio.Play();
        player.localScale = new Vector3(1, 1, 1);

        initPosX = transform.parent.position.x;
        transform.parent.tag = "Untagged";

        animators[0].SetBool("xMove", true);
        animators[1].SetBool("xMove", true);
    }


    void FixedUpdate()
    {
        if (isChased) return;
        Chase();
    }

    private void OnDisable()
    {
        qte.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "医院门" && !isChased) {
            animators[0].SetBool("xMove", false);
            animators[1].SetBool("xMove", false);
            gameObject.SetActive(false);
        }
        if (isFin) return;

        if (collision.CompareTag("Player") && !isChased) {
            //speed = 0;
            qte.SetActive(false);
            isChased = true;

            black.color = new Color(0, 0, 0, 0);
            black.gameObject.SetActive(true);
            black.DOColor(new Color(0, 0, 0, 1), 1.5f).OnComplete(() => {
                Player.Instance.animator.Play("Idle", 0);
                player.position = playerInitPos.position;
                transform.parent.position = new Vector3(initPosX, transform.parent.position.y, transform.parent.position.z);
                animators[0].SetBool("xMove", false);
                animators[1].SetBool("xMove", false);
                black.DOColor(new Color(0, 0, 0, 0), 1.5f).OnComplete(() => {
                    black.gameObject.SetActive(false);
                    flowChart.ExecuteBlock("红皇后2");
                    gameObject.SetActive(false);
                });
            });
        }
    }

    public bool isChased;
    public bool once;
    public void Chase()
    {
        //士兵跑
        transform.parent.position += new Vector3(speed / 2 * Time.fixedDeltaTime, 0, 0);
        if (isFin) return;
        //玩家跑
        if (missQteBlock) {
            if (!once) {
                Player.Instance.animator.SetTrigger("fall");
                Player.Instance.animator.SetBool("xMove", false);
                Player.Instance.footstepAudio.Stop();
                once = true;
            }

        } else {
            player.position += new Vector3(Player.Instance.speed / 2 * Time.fixedDeltaTime, 0, 0);
        }
    }
}
