using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Aviso : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Invoke("NextLevel", 4);
	}


	void NextLevel()
	{
		SceneManager.LoadScene(4);
	}
}
