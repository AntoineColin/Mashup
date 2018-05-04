using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;

public class KidAttack : MonoBehaviour {

	[SerializeField] int damageAtt1;
	public Collider2D damageZone;
	LayerMask layerMask;

	[SerializeField] float interactReach =0.5f;

	void Start(){
		layerMask = LayerMask.GetMask ("Interactable");
	}

	void Update(){
		if(Input.GetButtonDown ("Fire1")){
			Attack ();
		}
		if(Input.GetButtonDown ("Interact")){
			Interact ();
		}
	}

	public void Attack(){

	}

	void Interact(){
		Collider2D[] results = Physics2D.OverlapCircleAll (transform.position, interactReach, layerMask);
		foreach(Collider2D res in results){
			Debug.Log (res);
			res.GetComponent <Interactable>().Interact ();
		}
	}
}
