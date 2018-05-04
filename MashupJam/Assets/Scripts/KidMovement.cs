using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.CompilerServices;
using UnityEngine.Networking.Types;
using System;

public class KidMovement : MonoBehaviour {

	//OPT : bool canDoubleJump = true;

	[SerializeField] float speed = 5;		//horizontal movement speed
	[SerializeField] float jumpForce = 5;	//vertical jump force
	[SerializeField] float jumpStayForce = 5;	//vertical positive force to enhance the jump force on each frame
	float distToBottom;			//Distance where to check the ground from the center of collider
	int layerMask;				//layer mask of things you can jump from it 

	Rigidbody2D rgbd;	//own
	Collider2D col2d;	//own
	KidHealth health;	//own


	void Start () {

		/*
		 * Instanciation
		 */
		rgbd = GetComponent<Rigidbody2D> ();
		col2d = GetComponent<Collider2D> ();
		health = GetComponent<KidHealth> ();
		distToBottom = col2d.bounds.extents.y;
		layerMask = LayerMask.GetMask ("Decor", "Default", "Ennemy");
	}


	void FixedUpdate () {
		if (health.Conscious) {
			Move (Input.GetAxisRaw ("Horizontal"));
			if (Input.GetButton ("Jump")) {
				
				if (isGrounded ()) {
					Jump ();
				}else{
					JumpLonger ();
				}
			}
		}
	}

	/*
	 * The character is grounded checked on his right and left side
	 */
	public bool isGrounded(){
		return ((bool)Physics2D.Raycast (transform.position + new Vector3 (0.4f,0,0), Vector2.down, distToBottom + 0.1f, layerMask) || 
			(bool)Physics2D.Raycast (transform.position + new Vector3 (-0.4f,0,0), Vector2.down, distToBottom + 0.1f, layerMask) || 
			(bool)Physics2D.Raycast (transform.position, Vector2.down, distToBottom + 0.1f, layerMask));
	}

	void Move(float force){
		rgbd.velocity = new Vector2 (force * speed, rgbd.velocity.y);
	}

	void Jump(){
		rgbd.velocity = new Vector2 (rgbd.velocity.x, jumpForce);
	}

	void JumpLonger(){
		if(rgbd.velocity.y > 0){
			rgbd.velocity += new Vector2(0, jumpStayForce / 50);
		}
	}


	//UNUSED
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
}
	