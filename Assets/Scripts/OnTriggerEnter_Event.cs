using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnter_Event : MonoBehaviour
{
    GameManager Gmanager;
    [SerializeField] UnityEvent OnEnterEven;
    private void Start()
    {
        Gmanager = GameManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform==Gmanager.Player)
        {
            OnEnterEven?.Invoke();
        }
    }
}
