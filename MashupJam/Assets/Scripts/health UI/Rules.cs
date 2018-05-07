using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour {

    static int maxPlayerHealth;
    public static int MAX_PLAYER_HEALTH
    {
        get { return maxPlayerHealth; }
        set
        {
            value = Mathf.Clamp(value, 0, 100);
            maxPlayerHealth = value;
        }
    }
}
