using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;

public class KidAttack : MonoBehaviour {

	int damageAtt1;
	public GameObject damageZone;

	void Update(){
		if(Input.GetButtonDown ("Fire1")){
			Attack ();
		}
	}

	public void Attack(){

	}
}
