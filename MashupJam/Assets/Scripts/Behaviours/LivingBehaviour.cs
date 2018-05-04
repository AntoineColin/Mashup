using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingBehaviour : MonoBehaviour {

	/*
	 * COMPONENTS
	 */
	protected Rigidbody2D rb2d;
	protected Collider2D coll2d;
	protected LivingHealth health;

	/*
	 * RELATIVES
	 */
	protected LayerMask opponents, ground;
	[SerializeField] string layerMaskOpponent = "Player";
	protected GameObject target;

	/*
	 * CONDITION
	 */
	float distToBottom, distToRight, distToLeft;
	protected float wanderSide = 1;
	protected Vector2 direction;

	/*
	 * PROPERTIES
	 */
	[SerializeField] protected float speed;
	[SerializeField] protected float jumpForce;
	[SerializeField] protected float jumpStayForce;


	protected Action state;

	void Awake(){
		rb2d = GetComponent<Rigidbody2D> ();
		coll2d = GetComponent<Collider2D> ();
		health = GetComponent<LivingHealth> ();

		distToBottom = coll2d.bounds.extents.y;
		distToRight = coll2d.bounds.extents.x;
		distToLeft = -distToRight;
		opponents = LayerMask.GetMask (layerMaskOpponent);
		ground = LayerMask.GetMask ("Decor", "Default");
		target = GameObject.Find ("Kid");
		InvokeRepeating ("RandomDirection", 0.2f , 0.4f);
	}



	#region states
	public void SetState(Action s){
		state = s;
	}
	#endregion



	#region tests
	/*
	 * TESTS
	 */
	public bool isGrounded(){
		return ((bool)Physics2D.Raycast (transform.position + new Vector3 (distToRight - 0.1f,0,0), Vector2.down, distToBottom + 0.1f, ground) || 
			(bool)Physics2D.Raycast (transform.position + new Vector3 (distToLeft + 0.1f,0,0), Vector2.down, distToBottom + 0.1f, ground) || 
			(bool)Physics2D.Raycast (transform.position, Vector2.down, distToBottom + 0.1f, ground));
	}

	public bool isNear(GameObject target, float nearLimit){
		if (nearLimit < 0)
			return true;
		float distance = (target.transform.position - transform.position).magnitude;
		return (distance <= nearLimit);
	}

	public bool isFar(GameObject target, float farLimit){
		return !isNear (target, farLimit);
	}

	public bool isInSight(GameObject target, float farLimit){
		return true;
	}

	public bool isAlignVertical(Vector2 targetPos, float tolerance){
		return !((targetPos.x > transform.position.x + tolerance) || (targetPos.x < transform.position.x - tolerance));
	}

	public bool isAlignHorizontal(Vector2 targetPos, float tolerance){
		return !((targetPos.x > transform.position.y + tolerance) || (targetPos.x < transform.position.y - tolerance));;
	}

	public bool isOnBorder(){
		bool hole = !((bool)Physics2D.Raycast (transform.position + new Vector3(distToRight + 0.1f, 0),Vector2.down, distToBottom + 0.2f, ground) && 
			(bool)Physics2D.Raycast (transform.position + new Vector3(distToLeft - 0.1f, 0),Vector2.down, distToBottom + 0.2f, ground));
		bool wall = (bool)Physics2D.Raycast (transform.position, Vector2.right, distToRight + 0.1f, ground) ||
			(bool)Physics2D.Raycast (transform.position, Vector2.left, -distToLeft + 0.1f, ground);
		return hole || wall;
	}

	public int whichHorizontalSide(Vector2 targetPos){
		if ((targetPos - (Vector2)transform.position).x > 0)
			return 1;
		else
			return -1;
	}

	public int whichVerticalSide(Vector2 targetPos){
		if ((targetPos - (Vector2)transform.position).y > 0)
			return 1;
		else
			return -1;
	}

	public void GetEnnemy(string ennemyTag){
		target = GameObject.FindGameObjectWithTag (ennemyTag);
	}

	public void GetEnnemy(string ennemyTag, float checkedRange){
		target = Physics2D.OverlapCircle (transform.position, checkedRange).gameObject;
		Debug.Log ("found target : " + target);
	}
	#endregion


	#region capacities
	/*
	 * CAPACITIES
	 */
	public void SayHi(){
		Debug.Log ("Hi");
	}

	public void Walk(float speed){
		rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
	}

	public void Jump(float jumpHeight){
		Debug.Log ("JUMP");
		if(isGrounded ())
			rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpHeight);
	}

	public void JumpLonger(float jumpStayForce){
		if(rb2d.velocity.y > 0){
			rb2d.velocity += new Vector2(0, jumpStayForce / 50);
		}
	}

	public void FlyDumbChase(GameObject target, float speedChase){
		Vector2 direction = (target.transform.position - transform.position).normalized;
		rb2d.velocity = direction * speedChase;
	}

	public void WalkDumbChase(GameObject target, float speedChase){
		Walk (whichHorizontalSide (target.transform.position) * speedChase);
		if (whichVerticalSide (target.transform.position) > 0 ) {
			Jump (jumpForce);
		}
	}

	public void Wander(float walkSpeed){
		if(isOnBorder ())
			wanderSide = -wanderSide;

		Walk (walkSpeed*wanderSide);
	}

	public void FlyRandom(float speed){
		
		rb2d.velocity = direction * speed;
	}

	public void RandomDirection(){
		Debug.Log ("Random direction");
		direction = UnityEngine.Random.insideUnitCircle;
	}

	#endregion
}
