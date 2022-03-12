using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject close;
    [SerializeField] private GameObject open;
    [SerializeField] private Transform topos;

    public void GetIn(Transform player)
    {
        close.SetActive(false);
        open.SetActive(true);
        player.position = topos.position;
    }
}
