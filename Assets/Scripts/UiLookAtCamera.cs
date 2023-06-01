using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLookAtCamera : MonoBehaviour
{
    GameManager Gmanager;

    void Start()
    {
        Gmanager = GameManager.Instance;
        Gmanager.OnUpdate.AddListener(OnProcess);
    }

    public void OnProcess()
    {
    transform.rotation= Quaternion.LookRotation(transform.position- Gmanager.MainCam.transform.position);
    }
}
