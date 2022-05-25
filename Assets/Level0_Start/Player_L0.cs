using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_L0 : Player
{
    public StartManager l0Manager;
    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    protected override void Update()
    {
        base.Update();
        //���⽻����Ʒ����������д
        if (froze) return;
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(INTERACTIVEITEM)) {
            switch (coll.name) {
                case "��Ļ":
                    if (froze) return;
                    Froze();
                    l0Manager.flowChart.ExecuteBlock("��Ļ");
                    break;
                case "Illusion":
                    if (froze) return;
                    Froze();
                    l0Manager.flowChart.ExecuteBlock("Illusion");
                    break;
            }
        }

    }


    protected override void Talk()
    {
        if (froze) return;

        switch (coll.name) {
            case "ROBO":
                l0Manager.flowChart.ExecuteBlock("ROBO");
                break;
        }

        base.Talk();
    }

}
