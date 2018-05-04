using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Security;
using UnityEngine.Analytics;
using System;
using System.Reflection;

public abstract class EnnemyBehaviour : LivingBehaviour {

	[SerializeField]protected float chaseDistance = 3;
		
	void LateUpdate(){
		state ();
	}


	/*
	 * STATES
	 */
	#region states
	public void Patrol(){
		Wander (speed);
		if (isNear (target, chaseDistance))
			SetState (Kamikaze);
	}

	public void Kamikaze(){
		WalkDumbChase (target, speed);

		if (isFar (target, chaseDistance))
			SetState (Patrol);
	}

	public void FlyKamikaze(){
		FlyDumbChase (target, speed);
	}

	public void Fly(){
		FlyRandom (speed);
		if(isNear (target, chaseDistance)){
			SetState (FlyKamikaze);
		}
	}

	public void FlyPrudent(){
		FlyRandom (speed);
		if(isNear (target, chaseDistance)){
			SetState (FlyFlight);
		}
	}

	public void FlyFlight(){
		
	}

	#endregion

}
