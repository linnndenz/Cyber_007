using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interlude2 : Player
{
    public LevelManager_Interlude2 m_levelManager;
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
                case "Illusion":
                    Froze();
                    m_levelManager.audioManager.PlaySE(0);
                    m_levelManager.flowChart.ExecuteBlock("Illusion");
                    break;
                case "��Ļ":
                    Froze();
                    m_levelManager.flowChart.ExecuteBlock("��Ļ");
                    break;
            }
        }

    }


    protected override void Talk()
    {
        if (froze) return;

        switch (coll.name) {
            case "ROBO":
                m_levelManager.flowChart.ExecuteBlock("ROBO");
                break;
        }

        base.Talk();
    }
}
