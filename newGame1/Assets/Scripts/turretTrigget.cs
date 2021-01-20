using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretTrigget : MonoBehaviour
{
    private bool _isTriggered = false;
    private Transform player;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            _isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            _isTriggered = false;
        }
    }

    private void FixedUpdate()
    {

    }
     
    private void Update()
    {
        if (_isTriggered)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotationGO = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationGO, Time.deltaTime * rotationSpeed);
        }
    }

}
