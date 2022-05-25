using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interlude3 : Player
{
    public LevelManager_Interlude3 m_levelManager;

    protected override void Talk()
    {
        if (froze) return;

        switch (coll.name) {
            case "ROBO":
                flowChart.ExecuteBlock("ROBO");
                break;
        }

        base.Talk();
    }
}
