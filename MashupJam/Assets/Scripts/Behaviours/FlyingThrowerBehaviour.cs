using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingThrowerBehaviour : EnnemyBehaviour {


	// Use this for initialization
	void Start () {
		StatePush (FlyProwl);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(IsAlignVertical (target.transform.position, 0.1f))
			Shoot (ammo, Vector2.zero, damage);
	}
}
