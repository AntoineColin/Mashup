using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour{

    public void LoadLevel(string CutScene)
    {
        Debug.Log("Load scene requested for:" + CutScene);
        SceneManager.LoadSceneAsync(CutScene, LoadSceneMode.Single);
    }

    public void QuitRequest(string name)
    {
        Debug.Log("Quit requested for:" + name);
        Application.Quit();
    }

    public void ReturnToStart(string Start)
    {
        Debug.Log("Load scene requested for:" + Start);
        SceneManager.LoadSceneAsync(Start, LoadSceneMode.Single);
    }

    public void Win(string Win)
    {
        Debug.Log("Load scene requested for:" + Win);
        SceneManager.LoadSceneAsync(Win, LoadSceneMode.Single);
    }

   
    public void TestScene(string TestScene)
    {
        Debug.Log("Load scene requested for:" + TestScene);
        SceneManager.LoadSceneAsync(TestScene, LoadSceneMode.Single);
    }
}
