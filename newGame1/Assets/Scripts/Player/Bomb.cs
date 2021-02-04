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
    [SerializeField] private int damage;
    private bool isExploding;


    // Start is called before the first frame update
    void Start()
    {
        bombTimer = 2f;
        radius = 15f;
        lift = 10f;
        power = 50f;

        isExploding = false;
    }

    void FixedUpdate()
    {
        bombTimer -= Time.deltaTime;
        if (bombTimer <= 0 && !isExploding)
        {
            isExploding = true;
            Vector3 explPos = transform.position;
            Collider[] colls = Physics.OverlapSphere(explPos, radius);
            foreach (Collider col in colls)
            {
                if (col.GetComponent<Rigidbody>())
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(power, explPos, radius, lift, ForceMode.Impulse);
                }
                if (col.GetComponent<Enemy>())
                {
                    var enemyScr = col.GetComponent<Enemy>();
                    enemyScr.Damage(damage);
                }
            }
            GameObject explode = transform.GetChild(0).gameObject;
            explode.SetActive(true);
            Destroy(gameObject, 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
