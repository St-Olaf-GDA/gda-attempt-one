using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Made and Updated by Andrew
public class SceneChanger : MonoBehaviour
{
	public static SceneChanger instance = new SceneChanger();
	public void ChangeScene()
	{
    //sceneChanger will append this number to the end of
    //the scene it loads, so please consider naming every
    //scene after the level value (ex scene1 for level=1)
		string sceneName = "scene"+ SaveData.instance.data.level.ToString();
		SceneManager.LoadScene (sceneName);
	}
	public void Exit()
	{
		Application.Quit ();
	}
}