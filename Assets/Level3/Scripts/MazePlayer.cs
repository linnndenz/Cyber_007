using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePlayer : MonoBehaviour
{
    //迷宫计数
    public SpriteRenderer cntSr;
    public Sprite[] cntSprites;

    public bool froze = false;
    public void Froze() => froze = true;
    public void DeFroze() => froze = false;
    public LevelManager_L3 levelManager;
    public int mazeIndex;//012
    public GameObject[] yearPages;

    LayerMask mazeWallLayer;

    Animator animator;

    int cnt0, cnt1, cnt2;
    private void Start()
    {
        mazeWallLayer = 1 << LayerMask.NameToLayer("MazeWall");
        animator = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (froze) return;
        //出口
        if (collision.name == "出口") {
            if (mazeIndex == 0 && cnt0 < 2
                || mazeIndex == 1 && cnt1 < 2
                || mazeIndex == 2 && cnt2 < 4) {
                levelManager.flowChart.ExecuteBlock("迷宫_未集齐");
            } else {
                yearPages[mazeIndex].gameObject.SetActive(true);
            }
            Froze();
        }

        //maze1
        if (collision.name == "登记表") {
            LevelManager.Instance.audioManager.PlaySE(2);
            levelManager.GetTable();
            collision.gameObject.SetActive(false);
            cnt0++;
            cntSr.sprite = cntSprites[cnt0];
        } else if (collision.name == "排队人") {
            LevelManager.Instance.audioManager.PlaySE(2);
            levelManager.GetQueue();
            collision.gameObject.SetActive(false);
            cnt0++;
            cntSr.sprite = cntSprites[cnt0];
        }

        //maze2
        if (collision.name == "日记上半") {
            LevelManager.Instance.audioManager.PlaySE(2);
            levelManager.GetDiary1();
            collision.gameObject.SetActive(false);
            cnt1++;
            cntSr.sprite = cntSprites[cnt1];
        } else if (collision.name == "日记下半") {
            LevelManager.Instance.audioManager.PlaySE(2);
            levelManager.GetDiary2();
            collision.gameObject.SetActive(false);
            cnt1++;
            cntSr.sprite = cntSprites[cnt1];
        }

        //maze3
        if (collision.name == "报告1") {
            LevelManager.Instance.audioManager.PlaySE(2);
            levelManager.GetFile(0);
            collision.gameObject.SetActive(false);
            cnt2++;
            cntSr.sprite = cntSprites[cnt2];
        } else if (collision.name == "报告2") {
            LevelManager.Instance.audioManager.PlaySE(2);
            levelManager.GetFile(1);
            collision.gameObject.SetActive(false);
            cnt2++;
            cntSr.sprite = cntSprites[cnt2];
        } else if (collision.name == "报告3") {
            LevelManager.Instance.audioManager.PlaySE(2);
            levelManager.GetFile(2);
            collision.gameObject.SetActive(false);
            cnt2++;
            cntSr.sprite = cntSprites[cnt2];
        } else if (collision.name == "报告4") {
            LevelManager.Instance.audioManager.PlaySE(2);
            levelManager.GetFile(3);
            collision.gameObject.SetActive(false);
            cnt2++;
            cntSr.sprite = cntSprites[cnt2];
        }
    }

    void Update()
    {
        if (froze) return;
        Move();
    }

    //手动关闭输入年份页面时，左退一步
    public void BackOneGrid()
    {
        animator.SetBool("isUp", false);
        animator.SetBool("isDown", false);
        animator.SetBool("isRight", true);
        animator.transform.localScale = new Vector3(1, 1, 1);
        transform.position += new Vector3(-0.296f, 0, 0);
    }

    float intervalTime = 0.1f;
    float timer = -1;
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            animator.SetBool("isUp", true);
            animator.SetBool("isDown", false);
            animator.SetBool("isRight", false);
            animator.transform.localScale = new Vector3(1, 1, 1);
            timer = -1;
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            animator.SetBool("isUp", false);
            animator.SetBool("isDown", true);
            animator.SetBool("isRight", false);
            animator.transform.localScale = new Vector3(1, 1, 1);
            timer = -1;
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            animator.SetBool("isUp", false);
            animator.SetBool("isDown", false);
            animator.SetBool("isRight", true);
            animator.transform.localScale = new Vector3(-1, 1, 1);
            timer = -1;
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            animator.SetBool("isUp", false);
            animator.SetBool("isDown", false);
            animator.SetBool("isRight", true);
            animator.transform.localScale = new Vector3(1, 1, 1);
            timer = -1;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            if (timer < 0) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.3f, mazeWallLayer);
                if (hit.collider == null) {
                    transform.position += new Vector3(0, 0.275f, 0);
                }
                timer = intervalTime;
            } else {
                timer -= Time.deltaTime;
            }

        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            if (timer < 0) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, mazeWallLayer);
                if (hit.collider == null) {
                    transform.position += new Vector3(0, -0.275f, 0);
                }
                timer = intervalTime;
            } else {
                timer -= Time.deltaTime;
            }
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            if (timer < 0) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 0.3f, mazeWallLayer);
                if (hit.collider == null) {
                    transform.position += new Vector3(-0.296f, 0, 0);
                }
                timer = intervalTime;
            } else {
                timer -= Time.deltaTime;
            }
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            if (timer < 0) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.3f, mazeWallLayer);
                if (hit.collider == null) {
                    transform.position += new Vector3(0.296f, 0, 0);
                }
                timer = intervalTime;
            } else {
                timer -= Time.deltaTime;
            }
        }

    }
}
