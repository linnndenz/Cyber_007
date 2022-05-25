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
        //特殊交互物品，在子类中写
        if (froze) return;
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(INTERACTIVEITEM)) {
            switch (coll.name) {
                case "屏幕":
                    if (froze) return;
                    Froze();
                    l0Manager.flowChart.ExecuteBlock("屏幕");
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
