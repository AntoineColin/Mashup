using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : LivingBehaviour {

	[SerializeField] protected float secondToDie = 4;
	float instanciationTime;

	protected override void Starting (){
		instanciationTime = Time.timeSinceLevelLoad;
	}

	void LateUpdate(){
		if(secondToDie < Time.timeSinceLevelLoad -  instanciationTime){
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		Injure (coll.gameObject);
		Destroy (gameObject);
	}
}
