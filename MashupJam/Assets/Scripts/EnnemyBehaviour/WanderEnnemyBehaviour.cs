using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderEnnemyBehaviour : EnnemyBehaviour {

	[SerializeField] float speed = 2;

	void LateUpdate(){
		
		Wander (speed);
	}
}
