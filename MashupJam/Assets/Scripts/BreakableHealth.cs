﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System.Net;

public class BreakableHealth : MonoBehaviour {


	[SerializeField]protected int life;	//lifepoints
	[SerializeField]protected float timeUnbeats;
	[SerializeField]protected float timeToDie;
	protected bool beatable = true;


	public virtual void Hurt(){
		Hurt (1);
	}

	public virtual void Hurt(int damage){
		Debug.Log ("Hurt a breakable for " + damage);
		if(beatable){
			life -= damage;
			if(life<=0){
				Die ();
			}
			StartCoroutine(Unbeat(timeUnbeats));
		}
	}

	protected void Die(){
		Debug.Log ("die");
		StartCoroutine (Disappear(timeToDie));

	}

	protected IEnumerator Disappear(float timeToDie){
		yield return new WaitForSeconds (timeToDie);
		Debug.Log ("mort");
		Destroy (gameObject);
	}

	protected IEnumerator Unbeat(float time){
		beatable = false;
		yield return new WaitForSeconds (time);
		beatable = true;

	}
}