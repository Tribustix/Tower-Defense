using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

	private void Awake() {

		GameObject [] mainSound = GameObject.FindGameObjectsWithTag("MainMusic");

		if(mainSound.Length > 1){
			Destroy(gameObject);
		}else{
			DontDestroyOnLoad(gameObject);
		}
		
		
	}

	private void DestroyLoop(GameObject[] array){
		for(int i = 0; i<array.Length; i++){
			Destroy(array[i]);
		}
	}
}
