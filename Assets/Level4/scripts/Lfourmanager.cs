using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lfourmanager : MonoBehaviour
{
    public int step;//当前进行到的步骤
    public int pressed;//当前按下的按键，用来跟step进行比较判断是否点击正确
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


    public void Button_Press(int i)//每个按键并不执行输出功能，而只改变pressed值，然后在另一个方法判断是否点击正确并打印
    {
        pressed = i;
        check = true;//按下按键时使check为真，开始按键检测，检测完要将check重新设为false
    }

    void Error()//错误函数，每次按键错误“只会调用一次”该函数
    {
        Debug.Log("Wrong");
        //此处可以再调用一些动画机函数之类的方法，添加错误特效
    }

    bool Check()//检查按键是否正确，错误就报错
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
        Debug.Log("开启协程");
        while (step <= 5)//step从1开始，“3”是总共的步骤数
        {
            yield return StartCoroutine("Step" + step);
            step++;
        }
        yield return null;
    }
    IEnumerator Step1()
    {
        Debug.Log("开始协程一");
        yield return new WaitUntil(() => Check() == true);//会一直调用Check()这个函数，直到这个函数返回true，
                                                          //但是因为加了check值，故每次错误只会调用一次报错函数
        Debug.Log("A");
        //此处可以加入想要执行的函数
        yield return null;
    }
    IEnumerator Step2()
    {
        Debug.Log("开始协程二");
        yield return new WaitUntil(() => Check() == true);
        Debug.Log("B");
        //此处可以加入想要执行的函数
        yield return null;
    }
    IEnumerator Step3()
    {
        Debug.Log("开始协程三");
        yield return new WaitUntil(() => Check() == true);
        Debug.Log("C");
        //此处可以加入想要执行的函数
        yield return null;
    }
    IEnumerator Step4()
    {
        Debug.Log("开始协程四");
        yield return new WaitUntil(() => Check() == true);
        Debug.Log("D");
        //此处可以加入想要执行的函数
        yield return null;
    }
    IEnumerator Step5()
    {
        Debug.Log("开始协程五");
        yield return new WaitUntil(() => Check() == true);
        Debug.Log("E");
       
        canve.SetActive(false);
        //此处可以加入想要执行的函数
        SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
        Texture2D texture2d = (Texture2D)Resources.Load("move");//更换为红色主题英雄角色图片
        Sprite sp = Sprite.Create(texture2d, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));//注意居中显示采用0.5f值
        spr.sprite = sp;
       
        mov = true;
      
        yield return null;
    }

   void Moving()
    {
        transform.position = Vector3.MoveTowards(start.position, end.position, speed * Time.deltaTime);
    }


}

