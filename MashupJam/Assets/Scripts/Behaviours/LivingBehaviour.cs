using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework;

public abstract class LivingBehaviour : MonoBehaviour
{
	/*
	 * COMPONENTS
	 */
	protected Rigidbody2D rb2d;
	protected Collider2D coll2d;
	protected LivingHealth health;
	protected Animator anim;

	/*
	 * RELATIVES
	 */
	[SerializeField] protected LayerMask opponents, ground;
	protected GameObject target;

	/*
	 * CONDITION
	 */
	float distToBottom, distToRight, distToLeft;
	protected float facing = 1;
	protected Vector2 direction, randDirection;
	protected bool grounded;
	protected int currentCooldown;

	/*
	 * PROPERTIES
	 */
	[SerializeField] protected float speed = 2;
	[SerializeField] protected float jumpForce = 2;
	[SerializeField] protected float jumpStayForce = 4;
	[SerializeField] protected int damage = 5;
	[SerializeField] protected string ennemyTag = "Player";
	[SerializeField] protected GameObject ammo;
	[SerializeField] protected int cooldown = 40;

	protected List<Action> state = new List<Action>();

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		coll2d = GetComponent<Collider2D>();
		health = GetComponent<LivingHealth>();
		anim = GetComponent<Animator> ();

		distToBottom = coll2d.bounds.extents.y;
		distToRight = coll2d.bounds.extents.x;
		distToLeft = -distToRight;
		target = GameObject.Find("Player");
		InvokeRepeating("RandomDirection", 0.2f, 0.4f);

		currentCooldown = cooldown;
		facing = 1;
		Starting ();
	}

	protected abstract void Starting ();

	void Update(){
		IsGrounded ();
		if (anim != null) {
			anim.SetBool ("inAir", !grounded);
			if (direction.x != 0)
				anim.SetBool ("moving", true);
			else
				anim.SetBool ("moving", false);
			anim.SetFloat ("direction", direction.x);
		}

		//Trigger the AI behaviour
		if (state.Count != 0) {
			state [state.Count-1] ();
		}

		currentCooldown--;
	}

	#region states
	public Action GetState(){
		if (state.Count == 0)
			return null;
		else
			return state [state.Count - 1];
	}

	public void StatePush(Action s)
	{
		if (s != GetState ())
			state.Add (s);
	}

	public void StatePop(){
		state.RemoveAt (state.Count-1);
	}
	#endregion


	#region tests
	/*
	 * TESTS
	 */
	public bool IsGrounded()
	{
		grounded = Physics2D.Raycast (transform.position + new Vector3 (distToRight - 0.1f, 0, 0), Vector2.down, distToBottom + 0.1f, ground) ||
				Physics2D.Raycast (transform.position + new Vector3 (distToLeft + 0.1f, 0, 0), Vector2.down, distToBottom + 0.1f, ground) ||
				Physics2D.Raycast (transform.position, Vector2.down, distToBottom + 0.1f, ground);
		return grounded;
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

	public void Waiting(){
		
	}

	public void Walk(float speed)
	{
		direction.x = speed;
		rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
	}

	public void Jump(float jumpHeight)
	{
		if (grounded)
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
		facing = WhichHorizontalSide (target.transform.position);
		Debug.Log (facing + " / " + speedChase);
		Walk(facing * speedChase);
		if (WhichVerticalSide(target.transform.position) > 0)
		{
			Jump(jumpForce);
		}
	}

	public void Wander(float walkSpeed)
	{
		if (IsOnBorder())
			facing = -facing;

		Walk(walkSpeed * facing);
	}

	public void Prowl(GameObject target, float limitChase){
		float upDown = 0;
		if (!IsAlignHorizontal (target.transform.position + new Vector3 (0, limitChase), 4))
			upDown = WhichVerticalSide (target.transform.position + new Vector3 (0, 4));
		if(!IsAlignVertical (target.transform.position, limitChase)){
			facing = WhichHorizontalSide (target.transform.position);
		}
		direction = new Vector2 (facing, 0) * (speed + randDirection.y);
		rb2d.velocity = direction;
	}

	public void FlyRandom(float speed)
	{
		direction = randDirection;
		rb2d.velocity = direction * speed;
	}

	public void RandomDirection()
	{
		randDirection = UnityEngine.Random.insideUnitCircle;
	}

	public void Flight(GameObject target, float speed){
		direction = transform.position - target.transform.position;
		if(direction.x > 0){
			facing = 1;
			Walk (speed);
		}else{
			facing = -1;
			Walk (-speed); 
		}
	}

	public void Injure(GameObject target){
		if (ennemyTag == target.tag) {
			target.GetComponent<BreakableHealth> ().Hurt (damage);
		}
	}

	public void Shoot(GameObject ammo, Vector2 startForce,int ammoDamage){
		if (currentCooldown <= 0) {
			currentCooldown = cooldown;
			GameObject shot = Instantiate (ammo, transform.position, Quaternion.identity);
			if (ennemyTag == "Player") {
			
				shot.layer = LayerMask.NameToLayer ("BadBullet");
			} else {
				shot.layer = LayerMask.NameToLayer ("GoodBullet");
			}
			LivingBehaviour living = shot.GetComponent<LivingBehaviour> ();
			living.ennemyTag = ennemyTag;
			living.damage = ammoDamage;
			shot.GetComponent<Rigidbody2D> ().AddForce (startForce * 2, ForceMode2D.Impulse);
		}
	}
		

	#endregion
}
