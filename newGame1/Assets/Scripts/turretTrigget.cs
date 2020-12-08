using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretTrigget : MonoBehaviour
{
    private bool _isTriggered = false;
    private GameObject player;
    [SerializeField] private GameObject playerTrue;

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
            // _isTriggered = false;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(playerTrue.transform.position);
    }

    private void Update()
    {
        /*  if (_isTriggered)
          {
              transform.rotation = Quaternion.LookRotation(player.transform.position);
          }*/
    }

}
