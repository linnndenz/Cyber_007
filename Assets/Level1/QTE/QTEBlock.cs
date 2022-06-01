using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEBlock : MonoBehaviour
{
    public float speed = 10;
    bool isHit;
    public GameObject hit;
    public GameObject miss;
    public KeyCode keycode;
    public Soldier soldier;
    public Transform initPos;
    public Transform endPos;
    public Transform lPos;
    public Transform rPos;

    void OnEnable()
    {
        isHit = false;
        transform.position = initPos.position;
        hit.SetActive(false);
        miss.SetActive(false);
    }

    void Update()
    {
        if (isHit) return;
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (transform.position.x > lPos.position.x && transform.position.x < rPos.position.x) {
            if (Input.GetKeyDown(keycode)) {
                LevelManager.Instance.audioManager.PlaySE(10);
                isHit = true;
                hit.SetActive(true);
                StartCoroutine(Close());
            } else if (Input.anyKeyDown) {
                LevelManager.Instance.audioManager.PlaySE(10);
                isHit = true;
                miss.SetActive(true);
                soldier.missQteBlock = true;
                StartCoroutine(Close());
            }
        } else if (transform.position.x > rPos.position.x) {//过了rPos判定miss
            soldier.missQteBlock = true;
            miss.SetActive(true);
        }

        if (transform.position.x > endPos.position.x) {//过了endpos关闭
            gameObject.SetActive(false);
        }
    }

    public IEnumerator Close()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

}
