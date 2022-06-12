using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lfourmanager : MonoBehaviour
{
    public int step;//��ǰ���е��Ĳ���
    public int pressed;//��ǰ���µİ�����������step���бȽ��ж��Ƿ�����ȷ
    bool check;
    public Transform start;
    public Transform end;
    float speed = 4;
    bool mov = false;
    public GameObject canve;
   
    private void Start()
    {
        step = 1;
        StartCoroutine("Begin");
    }

    private void Update()
    {
        if (mov == true)
        {
            System.Threading.Thread.Sleep(20);
            Moving();
        }
    }


    public void Button_Press(int i)//ÿ����������ִ��������ܣ���ֻ�ı�pressedֵ��Ȼ������һ�������ж��Ƿ�����ȷ����ӡ
    {
        pressed = i;
        check = true;//���°���ʱʹcheckΪ�棬��ʼ������⣬�����Ҫ��check������Ϊfalse
    }

    void Error()//��������ÿ�ΰ�������ֻ�����һ�Ρ��ú���
    {
        Debug.Log("Wrong");
        //�˴������ٵ���һЩ����������֮��ķ�������Ӵ�����Ч
    }

    bool Check()//��鰴���Ƿ���ȷ������ͱ���
    {
        if (check)
        {
            if (pressed != step)
            {
                Error();
                check = false;
                return false;
            }
            check = false;
            return true;
        }
        return false;
    }

    IEnumerator Begin()
    {
        Debug.Log("����Э��");
        while (step <= 5)//step��1��ʼ����3�����ܹ��Ĳ�����
        {
            yield return StartCoroutine("Step" + step);
            step++;
        }
        yield return null;
    }
    IEnumerator Step1()
    {
        Debug.Log("��ʼЭ��һ");
        yield return new WaitUntil(() => Check() == true);//��һֱ����Check()���������ֱ�������������true��
                                                          //������Ϊ����checkֵ����ÿ�δ���ֻ�����һ�α�����
        Debug.Log("A");
        //�˴����Լ�����Ҫִ�еĺ���
        yield return null;
    }
    IEnumerator Step2()
    {
        Debug.Log("��ʼЭ�̶�");
        yield return new WaitUntil(() => Check() == true);
        Debug.Log("B");
        //�˴����Լ�����Ҫִ�еĺ���
        yield return null;
    }
    IEnumerator Step3()
    {
        Debug.Log("��ʼЭ����");
        yield return new WaitUntil(() => Check() == true);
        Debug.Log("C");
        //�˴����Լ�����Ҫִ�еĺ���
        yield return null;
    }
    IEnumerator Step4()
    {
        Debug.Log("��ʼЭ����");
        yield return new WaitUntil(() => Check() == true);
        Debug.Log("D");
        //�˴����Լ�����Ҫִ�еĺ���
        yield return null;
    }
    IEnumerator Step5()
    {
        Debug.Log("��ʼЭ����");
        yield return new WaitUntil(() => Check() == true);
        Debug.Log("E");
       
        canve.SetActive(false);
        //�˴����Լ�����Ҫִ�еĺ���
        SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
        Texture2D texture2d = (Texture2D)Resources.Load("move");//����Ϊ��ɫ����Ӣ�۽�ɫͼƬ
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));//ע�������ʾ����0.5fֵ
        spr.sprite = sp;
       
        mov = true;
      
        yield return null;
    }

   void Moving()
    {
        transform.position = Vector3.MoveTowards(start.position, end.position, speed * Time.deltaTime);
    }


}

