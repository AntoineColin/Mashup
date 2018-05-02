using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingZone : MonoBehaviour {

	[SerializeField] int damage = 15;

	void OnCollisionEnter2D(Collision2D coll){
		coll.gameObject.GetComponent<BreakableState> ().Hurt(damage);
	}
}
