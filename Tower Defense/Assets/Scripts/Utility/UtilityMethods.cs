using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UtilityMethods{

	public static void MoveUiElementToWorldPosition (RectTransform RectTransform, Vector3 worldPosition) {
		Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);
		RectTransform.position = screenPoint;
	}


	//This method allows GameObjects to rotate and look at a certain position smoothly.
	public static Quaternion SmoothyLook (Transform fromTransform, Vector3 toVector3) {
		
		// This method will stop if the origin point and destination are the same
		if(fromTransform.position == toVector3){
			return fromTransform.localRotation;
		}

		// This blocks stores the current rotation and creates the target rotation for the Transform.
		Quaternion currentRotation = fromTransform.localRotation;
		Quaternion targetRotation = Quaternion.LookRotation(toVector3 - fromTransform.position);

		return  Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 10f);

	}

	public static List<ScoresEntry> LoadPreviousScores (string difficulty) {
		try{
			string levelDifficuty = difficulty;
			var scoresFile = Application.streamingAssetsPath + "/Saved Data/" + levelDifficuty + "_scores.dat";
			using (var stream = File.Open(scoresFile, FileMode.Open)){
				var bin = new BinaryFormatter();
				var scores = (List<ScoresEntry>)bin.Deserialize(stream);
				return scores;
			}
		}catch (IOException ex) {
			Debug.LogWarning("Couldn't load previous scores for : "+ difficulty + ".Exception " + ex.Message);
			return new List<ScoresEntry>();
		}
	}

}
