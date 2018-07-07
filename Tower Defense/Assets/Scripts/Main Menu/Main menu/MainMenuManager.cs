using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenuManager : MonoBehaviour {
	
	void Start () {

		CreateScoresFiles();

	}

	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			QuitGame();
		}

	}

	public void StartGame () {
		SceneManager.LoadScene("Game");
	}

	public void GoToOptions () {
		SceneManager.LoadScene("Options");
	}

	public void GoToScores () {
		SceneManager.LoadScene("Scores");
	}

	public void BackToMainMenu () {
		SceneManager.LoadScene("Main Menu");
	}

	public void QuitGame () {
		Application.Quit();
	}

	

	public void CreateScoresFiles () {
	
		string[] difficulties = new string[] {"Easy", "Medium", "Hard"};

		foreach(var difficulty in difficulties){
			
			var bFormatter = new BinaryFormatter();
			
			var filePath = Application.streamingAssetsPath + "/Saved Data/" + difficulty + "_scores.dat";

			if(!File.Exists(filePath)){
				var scores = UtilityMethods.LoadPreviousScores(difficulty);
				var newScore = new ScoresEntry();
				newScore.name = "Master";
				newScore.score = 0;

				using  (var file = File.Open(filePath, FileMode.Create)){
				scores.Add(newScore);
				bFormatter.Serialize(file, scores);
				}
			}

			

			
		}

		

	}
}
