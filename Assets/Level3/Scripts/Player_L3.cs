using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_L3 : Player
{
    //流程
    LevelManager_L3 l3Manager;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        l3Manager = (LevelManager_L3)levelManager;
    }

    protected override void Update()
    {
        base.Update();
        //特殊交互物品，在子类中写
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(INTERACTIVEITEM)) {
            switch (coll.name) {
                case "Illusion":
                    if (froze) return;
                    if (l3Manager.isFinishPipeGame) {
                        if (l3Manager.isScanBrain) return;
                        else {
                            Froze();
                            if (l3Manager.isPutBrain) {
                                flowChart.ExecuteBlock("Illusion_放置大脑");
                            } else {
                                flowChart.ExecuteBlock("Illusion_未放置大脑");
                            }
                        }
                        return;
                    }
                    Froze();
                    flowChart.ExecuteBlock("Illusion");
                    break;
                case "Maze1":
                    if (l3Manager.isFirstGetInMaze) {
                        l3Manager.FirstInteractMaze(0);
                    } else {
                        l3Manager.OpenMaze(0);
                    }
                    break;
                case "Maze2":
                    if (l3Manager.isFirstGetInMaze) {
                        l3Manager.FirstInteractMaze(1);
                    } else {
                        l3Manager.OpenMaze(1);
                    }
                    break;
                case "Maze3":
                    if (l3Manager.isFirstGetInMaze) {
                        l3Manager.FirstInteractMaze(2);
                    } else {
                        l3Manager.OpenMaze(2);
                    }
                    break;

                case "仪器":
                    if (!l3Manager.isGetQueue) {
                        Froze();
                        flowChart.ExecuteBlock("仪器");
                    } else {
                        if (froze) return;//避免反复开启questionOpenMinerGame界面
                        if (l3Manager.minerGame.level == 4) {//第四关前询问
                            Froze();
                            if (!l3Manager.isGetTable) {
                                flowChart.ExecuteBlock("V植入_没登记表");
                            } else if (!l3Manager.isFinTable) {
                                flowChart.ExecuteBlock("V植入_登记表错误");
                            } else {
                                flowChart.ExecuteBlock("V植入");
                            }
                            return;
                        }
                        if (l3Manager.minerGame.level == 5) { return; }//矿工游戏完成不开

                        l3Manager.questionOpenMinerGame.SetActive(true);
                        Froze();
                    }
                    break;
                case "电脑":
                    if(froze) return;
                    Froze();
                    l3Manager.OpenComputer();
                    break;
                case "结局门":
                    Froze();
                    l3Manager.Right2079();
                    break;
            }
        }

    }


    protected override void Talk()
    {
        if (froze) return;

        switch (coll.name) {
            case "医生":
                if (!l3Manager.isGetQueue) {
                    flowChart.ExecuteBlock("医生");
                } else {
                    flowChart.ExecuteBlock("医生_获得排队人");
                }
                break;
            case "ROBO":
                if (l3Manager.isPassRobo) return;
                flowChart.ExecuteBlock("ROBO");
                break;
        }

        base.Talk();
    }

}
