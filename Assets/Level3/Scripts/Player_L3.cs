using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_L3 : Player
{
    //����
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
        //���⽻����Ʒ����������д
        if (Input.GetKeyDown(KeyCode.E) && coll && coll.CompareTag(INTERACTIVEITEM)) {
            switch (coll.name) {
                case "Illusion":
                    if (froze) return;
                    if (l3Manager.isFinishPipeGame) {
                        if (l3Manager.isScanBrain) return;
                        else {
                            Froze();
                            if (l3Manager.isPutBrain) {
                                flowChart.ExecuteBlock("Illusion_���ô���");
                            } else {
                                flowChart.ExecuteBlock("Illusion_δ���ô���");
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

                case "����":
                    if (!l3Manager.isGetQueue) {
                        Froze();
                        flowChart.ExecuteBlock("����");
                    } else {
                        if (froze) return;//���ⷴ������questionOpenMinerGame����
                        if (l3Manager.minerGame.level == 4) {//���Ĺ�ǰѯ��
                            Froze();
                            if (!l3Manager.isGetTable) {
                                flowChart.ExecuteBlock("Vֲ��_û�ǼǱ�");
                            } else if (!l3Manager.isFinTable) {
                                flowChart.ExecuteBlock("Vֲ��_�ǼǱ����");
                            } else {
                                flowChart.ExecuteBlock("Vֲ��");
                            }
                            return;
                        }
                        if (l3Manager.minerGame.level == 5) { return; }//����Ϸ��ɲ���

                        l3Manager.questionOpenMinerGame.SetActive(true);
                        Froze();
                    }
                    break;
                case "����":
                    if(froze) return;
                    Froze();
                    l3Manager.OpenComputer();
                    break;
                case "�����":
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
            case "ҽ��":
                if (!l3Manager.isGetQueue) {
                    flowChart.ExecuteBlock("ҽ��");
                } else {
                    flowChart.ExecuteBlock("ҽ��_����Ŷ���");
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
