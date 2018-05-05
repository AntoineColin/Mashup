using UnityEngine;

public class KidBehaviour : LivingBehaviour
{
	[SerializeField] float interactReach = 0.5f;
	[SerializeField] private LayerMask activableLayer;

	protected override void Starting ()	{
		activableLayer = LayerMask.GetMask("Interactable"); // String - slow, error prone.
		ennemyTag = "Ennemy";
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
				Shoot (ammo, new Vector2(facing * 4, 0));
			}
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
}
