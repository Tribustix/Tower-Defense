using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public int gold;
	public int waveNumber;
	public int escapedEnemies;
	public int maxAllowedEscapedEnemies;
	public bool enemySpawningOver;
	public int score = 0;

	public Camera myCamera;

	private DifficultyManager difficulty;

	private bool gameOver;
	
	void Awake	() {
		Instance = this;
		myCamera = Camera.main;

	}
	
	void Start () {

		if(!PlayerPrefs.HasKey("Difficulty")){

			Difficulty.Diff = Difficulty.Difficulties.Hard;
			PlayerPrefs.SetInt("Difficulty", (int)Difficulty.Diff);

		}

		
		PlayerPrefs.SetString("Player", null);

		SetGameValues();

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
		score += gold;
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
	
	public void GoToScores () {
		SceneManager.LoadScene("Scores");
	}

	public void OnEnemyEscape () {
		escapedEnemies++;
		UIManager.Instance.ShowDamage();
		score -= 500;

		if(escapedEnemies == maxAllowedEscapedEnemies) {
			OnGameLose();
		}
	}

	public void SaveScore () {
		string levelDifficuty = GetDifficulty();
		var scores = UtilityMethods.LoadPreviousScores(levelDifficuty);
		var newScore = new ScoresEntry();
		newScore.name = PlayerPrefs.GetString("Player");
		newScore.score = this.score;

		var bFormatter = new BinaryFormatter();

		var filePath = Application.streamingAssetsPath + "/Saved Data/" + levelDifficuty + "_scores.dat";
		
		using  (var file = File.Open(filePath, FileMode.Create)){
			scores.Add(newScore);
			bFormatter.Serialize(file, scores);
		}
	}

	public void SetPlayerName (string player) {

		PlayerPrefs.SetString("Player" , player);

	}

	public string GetDifficulty (){
		
		string difficulty = null;

		switch(PlayerPrefs.GetInt("Difficulty")){

			case 0: 
				difficulty = "Easy";
				break;

			case 1: 
				difficulty = "Medium";
				break;
				
			case 2: 
				difficulty = "Hard";
				break;
		}

		return difficulty;

	}

	public void SetGameValues () {

		switch(GetDifficulty()){

			case "Easy":

			gold = 600;
			maxAllowedEscapedEnemies = 5;
			break;
			
			case "Medium":
			gold = 800;
			maxAllowedEscapedEnemies = 4;
			break;

			case "Hard":
			gold = 1500;
			maxAllowedEscapedEnemies = 3;
			break;

		}


	}

}
