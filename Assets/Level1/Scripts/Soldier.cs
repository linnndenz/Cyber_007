using DG.Tweening;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    public Image black;
    public float initPosX;
    float speed;
    public Transform playerInitPos;
    public Flowchart flowChart;
    public Animator[] animators;

    void Start()
    {
        speed = Player.Instance.speed * 0.8f;
    }

    void OnEnable()
    {
        initPosX = transform.parent.position.x;
        transform.parent.tag = "Untagged";

        animators[0].SetBool("xMove", true);
        animators[1].SetBool("xMove", true);
    }

    void OnDisable()
    {
        speed = Player.Instance.speed * 0.8f;
    }

    void FixedUpdate()
    {
        Chase();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            speed = 0;
            Player.Instance.Froze();
            black.gameObject.SetActive(true);
            black.color = new Color(0, 0, 0, 0);
            black.DOColor(new Color(0, 0, 0, 1), 1.5f).OnComplete(() => {
                Player.Instance.transform.position = playerInitPos.position;
                transform.parent.position = new Vector3(initPosX, transform.parent.position.y, transform.parent.position.z);
                black.DOColor(new Color(0, 0, 0, 0), 1.5f).OnComplete(() => {
                    black.gameObject.SetActive(false);
                    flowChart.ExecuteBlock("ºì»Êºó2");
                    gameObject.SetActive(false);
                });
            });
        }
    }

    public void Chase()
    {
        transform.parent.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
    }
}
