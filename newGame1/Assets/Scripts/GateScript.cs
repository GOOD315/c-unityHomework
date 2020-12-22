using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public bool gateOpen;
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
            openGate();
        }
        else
        {
            closeGate();
        }
    }

    public void openGate()
    {
        if (transform.position.y < 10)
        {
            print("openGate");
            transform.position = new Vector3(transform.position.x, transform.position.y + (gateSpeed * Time.deltaTime), transform.position.z);
        }
    }

    void closeGate()
    {
        if (transform.position.y >= 10)
        {
            print("closeGate");
            transform.position = new Vector3(transform.position.x, transform.position.y - (gateSpeed * Time.deltaTime), transform.position.z);
        }
    }
}
