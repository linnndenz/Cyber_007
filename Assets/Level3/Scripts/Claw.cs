using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public bool isHooting;
    //preMove的左右点
    [Header("下钩之前移动")]
    public float preMoveSpeed;
    public Transform leftPos;
    public Transform rightPos;
    [Header("下钩")]
    public Transform hook;
    public float moveSpeed;
    public float moveDistance;
    bool isBacking;

    MinerGame minerGame;

    void Start()
    {
        minerGame = GetComponentInParent<MinerGame>();
        ResetClaw();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHooting && !isBacking) {
            //得分
            if (collision.CompareTag("Point")) {
                minerGame.GetPoint();
                collision.gameObject.SetActive(false);
                StopHook();
            }

            //扣血
            if (collision.CompareTag("TextWall")) {
                minerGame.GetHurt();
                StopHook();
            }

        }

    }

    void Update()
    {
        if (!isHooting && Input.GetKeyDown(KeyCode.E)) {
            isHooting = true;
        }

        if (!isHooting) {
            PreMove();
        } else {
            Hooking();
        }
    }

    //发射前的移动状态
    int preMoveDir = 1;
    public void PreMove()
    {
        if (transform.position.x > rightPos.position.x) preMoveDir = -1;
        else if (transform.position.x < leftPos.position.x) preMoveDir = 1;

        transform.position += new Vector3(preMoveDir * preMoveSpeed * Time.deltaTime, 0, 0);
    }

    //发射状态
    public void Hooking()
    {
        //hook回收
        if (Mathf.Abs(hook.localPosition.y) > moveDistance && !isBacking) {
            StopHook();
            return;
        }

        //hook发射移动
        hook.localPosition -= new Vector3(0, moveSpeed * Time.deltaTime, 0);

        //旋转发射器
        if (Input.GetMouseButtonDown(0)) {
            if (transform.localEulerAngles.z > 250 && transform.localEulerAngles.z < 270) return;
            transform.localEulerAngles += new Vector3(0, 0, -10);
        } else if (Input.GetMouseButtonDown(1)) {
            if (transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 110) return;
            transform.localEulerAngles += new Vector3(0, 0, 10);
        }
    }

    //重置爪子
    public void ResetClaw()
    {
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = new Vector3(0, transform.position.y, 0);
        hook.localPosition = Vector3.zero;
    }

    //发射中止/终止
    public void StopHook()
    {
        isBacking = true;
        hook.DOMove(transform.position, 1f).OnComplete(
            () => {
                isBacking = false;
                isHooting = false;
                transform.localEulerAngles = Vector3.zero;
            });
    }

}
