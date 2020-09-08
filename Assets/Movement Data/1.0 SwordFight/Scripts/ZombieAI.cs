using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public enum WanderType { Random, Waypoint };


    public PlayerController player;
    public WanderType wanderType = WanderType.Random;
    public float attackDistance = 1.5f;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    public float fov = 120f;
    public float viewDistance = 10f;
    public float wanderRadius = 7f;
    public Transform[] waypoints; //Array of waypoints is only used when waypoint wandering is selected
    [SerializeField]
    private bool isAware = false;
    private Vector3 wanderPoint;
    private NavMeshAgent agent;
    private Renderer renderer;
    private int waypointIndex = 0;
    private Animator animator;


    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        animator = GetComponentInChildren<Animator>();
        wanderPoint = RandomWanderPoint();
        player = FindObjectOfType<PlayerController>();
        GameManager.OnPlayerDead += OnTargetDead;
        HandCollider.enabled = false;
    }

    private void OnTargetDead()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Aware", false);
        isAware = false;
       
    }
    private void OnDisable()
    {
        GameManager.OnPlayerDead -= OnTargetDead;
    }
    public Collider HandCollider;
    public void OnStartAttack() {
        HandCollider.enabled = true;
    }

    public void OnEndAttack() {
        HandCollider.enabled = false;

    }
    public void Update()
    {
        if (isAware)
        {
            agent.SetDestination(player.transform.position);
            
            //renderer.material.color = Color.red;
            if (agent.remainingDistance < attackDistance)
            {
                // attack
                animator.SetBool("Attack", true);
                agent.speed = 0;
                agent.velocity = Vector3.zero;
            }
            else
            {
                // chase
                animator.SetBool("Aware", true);
                animator.SetBool("Attack", false);
                agent.speed = chaseSpeed;
            }
        }
        else
        {
            SearchForPlayer();
            Wander();
            animator.SetBool("Aware", false);
            agent.speed = wanderSpeed;
            //renderer.material.color = Color.blue;
            
        }
    }

    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov / 2f)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < viewDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, player.transform.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                    }
                }
            }
        }
    }

    public void OnAware()
    {
        isAware = true;
    }

    public void Wander()
    {
        if (wanderType == WanderType.Random)
        {
            if (Vector3.Distance(transform.position, wanderPoint) < 2f)
            {
                wanderPoint = RandomWanderPoint();
            }
            else
            {
                agent.SetDestination(wanderPoint);
            }
        }
        else
        {
            //Waypoint wandering
            if (waypoints.Length >= 2)
            {
                if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) < 2f)
                {
                    if (waypointIndex == waypoints.Length - 1)
                    {
                        waypointIndex = 0;
                    }
                    else
                    {
                        waypointIndex++;
                    }
                }
                else
                {
                    agent.SetDestination(waypoints[waypointIndex].position);
                }
            }
            else
            {
                Debug.LogWarning("Please assign more than 1 waypoint to the AI: " + gameObject.name);
            }
        }
    }

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (UnityEngine.Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }

    public void Dead() {
        animator.Rebind();
        animator.SetTrigger("Dead");
        agent.velocity = Vector3.zero;
        agent.speed = 0;
        agent.isStopped = true;

        GetComponent<Collider>().enabled = false;
        GameManager.Instance.EnemyKilled();
        Destroy(gameObject, 5f);
    }
}
