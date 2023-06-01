using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerInUIView : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float RotateSpeed;

 
    void Update()
    {
        transform.Rotate(0,SimpleInput.GetAxis("Horizontal")* RotateSpeed, 0);
    }
}
