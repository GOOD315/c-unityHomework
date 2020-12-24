using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyFpsController;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private NavMeshAgent agent;

    private Transform playerPos;
    private float stopDistance = 3;
    private float attackDistance = 8;

    [SerializeField] private List<Transform> Waypoints = new List<Transform>();

    public int Health { get => _health; set => _health = value; }

    void Awake()
    {
        Health = 100;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;
        playerPos = FindObjectOfType<MyFpsController2>().transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerPos.position);
        if (Health <= 0)
        {
            Health = 0;
            Destroy(gameObject);
        }
    }

    public void Damage(int dmg)
    {
        _health -= dmg;
    }
}
