using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetAi_Controller : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] NavMeshAgent Agent;

    [Space]
    [Header("Controle")]
    [SerializeField] GameManager Gmanager;   
    [SerializeField] float movespeed=2f;
    [SerializeField] Transform player_follow;

    [Space]
    [Header("Animations")]
    [SerializeField] Animator anim;
    [Space]
    [Header("Reset")]
    [SerializeField] float RestDist = 2f;
    void Start()
    {
        anim = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Gmanager = GameManager.Instance;
        player_follow = Gmanager.Player_follow;
        NavMeshHit navHit;

        if (NavMesh.FindClosestEdge(player_follow.position, out navHit, 0))
        {
            Agent.transform.position = navHit.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(player_follow.position);
        anim.SetFloat("Move",Agent.velocity.magnitude);

        if (Agent.remainingDistance> RestDist)
        {
            transform.position = player_follow.position;
            //NavMeshHit navHit;
            //if (NavMesh.FindClosestEdge(player_follow.position, out navHit,0))
            //{
            //    Agent.transform.position = navHit.position;
               
            //}

        }
    }
}
