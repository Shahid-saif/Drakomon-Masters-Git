using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Fight", menuName = "Fight/Fight data")]
public class FightScene_FightDataHolder : ScriptableObject
{
    [Header("Fight settings")]
   public FightScene_FightDataHolder Prev;
    public GameObject EnemyHuman;
    public List<string> EnemyMonstersIDs;
    public bool CanExit;
    public string FightSceneName;
    public string ReturnSceneName;
    public int FightRounds;
   // public bool OneTimeFight;
    public bool Used;
}
