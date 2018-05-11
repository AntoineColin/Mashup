using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyHealth : LivingHealth {

	public static event OnDeath OnEnnemyDeath;

	protected override void Die ()
	{
		OnEnnemyDeath ();
		base.Die ();
	}
}
