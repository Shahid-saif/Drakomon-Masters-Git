using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GlobalMessage : MonoBehaviour
{
    public bool IsInfo = true;
    public bool IsItem = false;
    public string MessageTitle;
    [TextArea(2,10)]
    public string MessageMainText;

    [Header("item message")]
    public Item item;
    public int Count = 3;


    public UnityEvent Yes_Event;
    public UnityEvent No_Event;


    public void SetYesEvent()
    {
        Yes_Event?.Invoke();
    }

    public void SetNoEvent()
    {
        No_Event?.Invoke();
    }

}
