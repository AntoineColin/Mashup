using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour, Interactable {
	
	#region Interactable implementation
	public void Interact ()
	{
		Debug.Log ("message send");
	}
	#endregion

}
