using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbFlyingEnnemyBehaviour : EnnemyBehaviour {

	[SerializeField] float chaseSpeed = 1.5f;

	void LateUpdate(){
		FlyDumbChase (target, chaseSpeed);
	}


}
