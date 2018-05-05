using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbFlyingEnnemyBehaviour : EnnemyBehaviour {

	void Start(){
		StatePush (FlyRandom);
	}

	void LateUpdate(){
		if (IsNear (target, chaseDistance)){
			StatePush (FlyKamikaze);
		}
	}
}
