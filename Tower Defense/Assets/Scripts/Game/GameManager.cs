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
		
		gameOver = true;

		SoundManager.Instance.PlayOneShot(SoundManager.Instance.gameWin, 1);

		UIManager.Instance.ShowWinScreen();
	}

	void OnGameLose () {
		gameOver =  true;
		
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.gameLose, 1);
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
		UIManager.Instance.ShowDamage();

		if(escapedEnemies == maxAllowedEscapedEnemies) {
			OnGameLose();
		}
	}
}
