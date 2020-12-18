using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilities : MonoBehaviour
{
    // BOMB
    [SerializeField] private GameObject bomb;
    private Transform mCam;
    private int bombForce;

    // CHARGE
    private float chargeSpeed;


    // Start is called before the first frame update
    void Start()
    {
        bombForce = 30;
        mCam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 pos = new Vector3(mCam.position.x, mCam.position.y + 0.5f, mCam.position.z);
            GameObject temp = Instantiate(bomb, pos + mCam.forward, Quaternion.identity);
            Rigidbody rb = temp.GetComponent<Rigidbody>();
            rb.AddForce(mCam.forward * bombForce, ForceMode.Impulse);
        }
    }

}
