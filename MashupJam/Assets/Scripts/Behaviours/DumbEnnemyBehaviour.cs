using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnnemyBehaviour : EnnemyBehaviour {
	
	void Start(){
		chaseDistance = -1;
		StatePush (WalkKamikaze);
	}
}
