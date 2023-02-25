using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

public GameObject[] pauseUI; //index 0: button; index 1: panel

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TransitionScene(int level)
	{
		SceneManager.LoadScene(level);
	}

public void Pause()
{
Time.timeScale = 0;
	pauseUI[0].SetActive(false);
	pauseUI[1].SetActive(true);
}

public void UnPause()
{
    Time.timeScale = 1;
    pauseUI[0].SetActive(true);
    pauseUI[1].SetActive(false);
}

 public void Exit()
{
 
    Application.Quit();

}

}