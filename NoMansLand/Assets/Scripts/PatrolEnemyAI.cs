using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    private float playerAcceleration = 100f;
    private float playerSpeed = 45f;

    private float detectedSpeed = 100f;

    //Patroling
    public GameObject patrolObject;
    private Transform[] patrolPoints;
    private int patrolIndex = 0;
    private Vector3 walkPoint;
    private bool walkPointSet;
    
    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    
    // States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.acceleration = playerAcceleration;
        agent.speed = playerSpeed;
        patrolPoints = patrolObject.transform.GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        // check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
    }

    private void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 horizontalDistance = new Vector3(walkPoint.x, transform.position.y, walkPoint.z);
        Vector3 distanceToWalkPoint = transform.position - horizontalDistance;
        if (distanceToWalkPoint.magnitude < 5f)
        {
            walkPointSet = false;
            agent.speed = playerSpeed;
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    private void SearchWalkPoint()
    {
        float newX = patrolPoints[patrolIndex].position.x;
        float newZ = patrolPoints[patrolIndex].position.z;

        walkPoint = new Vector3(
            newX, 
            transform.position.y, 
            newZ);
        if (Physics.Raycast(walkPoint, -transform.up, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        agent.speed = detectedSpeed;
    }

    private void AttackPlayer()
    {
        // stop enemy from moving
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack code goes here
            
            
            
            // Attack code finishes here 
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
