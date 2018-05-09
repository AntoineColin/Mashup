using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image[] heartsImage;
    public int healthPerHeart = 4;

    void OnEnable()
    {
        Rules.MAX_PLAYER_HEALTH = heartsImage.Length * healthPerHeart;
		KidHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
		KidHealth.OnHealthChanged -= OnHealthChanged;
    }

    void OnHealthChanged(int health)
    {
		Debug.Log ("adapted UI");
        int heart = health / healthPerHeart; // deffault to lower bound 19/4 4 R 3
        int heartFill = health % healthPerHeart; // return the remainder of the division

        if (health % healthPerHeart == 0)
        {
            if (heart == heartsImage.Length) // indicates full health
            {
                heartsImage[heart - 1].fillAmount = 1;
                return;
            }

            if (heart > 0) // indicates anything but zero health where there are only whole hearts or empty hearts
            {
                heartsImage[heart].fillAmount = 0;
                heartsImage[heart - 1].fillAmount = 1;
            }

            else // indicates zero health
            {
                heartsImage[heart].fillAmount = 0;
            }
            return;

        }

        heartsImage[heart].fillAmount = heartFill / (float)healthPerHeart;
    }
}
