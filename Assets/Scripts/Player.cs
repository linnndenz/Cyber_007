using BagDataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; protected set; }

    #region 预设
    [SerializeField] float speed;
    public bool Froze { get; set; }
    #endregion

    #region 数据
    public Bag bag = new Bag();
    #endregion

   

    void FixedUpdate()
    {
        Move();
    }

    float xMove = 0;
    private void Move()
    {
        if (Froze) xMove = 0;
        else xMove = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        transform.position += new Vector3(xMove, 0, 0);

        //转向
        if (Mathf.Abs(xMove) > 0.001f) {
            if (xMove > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }


    }
}
