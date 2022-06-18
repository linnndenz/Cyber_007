using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class beibao1 : MonoBehaviour
{
    public string Rose;
    public string Rob;
    // Start is called before the first frame update
    void Start()
    {
        
}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Say1();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Say2();
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
