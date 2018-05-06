using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class KidBehaviour : LivingBehaviour
{
	[SerializeField] float interactReach = 0.5f;
	[SerializeField] private LayerMask activableLayer;

	protected override void Starting ()	{
		activableLayer = LayerMask.GetMask("Interactable"); // String - slow, error prone.
		ennemyTag = "Ennemy";
		ammo.SetActive (false);
	}

	void LateUpdate()
	{
		if (health.Conscious)
		{
			var moveInput = Input.GetAxisRaw ("Horizontal");
			if (moveInput != 0)
				facing = moveInput;
			Walk(moveInput * speed);
			if (Input.GetButton("Jump"))
			{
				if (grounded)
				{
					Jump(jumpForce);
				}
				else
				{
					JumpLonger(jumpStayForce);
				}
			}
			if(Input.GetButton ("Fire1")){
				//Shoot (ammo, new Vector2(facing * 4, 0));
				StartCoroutine (Melee ());
			}
		}

	}

	public IEnumerator Melee(){
		if(currentCooldown <= 0){
			currentCooldown = cooldown;
			anim.SetBool ("facingLeft", facing == 1);
			anim.SetBool ("attacking", true);
			yield return new WaitForSeconds (0.1f);
			anim.SetBool ("attacking", false);
		}
	}

	void Interact()
	{
		Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, interactReach, activableLayer);
		foreach (Collider2D res in results)
		{
			Debug.Log(res);
			res.GetComponent<Interactable>().Interact();
		}
	}

	void OnTriggerStay2D(Collider2D coll){
		if(coll.gameObject.tag == ennemyTag){
			Injure (coll.gameObject);
		}
	}
		
}
