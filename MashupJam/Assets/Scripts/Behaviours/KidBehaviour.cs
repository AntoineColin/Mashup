using UnityEngine;

public class KidBehaviour : LivingBehaviour
{
	[SerializeField] float interactReach = 0.5f;
	[SerializeField] private LayerMask activableLayer;

	void Start()
	{
		activableLayer = LayerMask.GetMask("Interactable"); // String - slow, error prone.
		ground = ground + LayerMask.GetMask("Ennemy"); // String - slow, error prone.
	}

	void LateUpdate()
	{
		if (health.Conscious)
		{
			Walk(Input.GetAxisRaw("Horizontal") * speed);
			if (Input.GetButton("Jump"))
			{
				if (IsGrounded())
				{
					Jump(jumpForce);
				}
				else
				{
					JumpLonger(jumpStayForce);
				}
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
