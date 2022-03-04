using BagDataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public int Index { get; private set; }

    public bool IsChosen { get; set; }

    void Start()
    {
        Index = transform.GetSiblingIndex();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsChosen = !IsChosen;
        BagUI.ClickSlot(Index);
    }
}
