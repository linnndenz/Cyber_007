using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Transform door;

    [SerializeField] private GameObject close;
    [SerializeField] private GameObject open;
    [SerializeField] private Transform topos;


    private void Start()
    {
        door = transform.Find("це");
    }

    public void GetIn(Transform player)
    {
        Player.Instance.Froze();
        LevelManager.Instance.audioManager.PlaySE(3);
        door.DOMoveX(door.position.x + 2, 0.5f).OnComplete(
            () => {
                close.SetActive(false);
                open.SetActive(true);
                player.position = topos.position;
                door.position = door.position - new Vector3(2, 0, 0);
                Player.Instance.DeFroze();
            });


    }
}
