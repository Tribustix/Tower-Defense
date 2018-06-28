using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public int gold;
	public int waveNumber;
	public int escapedEnemies;
	public int maxAllowedEscapedEnemies = 5;
	public bool enemySpawningOver;

	public AudioClip gameWinSound;
	public AudioClip gameLoseSound;
	public Camera myCamera;

	private bool gameOver;


	void Awake	() {
		Instance = this;
		myCamera = Camera.main;
		
	}
	
	void Start () {
		
	}
	
	void Update () {

		if(!gameOver && enemySpawningOver) {

			if(EnemyManager.Instance.Enemies.Count == 0){
				OnGameWin();
			}

		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			QuitToTitleScreen();
		}
		
	}

	void OnGameWin () {
		AudioSource.PlayClipAtPoint(gameWinSound, myCamera.transform.position);
		gameOver = true;
		UIManager.Instance.ShowWinScreen();
	}

	void OnGameLose () {
		gameOver =  true;

		AudioSource.PlayClipAtPoint(gameLoseSound, myCamera.transform.position);
		EnemyManager.Instance.DestroyAllEnemies();
		WaveManager.Instance.StopSpawning();
		UIManager.Instance.ShowLoseScreen();
	}

	public void QuitToTitleScreen () {
		SceneManager.LoadScene("Main Menu");
	}

	public void RetryLevel () {
    SceneManager.LoadScene("Game");
  }

	public void OnEnemyEscape () {
		escapedEnemies++;

		if(escapedEnemies == maxAllowedEscapedEnemies) {
			OnGameLose();
		}
	}
}
