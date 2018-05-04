using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingBehaviour : MonoBehaviour
{
	/*
	 * COMPONENTS
	 */
	protected Rigidbody2D rb2d;
	protected Collider2D coll2d;
	protected LivingHealth health;

	/*
	 * RELATIVES
	 */
	[SerializeField] protected LayerMask opponents, ground;
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

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		coll2d = GetComponent<Collider2D>();
		health = GetComponent<LivingHealth>();

		distToBottom = coll2d.bounds.extents.y;
		distToRight = coll2d.bounds.extents.x;
		distToLeft = -distToRight;
		target = GameObject.Find("Player");
		InvokeRepeating("RandomDirection", 0.2f, 0.4f);
	}



	#region states
	public void SetState(Action s)
	{
		state = s;
	}
	#endregion



	#region tests
	/*
	 * TESTS
	 */
	public bool IsGrounded()
	{
		return Physics2D.Raycast(transform.position + new Vector3(distToRight - 0.1f, 0, 0), Vector2.down, distToBottom + 0.1f, ground) ||
			Physics2D.Raycast(transform.position + new Vector3(distToLeft + 0.1f, 0, 0), Vector2.down, distToBottom + 0.1f, ground) ||
			Physics2D.Raycast(transform.position, Vector2.down, distToBottom + 0.1f, ground);
	}

	public bool IsNear(GameObject target, float nearLimit)
	{
		if (nearLimit < 0)
			return true;
		float distance = (target.transform.position - transform.position).magnitude;
		return (distance <= nearLimit);
	}

	public bool IsFar(GameObject target, float farLimit)
	{
		return !IsNear(target, farLimit);
	}

	public bool IsInSight(GameObject target, float farLimit)
	{
		return true;
	}

	public bool IsAlignVertical(Vector2 targetPos, float tolerance)
	{
		return !((targetPos.x > transform.position.x + tolerance) || (targetPos.x < transform.position.x - tolerance));
	}

	public bool IsAlignHorizontal(Vector2 targetPos, float tolerance)
	{
		return !((targetPos.x > transform.position.y + tolerance) || (targetPos.x < transform.position.y - tolerance));
		;
	}

	public bool IsOnBorder()
	{
		bool hole = !(Physics2D.Raycast(transform.position + new Vector3(distToRight + 0.1f, 0), Vector2.down, distToBottom + 0.2f, ground) &&
			Physics2D.Raycast(transform.position + new Vector3(distToLeft - 0.1f, 0), Vector2.down, distToBottom + 0.2f, ground));
		bool wall = Physics2D.Raycast(transform.position, Vector2.right, distToRight + 0.1f, ground) ||
			Physics2D.Raycast(transform.position, Vector2.left, -distToLeft + 0.1f, ground);
		return hole || wall;
	}

	public int WhichHorizontalSide(Vector2 targetPos)
	{
		if ((targetPos - (Vector2)transform.position).x > 0)
			return 1;
		else
			return -1;
	}

	public int WhichVerticalSide(Vector2 targetPos)
	{
		if ((targetPos - (Vector2)transform.position).y > 0)
			return 1;
		else
			return -1;
	}

	public void GetEnnemy(string ennemyTag)
	{
		target = GameObject.FindGameObjectWithTag(ennemyTag);
	}

	public void GetEnnemy(string ennemyTag, float checkedRange)
	{
		target = Physics2D.OverlapCircle(transform.position, checkedRange).gameObject;
		Debug.Log("found target : " + target);
	}
	#endregion


	#region capacities
	/*
	 * CAPACITIES
	 */
	public void SayHi()
	{
		Debug.Log("Hi");
	}

	public void Walk(float speed)
	{
		rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
	}

	public void Jump(float jumpHeight)
	{
		Debug.Log("JUMP");
		if (IsGrounded())
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
	}

	public void JumpLonger(float jumpStayForce)
	{
		if (rb2d.velocity.y > 0)
		{
			rb2d.velocity += new Vector2(0, jumpStayForce / 50);
		}
	}

	public void FlyDumbChase(GameObject target, float speedChase)
	{
		Vector2 direction = (target.transform.position - transform.position).normalized;
		rb2d.velocity = direction * speedChase;
	}

	public void WalkDumbChase(GameObject target, float speedChase)
	{
		Walk(WhichHorizontalSide(target.transform.position) * speedChase);
		if (WhichVerticalSide(target.transform.position) > 0)
		{
			Jump(jumpForce);
		}
	}

	public void Wander(float walkSpeed)
	{
		if (IsOnBorder())
			wanderSide = -wanderSide;

		Walk(walkSpeed * wanderSide);
	}

	public void FlyRandom(float speed)
	{

		rb2d.velocity = direction * speed;
	}

	public void RandomDirection()
	{
		Debug.Log("Random direction");
		direction = UnityEngine.Random.insideUnitCircle;
	}

	#endregion
}
