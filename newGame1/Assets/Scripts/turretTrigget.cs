using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretTrigget : MonoBehaviour
{
    private bool _isTriggered = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

}
