using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoresManager : MonoBehaviour {
	
	public static ScoresManager Instance;

	void Awake () {
		
		Instance = this;
	}

	void Start () {
		
		DisplayScores("Medium");

	}
	
	public void DisplayScores (string difficulty) {

		var scores = UtilityMethods.LoadPreviousScores(difficulty);
		var topThree = scores.OrderByDescending(score => score.score ).Take(3);
		var scoresLabel = GameObject.Find("Scores").GetComponent<Text>();
		scoresLabel.text = "BEST SCORES DIFFICULTY " + difficulty.ToUpper() + "\n" + " \n";
		
		foreach (var score in topThree) {

			scoresLabel.text += score.name + "   " + score.score + "\n";

		} 

	}

}
