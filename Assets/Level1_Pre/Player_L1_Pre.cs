using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_L1_Pre : Player
{
    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public GameObject eTip;
    public Transform rTip;
    protected override void Update()
    {
        base.Update();
        if(coll == null) {
            eTip.SetActive(false);
        } else {
            eTip.SetActive(true);
        }

        if(transform.localScale.x <0) {
            rTip.localScale = new Vector3(-Mathf.Abs(rTip.localScale.x), rTip.localScale.y, rTip.localScale.z);
        } else {
            rTip.localScale = new Vector3(Mathf.Abs(rTip.localScale.x), rTip.localScale.y, rTip.localScale.z);
        }
    }
}
