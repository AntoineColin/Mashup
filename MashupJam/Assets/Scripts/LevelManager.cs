using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour{

	private int ennemyNb;

	void OnEnable(){
		KidHealth.OnKidDeath += Lose;
		EnnemyHealth.OnEnnemyDeath += Win;
	}

	void OnDisable(){
		KidHealth.OnKidDeath -= Lose;
		EnnemyHealth.OnEnnemyDeath -= Win;
	}

    public void LoadLevel(string Introduction)
    {
        SceneManager.LoadScene(Introduction, LoadSceneMode.Single);
    }

    public void QuitRequest(string name)
    {
        Application.Quit();
    }

	public int GenerateLevel(int difficulty){
		int EnnemyNumber = 0;
		return EnnemyNumber;
	}

	public void Lose(){
		Debug.Log ("Lose");
		GameObject.Destroy (GameObject.Find ("Player"));
		LoadLevel ("Start");
	}

	public void Win(){
		ennemyNb--;
		Debug.Log ("ennemies remaining : " + ennemyNb);
		if (ennemyNb <= 0) {
			Debug.Log ("Win");
			LoadLevel ("Map");
		}
	}
}
