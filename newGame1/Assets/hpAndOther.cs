using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpAndOther : MonoBehaviour
{

    private int _health;
    private int _maxHealth;
    public int Health { get => _health; set => _health = value; }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    // INCOME DAMAGE



    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        MaxHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int dmg)
    {
        _health -= dmg;
    }

    public void Heal(int heal)
    {
        _health += heal;
        if (_health > _maxHealth) _health = 100;
    }

}
