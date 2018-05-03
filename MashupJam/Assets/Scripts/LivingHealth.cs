using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingHealth : BreakableHealth {

	[SerializeField]protected int maxlife;
	[SerializeField]protected int shield;

	protected bool conscious = true;
	[SerializeField]protected float timeToConscious;

	public bool Conscious {
		get {
			return conscious;
		}
	}


	void Start(){
		life = maxlife;

	}

	public override void Hurt(int damage){
		//Debug.Log ("Hurt a Living for " + damage + " damages");
		if(beatable){
			base.Hurt (damageShield (damage));
		}
	}

	public virtual int damageShield(int damage){
		int diff = damage - shield;
		if(diff > 0){	//if damage are superior of the shield
			breakShield ();
			return diff;
		}else{			//if damage are inferior of the shield
			shield = -diff;
			return 0;
		}
	}

	public virtual void breakShield (){
		shield = 0;
	}

	public virtual void Heal(int care){
		life += care;
		if (life > maxlife)
			life = maxlife;
	}

	protected IEnumerator Unconscious(float time){
		conscious = false;
		yield return new WaitForSeconds (time);
		conscious = true;
	}
}
