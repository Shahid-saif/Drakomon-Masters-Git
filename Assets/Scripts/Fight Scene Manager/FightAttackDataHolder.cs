using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Fight", menuName = "Fight/Attack data")]
public class FightAttackDataHolder : ScriptableObject
{
    [Header("Info")]
    public string AttackID;
    public int AttackCount;
    public string Animation;
    public Sprite AttackImg;

    [Header("Attack")]
    public int AttackIndex;
    public bool MoveToEnemy;
    public bool IsProjectile;
    public int AttackPower;
    public GameObject effect;
    public GameObject OnMonstereffect;

    [Header("Active")]
    public int ActiveLevel;
    public bool IsActive;

    [Header("Timing")]
    public float timeBeforeAttack;
    public float timeAfterAttack;


    [Header("Sounds")]
    public string AttackSound;

}
