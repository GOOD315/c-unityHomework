using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyFpsController;
using UnityEngine.SceneManagement;

public class abilities : MonoBehaviour
{
    // BOMB
    [SerializeField] private GameObject bomb;
    private Transform mCam;
    private int bombForce;

    // BUTTONS
    public bool buttonReady;
    private float buttonDistance;
    private RaycastHit btnHit;
    private Ray btnRay;

    // INTERFACE
    [Header("Buttons")]
    public Text buttonText;

    // GATE
    [SerializeField] private GameObject gate;
    GateScript gateScript;

    // MOVEMENT
    private float dashSpeed;
    Rigidbody _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        // BOMB
        bombForce = 30;
        mCam = Camera.main.transform;

        // BUTTONS
        buttonText.enabled = false;
        this.buttonDistance = 4f;

        // MOVEMENT
        _rigidbody = GetComponent<Rigidbody>();
        dashSpeed = 1000f;
    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckButtonRange();

        // BOMB
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 pos = new Vector3(mCam.position.x, mCam.position.y + 0.5f, mCam.position.z);
            GameObject temp = Instantiate(bomb, pos + mCam.forward, Quaternion.identity);
            Rigidbody rb = temp.GetComponent<Rigidbody>();
            rb.AddForce(mCam.forward * bombForce, ForceMode.Impulse);
        }

        // GATE
        if (Input.GetKeyDown(KeyCode.E) && buttonReady)
        {
            if (btnHit.transform.name == "GateButton")
            {
                gateScript = gate.GetComponent<GateScript>();
                gateScript.gateOpen = !gateScript.gateOpen;
            }
        }

        // DASH
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

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
                // buttonText.enabled = true;
            }
            else
            {
                buttonReady = false;
                // buttonText.enabled = false;
            }
        }
        else
        {
            // buttonText.enabled = false;
            buttonReady = false;
        }
    }

    void Dash()
    {
        var direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        var worldDirection = transform.TransformDirection(direction);
        var dashVelocity = worldDirection * dashSpeed;
        var rigidbodyVelocity = _rigidbody.velocity;

        var dashForce = new Vector3(dashVelocity.x + rigidbodyVelocity.x, 0f, dashVelocity.z + rigidbodyVelocity.z);
        _rigidbody.AddForce(dashForce, ForceMode.Impulse);
        print("DSAH");
    }

}
