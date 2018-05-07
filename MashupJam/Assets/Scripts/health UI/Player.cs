using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public delegate void HealthBarDelegate(int health);
    public static event HealthBarDelegate OnHealthChanged;


    int health = 0;

        private void Start()
    {
        health = Rules.MAX_PLAYER_HEALTH;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        ClampHealth();
    }

    public void Heal(int heal)
    {
        health += heal;
        ClampHealth();

    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, Rules.MAX_PLAYER_HEALTH);
        OnHealthChanged(health);
    }


}
