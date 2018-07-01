using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {


	public void SetDiffcultyEasy (){

		Difficulty.Diff = Difficulty.Difficulties.Easy;
		PlayerPrefs.SetInt("Difficulty", (int)Difficulty.Diff);

	}

	public void SetDiffcultyMedium (){

		Difficulty.Diff = Difficulty.Difficulties.Medium;
		PlayerPrefs.SetInt("Difficulty", (int)Difficulty.Diff);

	}

	public void SetDiffcultyHard (){

		Difficulty.Diff = Difficulty.Difficulties.Hard;
		PlayerPrefs.SetInt("Difficulty", (int)Difficulty.Diff);
		
	}


}
