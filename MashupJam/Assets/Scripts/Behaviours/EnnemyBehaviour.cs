using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Security;
using UnityEngine.Analytics;
using System;
using System.Reflection;
using UnityEditorInternal;

public abstract class EnnemyBehaviour : LivingBehaviour
{
	[SerializeField] protected float chaseDistance = 3;

	protected override void Starting (){
		ennemyTag = "Player";
	}

	#region states
	/*
	 * STATES
	 */
	public void WalkPatrol()
	{
		Wander(speed);
	}

	public void WalkKamikaze()
	{
		WalkDumbChase(target, speed);

		if (IsFar(target, chaseDistance))
			StatePop ();
	}

	public void FlyKamikaze()
	{
		FlyDumbChase(target, speed);
		if (IsFar(target, chaseDistance))
			StatePop ();
	}

	public void FlyRandom()
	{
		FlyRandom(speed);
	}

	public void WalkFlight(){
		Flight (target, speed);
		if (IsFar (target, chaseDistance))
			StatePop ();
	}
		

	#endregion

	void OnCollisionEnter2D(Collision2D coll){
		Injure (coll.gameObject);
	}
}
