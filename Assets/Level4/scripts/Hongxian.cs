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
        Texture2D texture2d = (Texture2D)Resources.Load("hongxian");//����Ϊ��ɫ����Ӣ�۽�ɫͼƬ
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));//ע�������ʾ����0.5fֵ
        spr.sprite = sp;
        gameObject.SetActive(false);
        lanxian.SetActive(true);
    }

    //����⵽����ڸ��������С����¡�����ʱ���������º���
    public void OnPointerDown(PointerEventData eventData)
    {
        //��Ҫ�����Ĵ���
    }




    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
