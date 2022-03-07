using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject close;
    [SerializeField] private GameObject open;
    [SerializeField] private Transform topos;

   
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player")&&Input.GetKeyDown(KeyCode.E)) {
    //        close.SetActive(false);
    //        open.SetActive(true);
    //        collision.transform.position = topos.position;
    //    }
    //}

    public void GetIn(Transform player)
    {
        close.SetActive(false);
        open.SetActive(true);
        player.position = topos.position;
    }
}
