using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public AudioClip bagButtonSe;
    public GameObject outline;
    public Animator bagAnimator;
    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.SetActive(true);
    }
    public bool isOpen;
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.Instance.audioManager.PlaySE(bagButtonSe);
        isOpen = !isOpen;
        bagAnimator.SetBool("open", isOpen);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.SetActive(false);
    }
}
