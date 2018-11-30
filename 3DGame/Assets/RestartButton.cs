using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {
        SceneManager.LoadScene(1); // Main game
    }

    public void GoBackToTitle()
    {
        SceneManager.LoadScene(0); // Title screen
    }
}
