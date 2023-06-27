using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Items", menuName = "Items/Items data")]
public class ItemDataHolder : ScriptableObject
{
    public string ID;
    public string ItemName;
    [TextArea(0,3)]
    public string ItemDiscription;
    public int ItemCount;
 
    public Sprite ItemImg;

    [Header("Item Type")]
    public bool IsPostion;
    public bool IsKey;
    public bool IsBook;
    public bool IsEgg;

    [Header("Item Using")]
    public bool Usable;
    public bool UsableForMonsters;
    public bool NotUsable;
    [Header("Item effect")]
    public float EffectValue;
    public int EffectHP;
    public int EffectXP;

    [Header("Item Shop")]
    public int Price;

    [Space]
    [Header("Items Egg Effect")]
    public int EggEffect;


    [Space]
    [Header("Items SaveData")]
    public string SaveDataName;
    public int SaveItemCount;

 


}
