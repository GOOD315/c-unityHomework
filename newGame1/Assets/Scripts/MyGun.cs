using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGun : MonoBehaviour
{
    // RAYCAST/DISTANCE
    [SerializeField] private GameObject hitParticle;
    private RaycastHit hit;
    private Ray ray;
    private float shootingDistance;

    // AMMO
    [SerializeField] public int maxAmmo = 30;
    public int currentAmmo;
    private bool outOfAmmo;
    private bool isReloading;

    // BULLET
    [SerializeField] private float bulletForce;
    [SerializeField] private Transform bulletPrefab;
    public Transform bulletSpawnPoint;

    // FIRE
    private bool lastFired;
    private float lastFired1;
    public float fireRate;
    private Transform mCam;

    // MUZZLE FLASH

    // INTERFACE
    [Header("UI Components")]
    public Text currentWeaponText;
    public Text currentAmmoText;
    public Text totalAmmoText;
    public string weaponName = "АК-74";


    void Awake()
    {
        currentAmmo = maxAmmo;
        shootingDistance = 1000f;
        mCam = Camera.main.transform;
        isReloading = false;
    }


    void Start()
    {
        //Get weapon name from string to text
        // currentWeaponText.text = weaponName;
        //Set total ammo text from total ammo int
        // totalAmmoText.text = maxAmmo.ToString();
    }

    void Update()
    {
        //Set current ammo text from ammo int
        // currentAmmoText.text = currentAmmo.ToString();

        if (currentAmmo <= 0)
        {
            outOfAmmo = true;
        }

        // automatic fire
        if (Input.GetMouseButton(0) && !outOfAmmo && !isReloading)
        {
            //Shoot automatic
            if (Time.time - lastFired1 > 1 / fireRate)
            {

                lastFired1 = Time.time;

                lastFired = true;
                currentAmmo -= 1;

                // AUDIO
                // MUZZLE FLASH

                var bullet = (Transform)Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletForce;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }

    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(3);
        isReloading = false;
        currentAmmo = maxAmmo;
        outOfAmmo = false;
    }
}
