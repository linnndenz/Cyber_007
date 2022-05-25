using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoGame : MonoBehaviour
{
    public List<PhotoSprites> backBlocks;
    public List<PhotoSprites> frontBlocks;
    public Transform blockParent;
    private Image[,] blocks = new Image[4, 4];
    private bool[,] turns = new bool[4, 4];

    public GameObject vPhoto;
    public Flowchart flowChart;

    private void Start()
    {
        for (int i = 0; i < blocks.GetLength(0); i++) {
            for (int j = 0; j < blocks.GetLength(1); j++) {
                blocks[i, j] = blockParent.GetChild(i * 4 + j).GetComponent<Image>();
                blocks[i, j].type = Image.Type.Simple;
            }
        }

        turns[1, 0] = true;
        turns[1, 2] = true;
        turns[1, 3] = true;
        turns[2, 3] = true;
        turns[3, 1] = true;

        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                ChangeSprite(turns[i, j], i, j);
            }
        }
    }


    //µã»÷¿ì
    public void CickBlock(int index)
    {
        if (isWin) return;
        int row = index / 4;
        int line = index % 4;

        turns[row, line] = !turns[row, line];
        ChangeSprite(turns[row, line], row, line);
        if (row - 1 >= 0) {
            turns[row - 1, line] = !turns[row - 1, line];
            ChangeSprite(turns[row - 1, line], row - 1, line);
        }
        if (row + 1 < 4) {
            turns[row + 1, line] = !turns[row + 1, line];
            ChangeSprite(turns[row + 1, line], row + 1, line);
        }
        if (line - 1 >= 0) {
            turns[row, line - 1] = !turns[row, line - 1];
            ChangeSprite(turns[row, line - 1], row, line - 1);
        }
        if (line + 1 < 4) {
            turns[row, line + 1] = !turns[row, line + 1];
            ChangeSprite(turns[row, line + 1], row, line + 1);
        }

        CheckWin();

    }

    //ÇÐ»»ÕÕÆ¬
    private void ChangeSprite(bool b, int r, int l)
    {
        if (b) {
            blocks[r, l].sprite = frontBlocks[r].sprites[l];
        } else {
            blocks[r, l].sprite = backBlocks[r].sprites[l];

        }
    }

    bool isWin;
    private void CheckWin()
    {
        for (int i = 0; i < turns.GetLength(0); i++) {
            for (int j = 0; j < turns.GetLength(1); j++) {
                if (turns[i, j] == false) return;
            }
        }

        //win
        isWin = true;
        StartCoroutine(FinishGame());
        IEnumerator FinishGame()
        {
            yield return new WaitForSeconds(1.5f);
            vPhoto.SetActive(true);
            gameObject.SetActive(false);
            flowChart.ExecuteBlock("ÐÞ¸´Íê±Ï");
        }
    }
    

}

[Serializable]
public class PhotoSprites
{
    public Sprite[] sprites;
}
