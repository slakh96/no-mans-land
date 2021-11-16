using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer, whatIsObstruction;

    private float playerAcceleration = 100f;
    private float playerSpeed = 45f;

    private float detectedSpeed = 80f;

    //Patroling
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    
    // States
    public float sightRange;
    public float angle;
    public float attackRange = 1f;
    private bool playerInSightRange, playerInAttackRange;
    private float patrolAngle = 105f;
    private float alertAngle = 360f;
    private float patrolSightRange = 100f;
    private float alertSightRange = 120f;
    public bool hasSeenPlayer;
    private bool isPlaying;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.acceleration = playerAcceleration;
        agent.speed = playerSpeed;
        sightRange = patrolSightRange;
    }

    private void Update()
    {
        playerInSightRange = CheckPlayerInSightRange();
        hasSeenPlayer = playerInSightRange;
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (hasSeenPlayer) ChasePlayer();
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
            agent.speed = playerSpeed;
            angle = patrolAngle;
            sightRange = patrolSightRange;
            walkPointSet = false;
        }
    }
    private bool CheckPlayerInSightRange()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Vector3 facingDirection = (walkPoint - transform.position).normalized;
            if (Vector3.Angle(facingDirection, directionToTarget) < angle / 2)
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
        angle = alertAngle;
        sightRange = alertSightRange;
        agent.SetDestination(player.position);
        if (!isPlaying)
        {
            StartCoroutine (playSound("AlienChasing1", 20));
        }
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
    
    IEnumerator playSound(string name, int interval)
    {
        isPlaying = true;
        Sound s = FindObjectOfType<AudioManager>().Find(name);
        s.source.Play();
        yield return new WaitForSeconds (interval);
        s.source.Stop();
        isPlaying = false;
    }
}
