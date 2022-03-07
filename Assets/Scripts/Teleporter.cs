using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private GameObject close;
    [SerializeField] private GameObject open;
    [SerializeField] private Transform topos;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player")) {
    //        close.SetActive(false);
    //        open.SetActive(true);
    //        collision.transform.position = topos.position;
    //    }
    //}

    public void Teleport(Transform player)
    {
        close.SetActive(false);
        open.SetActive(true);
        player.position = topos.position;
    }
}
