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

    //点击每个pipeButton之后需要回调检查是否完成
    public void CheckFinish()
    {
        if (isFinish) return;

        for (int i = 0; i < pipes.Length; i++) {
            if (!pipes[i].correctIndexs.Contains(pipes[i].currIndex)) {
                return;
            }
        }

        //关卡完成
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
