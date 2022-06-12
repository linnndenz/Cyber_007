using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Beibao : MonoBehaviour
{
    public string Rose;
    public string Rob;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (SaveData.Instance.isGetRose)
        {
            Say1();
        }
        if (SaveData.Instance.isGetRob)
        {
            Say2();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void Say1()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(Rose)/* && isEndmiss == true*/)
        {
            flowChart.ExecuteBlock(Rose);

        }
    }

    void Say2()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(Rob)/* && isEndmiss == true*/)
        {
            flowChart.ExecuteBlock(Rob);

        }
    }
}
