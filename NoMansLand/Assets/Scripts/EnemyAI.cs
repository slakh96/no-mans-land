using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer, whatIsObstruction;

    [SerializeField] private float playerAcceleration = 100f;
    [SerializeField] private float playerSpeed = 45f;

    private float detectedSpeed = 100f;

    // Patroling
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    // Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    
    // States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    
    // Sight angle
    public float angle = 35f;
    public bool hasSeenPlayer;
    

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.acceleration = playerAcceleration;
        agent.speed = playerSpeed;
        hasSeenPlayer = false;
    }

    private bool CheckPlayerInSightRange()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.position, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, whatIsObstruction))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        else if (hasSeenPlayer)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        // check for sight and attack range
        playerInSightRange = CheckPlayerInSightRange();
        hasSeenPlayer = playerInSightRange;
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
            transform.LookAt(walkPoint);
            agent.SetDestination(walkPoint);
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(
            transform.position.x + randomX, 
            transform.position.y, 
            transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        transform.LookAt(player);
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
