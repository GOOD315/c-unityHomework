using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField] private bool gateOpen;
    [SerializeField] private float gateSpeed;
    private Vector3 openPos;
    private Vector3 closePos;


    // Start is called before the first frame update
    void Start()
    {
        closePos = transform.position;
        openPos = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (gateOpen)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + gateSpeed, transform.position.z);
        }
    }
}
