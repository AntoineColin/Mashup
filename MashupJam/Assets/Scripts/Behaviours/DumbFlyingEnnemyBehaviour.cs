using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbFlyingEnnemyBehaviour : EnnemyBehaviour {

	void Start(){
		SetState (Fly);
	}
}
