using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Run time Data", menuName ="RunTime/Player position")]
public class RunTimeData : ScriptableObject
{
    public bool BackFromRoom = false;
    public Vector3 position;
}
