using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    [Header("Input Var")]

    public float Horizontal;
    public float Vertical;
    public bool Intract;
    public bool Bike;

  public  void OnProcess()
    {
        Horizontal = SimpleInput.GetAxis("Horizontal");
        Vertical = SimpleInput.GetAxis("Vertical");
        Intract = Input.GetButton("Fire1");
        Bike = Input.GetButtonDown("Fire2");
    }
}
