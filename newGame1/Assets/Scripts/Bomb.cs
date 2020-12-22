using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class Bomb : MonoBehaviour
{
    private float bombTimer;
    private float radius;
    private float power;
    private float lift;

    // Start is called before the first frame update
    void Start()
    {
        bombTimer = 2f;
        radius = 15f;
        lift = 2f;
        power = 50f;
    }

    void FixedUpdate()
    {
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0)
        {
            print("VZRRIV AAA");
            Vector3 explPos = transform.position;
            Collider[] colls = Physics.OverlapSphere(explPos, radius);
            foreach (Collider col in colls)
            {
                if (col.GetComponent<Rigidbody>())
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(power, explPos, radius, lift, ForceMode.Impulse);
                }
            }
            GameObject explode = transform.GetChild(0).gameObject;
            explode.SetActive(true);
            // explode.transform.parent=
            Destroy(gameObject, 1f);
            bombTimer = 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
