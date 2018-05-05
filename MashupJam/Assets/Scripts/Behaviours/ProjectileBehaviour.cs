using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : LivingBehaviour {

	protected override void Starting (){
		
	}

	void OnCollisionEnter2D(Collision2D coll){
		Injure (coll.gameObject);
		Destroy (gameObject);
	}
}
