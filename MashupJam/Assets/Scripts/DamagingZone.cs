using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingZone : MonoBehaviour
{
	[SerializeField] int damage = 15;

	void OnCollisionEnter2D(Collision2D coll)
	{
		BreakableHealth breakableHealth = coll.gameObject.GetComponent<BreakableHealth>();

		if (breakableHealth != null)
		{
			breakableHealth.Hurt(damage);
		}
	}

	void OnTriggerEnter2D(Collider2D coll){

		BreakableHealth breakableHealth = coll.gameObject.GetComponent<BreakableHealth>();

		if (breakableHealth != null)
		{
			breakableHealth.Hurt(damage);
		}
	}
}
