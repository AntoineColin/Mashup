using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

public class WanderEnnemyBehaviour : EnnemyBehaviour {


	void Start(){
		SetState (Patrol);
	}
	
}
