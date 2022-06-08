using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeGame : MonoBehaviour
{
    public bool isFinish;
    public Transform pipeParent;
    PipeButton[] pipes;
    public LevelManager_L3 levelManager;

    void Start()
    {
        pipes = pipeParent.GetComponentsInChildren<PipeButton>();
    }

    //���ÿ��pipeButton֮����Ҫ�ص�����Ƿ����
    public void CheckFinish()
    {
        if (isFinish) return;

        for (int i = 0; i < pipes.Length; i++) {
            if (!pipes[i].correctIndexs.Contains(pipes[i].currIndex)) {
                return;
            }
        }

        //�ؿ����
        isFinish = true;
        levelManager.audioManager.PlaySE(6);
        StartCoroutine(nameof(FinishPipeGame));
    }

    public IEnumerator FinishPipeGame()
    {
        yield return new WaitForSeconds(1);
        levelManager.FinishPipeGame();
        gameObject.SetActive(false);
    }
}
