using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hongxian : MonoBehaviour
{
    public GameObject zhuban;
    public GameObject lanxian;
    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        SpriteRenderer spr = zhuban.GetComponent<SpriteRenderer>();
        Texture2D texture2d = (Texture2D)Resources.Load("hongxian");//更换为红色主题英雄角色图片
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));//注意居中显示采用0.5f值
        spr.sprite = sp;
        gameObject.SetActive(false);
        lanxian.SetActive(true);
    }

    //当检测到鼠标在该物体上有“按下”操作时，触发以下函数
    public void OnPointerDown(PointerEventData eventData)
    {
        //你要触发的代码
    }




    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
