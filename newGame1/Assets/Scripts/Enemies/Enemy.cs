using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyFpsController;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Spawner enemySpawner;

    private bool isShooting;
    private bool isMoving;
    private bool isDead = false;

    private Animator animator;

    private Transform playerPos;
    private float stopDistance = 3;
    private float attackDistance = 8;
    private float rotationSpeed = 8;

    [SerializeField] private List<Transform> Waypoints = new List<Transform>();

    public int Health { get => _health; set => _health = value; }

    void Awake()
    {
        Health = 100;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;
        playerPos = FindObjectOfType<MyFpsController2>().transform;

        isShooting = false;
        isMoving = true;

        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<Spawner>();

        animator = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Health = 0;
            enemySpawner.enemyCounter -= 1;
            isDead = true;
            Destroy(gameObject);
        }
        if (!isDead)
        {
            if (Vector3.Distance(gameObject.transform.position, playerPos.transform.position) <= 10)
            {
                animator.SetBool("isShooting", true);
                animator.SetBool("isMoving", false);
                agent.isStopped = true;
                isShooting = true;
                isMoving = false;
            }
            else
            {
                animator.SetBool("isShooting", false);
                animator.SetBool("isMoving", true);
                agent.isStopped = false;
                isShooting = false;
                isMoving = true;
            }

            if (isShooting)
            {
                LookAt(playerPos);
            }
            if (isMoving)
            {
                agent.SetDestination(playerPos.position);
            }
        }
    }

    public void Damage(int dmg)
    {
        _health -= dmg;
    }

    private void LookAt(Transform aim)
    {
        Vector3 direction = (aim.position - transform.position).normalized;
        Quaternion lookRotationGO = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationGO, Time.deltaTime * rotationSpeed);
    }
}
