using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First2071 : MonoBehaviour
{
    public Player player;
    public Flowchart flowchart;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            player.Froze();
            flowchart.ExecuteBlock("½øÈë2071");
            gameObject.SetActive(false);
        }
    }
}
