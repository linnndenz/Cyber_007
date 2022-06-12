using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class L4_Click : MonoBehaviour
{
    public UnityEvent clickEvent;
    private void OnMouseDown()
    {
        clickEvent.Invoke();
    }
}
