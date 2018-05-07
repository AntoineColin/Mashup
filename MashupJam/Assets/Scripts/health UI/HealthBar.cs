using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image[] hearts;
    public int healthPerHeart = 4;

    void OnEnable()
    {
        Rules.MAX_PLAYER_HEALTH = hearts.Length * healthPerHeart;
        Player.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        Player.OnHealthChanged -= OnHealthChanged;
    }

    void OnHealthChanged(int health)
    {
        int heart = health / healthPerHeart; // deffault to lower bound 19/4 4 R 3
        int heartFill = health % healthPerHeart; // return the remainder of the division

        if (health % healthPerHeart == 0)
        {
            if (heart == hearts.Length) // indicates full health
            {
                hearts[heart - 1].fillAmount = 1;
                return;
            }

            if (heart > 0) // indicates anything but zero health where there are only whole hearts or empty hearts
            {
                hearts[heart].fillAmount = 0;
                hearts[heart - 1].fillAmount = 1;
            }

            else // indicates zero health
            {
                hearts[heart].fillAmount = 0;
            }
            return;

        }

        hearts[heart].fillAmount = heartFill / (float)healthPerHeart;
    }
}
