using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FightEnemyMonster : MonoBehaviour
{
    [Header("Ref properties")]
    public string Id;
    public Animator Anim;
    [Header("Monsters properties")]
    public MonsterDataHolder DataHolder;
    public GameObject MonsterObj;

    [Header("Attack Data Holder")]
    public FightAttackDataHolder[] FightDataHolder;

    [Header("Monsters Values properties")]
    public int Health;
    public int MaxHealth = 100;
    public int Level;
    
   


}
