using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.CompilerServices;
using UnityEngine.Networking.Types;
using System;

public class KidBehaviour : LivingBehaviour {

	[SerializeField] float interactReach =0.5f;
	LayerMask activableLayer;

	void Start () {
		activableLayer = LayerMask.GetMask ("Interactable");
		ground = ground + LayerMask.GetMask ("Ennemy");
	}
		
	void LateUpdate () {
		if (health.Conscious) {
			Walk (Input.GetAxisRaw ("Horizontal") * speed);
			if (Input.GetButton ("Jump")) {
				if (isGrounded ()) {
					Jump (jumpForce);
				}else{
					JumpLonger (jumpStayForce);
				}
			}
		}
	}

	void Interact(){
		Collider2D[] results = Physics2D.OverlapCircleAll (transform.position, interactReach, activableLayer);
		foreach(Collider2D res in results){
			Debug.Log (res);
			res.GetComponent <Interactable>().Interact ();
		}
	}
}
	