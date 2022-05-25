using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class GameObjectButton : MonoBehaviour
{
    public UnityEvent unityEvent;
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            unityEvent.Invoke();
        } else {
            print("µã»÷ÔÚuiÉÏ");
        }
    }
}
