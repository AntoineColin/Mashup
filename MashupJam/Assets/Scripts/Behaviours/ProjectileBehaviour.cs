using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : LivingBehaviour {

	void OnCollisionEnter2D(Collision2D coll){
		Injure (coll.gameObject);
		Destroy (gameObject);
	}
}
