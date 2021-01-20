using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyFpsController;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] Spawner enemySpawnScr;
    [SerializeField] MyGun myGunScr;
    MyFpsController2 myFpsContrScr;
    abilities abilitiesScr;
    hpAndOther hpAndOtherScr;


    [Header("UI ELEMENTS: Gun")]
    public Text currentWeaponText;
    public Text currentAmmoText;
    public Text totalAmmoText;
    public Text ammoDivider;
    public Image cross;
    public Image gunIcon;
    [Header("UI ELEMENTS: Abilities")]
    public Text buttonText;
    [Header("UI ELEMENTS: Enemies")]
    public Text currentEnemyCntText;
    [Header("HEALTH")]
    public Image hpBar;
    public Image hpSlider;


    // STATES
    private bool isPlaying;

    private void Awake()
    {
        abilitiesScr = gameObject.GetComponent<abilities>();
        myFpsContrScr = gameObject.GetComponent<MyFpsController2>();
        hpAndOtherScr = gameObject.GetComponent<hpAndOther>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        // UI COMPONENTS
        currentWeaponText.text = myGunScr.weaponName;
        totalAmmoText.text = myGunScr.maxAmmo.ToString();
        currentAmmoText.text = myGunScr.currentAmmo.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        // GATE BUTTON
        if (abilitiesScr.buttonReady) { buttonText.enabled = true; }
        else { buttonText.enabled = false; }

        if (isPlaying)
        {
            currentAmmoText.text = myGunScr.currentAmmo.ToString();

        }

        currentEnemyCntText.text = enemySpawnScr.enemyCounter.ToString();

        checkHealth();
    }

    void checkHealth()
    {
        float health = (float)hpAndOtherScr.Health;
        float maxHealth = (float)hpAndOtherScr.MaxHealth;

        hpSlider.fillAmount = (1 - health * 100 / maxHealth / 100);
    }
}
