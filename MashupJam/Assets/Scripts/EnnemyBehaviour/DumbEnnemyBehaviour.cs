using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnnemyBehaviour : EnnemyBehaviour {

	[SerializeField] float chaseSpeed = 2;
	
	void LateUpdate(){
		WalkDumbChase (target, chaseSpeed);
	}
}
