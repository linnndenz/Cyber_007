using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public Transform blockParent;
    public float intervalTime = 1f;
    float intervalTimer;
    int curr;

    void OnEnable()
    {
        curr = 0;
        for (int i = 0; i < blockParent.childCount; i++) {
            blockParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (curr >= blockParent.childCount) { return; }//计数超出不继续

        if (intervalTimer < 0) {
            intervalTimer = intervalTime;
            blockParent.GetChild(curr).gameObject.SetActive(true);
            curr++;
        } else {
            intervalTimer -= Time.deltaTime;
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < blockParent.childCount; i++) {
            blockParent.GetChild(i).gameObject.SetActive(false);
        }
    }
}
