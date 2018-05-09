using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour{

	void OnEnable(){
		KidHealth.OnKidDeath += Lose;
	}

	void OnDisable(){
		KidHealth.OnKidDeath -= Lose;
	}

    public void LoadLevel(string CutScene)
    {
        SceneManager.LoadScene(CutScene, LoadSceneMode.Single);
    }

    public void QuitRequest(string name)
    {
        Application.Quit();
    }

    public void ReturnToStart(string Start)
    {
        SceneManager.LoadScene(Start, LoadSceneMode.Single);
    }

    public void Win(string Win)
    {
        SceneManager.LoadScene(Win, LoadSceneMode.Single);
    }

   
    public void TestScene(string TestScene)
    {
        SceneManager.LoadScene(TestScene, LoadSceneMode.Single);
    }

	public void Lose(){
		Debug.Log ("Lose");
		ReturnToStart ("Start");
	}
}
