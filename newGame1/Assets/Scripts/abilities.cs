using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilities : MonoBehaviour
{
    // BOMB
    [SerializeField] private GameObject bomb;
    private Transform mCam;
    private int bombForce;

    // CHARGE
    private float chargeSpeed;

    // BUTTONS
    private bool buttonReady;
    private float buttonDistance;
    private RaycastHit btnHit;
    private Ray btnRay;

    // INTERFACE
    [Header("Buttons")]
    public Text buttonText;

    // GATE
    [SerializeField] private GameObject gate;
    GateScript gateScript;


    // Start is called before the first frame update
    void Start()
    {
        bombForce = 30;
        mCam = Camera.main.transform;
        buttonText.enabled = false;
        this.buttonDistance = 4f;
    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckButtonRange();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 pos = new Vector3(mCam.position.x, mCam.position.y + 0.5f, mCam.position.z);
            GameObject temp = Instantiate(bomb, pos + mCam.forward, Quaternion.identity);
            Rigidbody rb = temp.GetComponent<Rigidbody>();
            rb.AddForce(mCam.forward * bombForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.E) && buttonReady)
        {
            if (btnHit.transform.name == "GateButton")
            {
                gateScript = gate.GetComponent<GateScript>();
                gateScript.gateOpen = !gateScript.gateOpen;
            }
        }

    }

    void CheckButtonRange()
    {
        btnRay = new Ray(mCam.position, mCam.transform.forward);
        if (Physics.Raycast(btnRay, out btnHit, buttonDistance))
        {
            if (btnHit.transform.name == "GateButton")
            {
                buttonReady = true;
                buttonText.enabled = true;
            }
            else
            {
                buttonReady = false;
                buttonText.enabled = false;
            }
        }
        else { buttonText.enabled = false; buttonReady = false; }
    }

}
