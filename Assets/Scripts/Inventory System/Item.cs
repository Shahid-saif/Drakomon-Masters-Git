using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public ItemDataHolder DataHolder;
    public GameObject ItemSceneObj;

   // public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemCount;

    public UnityEvent ItemEffect;
}
