using BagDataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickedItem : MonoBehaviour
{
    [SerializeField]public string itemName;

    public void PickIt()
    {
        gameObject.SetActive(false);
    }
}
