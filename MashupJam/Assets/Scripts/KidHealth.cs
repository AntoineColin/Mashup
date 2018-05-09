using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class KidHealth : LivingHealth {

	public delegate void OnValueChanged(int value);
	public static event OnValueChanged OnHealthChanged;
	public static event OnDeath OnKidDeath;

	public override void Hurt (int damage)
	{
		base.Hurt (damage);
		OnHealthChanged (life);
	}

	public override void Heal (int care)
	{
		base.Heal (care);
		OnHealthChanged (life);
	}

	protected override IEnumerator Disappear(float timeToDie){
		StartCoroutine (Unconscious (timeToDie));
		yield return new WaitForSeconds (timeToDie);
		Debug.Log ("Wouaw");
		OnKidDeath ();
	}

	void ClampHealth()
	{
		life = Mathf.Clamp(life, 0, Rules.MAX_PLAYER_HEALTH);
		OnHealthChanged(life);
	}
}
