using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.CompilerServices;
using UnityEngine.Networking.Types;
using System;

public class KidMovement : MonoBehaviour {

	//OPT : bool canDoubleJump = true;

	public float speed = 5;		//horizontal movement speed
	public float jumpforce = 5;	//vertical jump force
	float distToBottom;			//Distance where to check the ground from the center of collider
	float inertia;

	Rigidbody2D rgbd;	//own
	Collider2D col2d;	//own
	KidState state;		//own


	void Start () {

		/*
		 * Instanciation
		 */
		try{
			rgbd = GetComponent<Rigidbody2D> ();
			col2d = GetComponent<Collider2D> ();
			state = GetComponent<KidState> ();
			distToBottom = col2d.bounds.extents.y;
		}catch(MissingComponentException e){
			Debug.LogError (e);
		}
	}


	void FixedUpdate () {
		if (state.Conscious) {
			if (isGrounded ()) {
				Move (Input.GetAxisRaw ("Horizontal"));
				if (Input.GetButton ("Jump")) {
					Jump ();
				}
			} else {
				MoveInAir (Input.GetAxisRaw ("Horizontal"));
			}
		}
	}

	bool isGrounded(){
		return ((bool)Physics2D.Raycast (transform.position + new Vector3 (0.4f,0,0), Vector2.down, distToBottom + 0.1f, (15<<9) + 1) || 
			(bool)Physics2D.Raycast (transform.position + new Vector3 (-0.4f,0,0), Vector2.down, distToBottom + 0.1f, (15<<9) + 1));
	}

	void Move(float force){
		rgbd.velocity = new Vector2 (force * speed, rgbd.velocity.y);
	}

	void MoveInAir(float force){
		rgbd.AddForce (new Vector2(force * 10, 0));

		//compensation of the forward acceleration in air
		if(rgbd.velocity.x > speed){
			rgbd.velocity = new Vector2 (speed, rgbd.velocity.y);
		}
		if(rgbd.velocity.x < -speed){
			rgbd.velocity = new Vector2 (-speed, rgbd.velocity.y);
		}
	}

	void Jump(){
		rgbd.velocity = new Vector2 (rgbd.velocity.x, jumpforce);
		inertia = rgbd.velocity.x;
	}
}
