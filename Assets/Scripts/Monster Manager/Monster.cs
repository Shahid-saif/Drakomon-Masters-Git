using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [Header("Ref properties")]
    public Animator Anim;

    public PetAi_Controller Controller;
    public NavMeshAgent agent;

    [Header("Monsters properties")]
    public MonsterDataHolder DataHolder;
    public GameObject MonsterObj;
    //public GameObject MonsterObj_Fight;
    public bool IsSelected=false;
  //  public bool IsPatrolling=false;

    [Header("Attack Data Holder")]
    public FightAttackDataHolder[] FightDataHolder;
    public Vector3 NormalScale;
    public Vector3 FightScale;

    [Header("Monsters UI Elements properties")]
    public List<GameObject> UiMonsterSelect;
    public List<GameObject> UiMonsterSkills;
    private void Awake()
    {
        NormalScale = transform.localScale;
        FightScale = transform.localScale * 2;
        Controller = GetComponent<PetAi_Controller>();
        agent = GetComponent<NavMeshAgent>();
    }

}
