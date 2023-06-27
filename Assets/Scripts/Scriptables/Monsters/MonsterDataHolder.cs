using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Monsters", menuName = "Mosnters/Monster data")]
public class MonsterDataHolder : ScriptableObject
{
    [Header("Data")]
    public string ID;
    public string Name;
    public bool IsActive=false;
    public bool Male = true;
    [Space]
    [Header("states info")]
    public int HP;
    public int XP;
    public int maxHP;
    public int MaxXP;
    public int Level=1;

    [Space]
    [Header("Monster Type")]
    public bool IsFire;
    public bool IsGreen;
    public bool IsWater;

    [Space]
    [Header("states info")]
    public int EggRes;
    public int Defense;
    public int Speed;

    [Space]
    [Header("Ref")]
    public Sprite MonsterImg;
    public GameObject MosnterPrefab;

    [Space]
    [Header("saveGame")]
    public string SaveName;
    public int saveHp;
    public int saveXp;
    public int SaveLevel;
    public int SaveIsActive;
}
