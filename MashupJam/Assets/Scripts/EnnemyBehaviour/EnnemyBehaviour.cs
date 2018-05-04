using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Security;
using UnityEngine.Analytics;

public abstract class EnnemyBehaviour : MonoBehaviour {

	protected Rigidbody2D rb2d;
	protected Collider2D coll2d;
	protected LivingHealth Health;

	protected LayerMask opponents, ground;
	[SerializeField] string layerMaskOpponent = "Player";
	protected GameObject target;
	float distToBottom, distToRight, distToLeft;
	float wanderSide = 1;


	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		coll2d = GetComponent<Collider2D> ();

		distToBottom = coll2d.bounds.extents.y;
		distToRight = coll2d.bounds.extents.x;
		distToLeft = -distToRight;
		opponents = LayerMask.GetMask (layerMaskOpponent);
		ground = LayerMask.GetMask ("Decor", "Default");
		target = GameObject.Find ("Kid");
	}
		



	/*
	 * STATES
	 */
	#region states

	#endregion



	#region tests
	/*
	 * TESTS
	 */
	public bool isGrounded(){
		return ((bool)Physics2D.Raycast (transform.position + new Vector3 (distToRight,0,0), Vector2.down, distToBottom + 0.1f, ground) || 
			(bool)Physics2D.Raycast (transform.position + new Vector3 (distToLeft,0,0), Vector2.down, distToBottom + 0.1f, ground) || 
			(bool)Physics2D.Raycast (transform.position, Vector2.down, distToBottom + 0.1f, ground));
	}

	public bool isNear(GameObject target, float nearLimit){
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
		if(isGrounded ())
			rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpHeight);
	}

	public void FlyDumbChase(GameObject target, float speedChase){
		Vector2 direction = (target.transform.position - transform.position).normalized;
		rb2d.velocity = direction * speedChase;
	}

	public void WalkDumbChase(GameObject target, float speedChase){
		Walk (whichHorizontalSide (target.transform.position) * speedChase);
		if (whichVerticalSide (target.transform.position) > 0 ) {
			Jump (3);
		}
	}

	public void Wander(float walkSpeed){
		if(isOnBorder ())
			wanderSide = -wanderSide;
		
		Walk (walkSpeed*wanderSide);
	}


	#endregion
}
