using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/QustDataHolder")]
public class Quest_DataHolder : ScriptableObject
{
    public string Id;
    public string QuestName;
    [TextArea(2,10)]
    public string QuestDescription;
    public bool IsCompleted;
    public bool IsActive;

    [Space]
    [Header("Data Save")]
    public string SaveName;
    public int IsCompleted_Data;
    public int IsActive_Data;
}
