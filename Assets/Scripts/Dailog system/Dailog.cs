using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dailog : MonoBehaviour
{

    [Header("Dialgos")]
    public List<Dailog_class> Dialogs;
    public Transform DialogPosition;
    public UnityEvent StartEvent;
    public UnityEvent EndEvent;
    public bool OnScreen = false;
    public bool HasNoName = false;



}
