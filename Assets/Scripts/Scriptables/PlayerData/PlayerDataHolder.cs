using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player data")]
public class PlayerDataHolder : ScriptableObject
{
    public string PlayerId;
    public string PlayerName;
    public int PlayerMony;
    public string PlayerLastScene;


    [Header("Save Data")]
    public int PlayerSaveMony;
}
