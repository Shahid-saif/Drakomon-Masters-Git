using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dailog_class 
{
    public string Name;
    [TextArea(1,10)]
    public string MainText;

    public UnityEvent EndSentencesEvetn;
}
