using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void StartGame () {
		SceneManager.LoadScene("Game");
	}

	public void GoToOptions () {
		SceneManager.LoadScene("Options");
	}

	public void BackToMainMenu () {
		SceneManager.LoadScene("Main Menu");
	}

	public void QuitGame () {
		Application.Quit();
	}

	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			QuitGame();
		}

	}
}
