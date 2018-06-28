using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	public static WaveManager Instance;
	public List<EnemyWave> enemyWaves = new List<EnemyWave>();
	
	private  float elapsedTime = 0f;
	private EnemyWave activeWave;
	private float spawnCounter = 0f;
	private List<EnemyWave> activatedWaves = new List<EnemyWave>();


	void Awake () {
		Instance = this;
	}

	void Update () {

		elapsedTime += Time.deltaTime;

		SearchForWave();
		UpdateActiveWave();
	}

	void SearchForWave () {
		foreach (EnemyWave enemyWave in enemyWaves){

			if(!activatedWaves.Contains(enemyWave) && enemyWave.startSpawnTimeInSeconds <= elapsedTime){
				activeWave = enemyWave;
				activatedWaves.Add(enemyWave);
				spawnCounter = 0f;
				GameManager.Instance.waveNumber++;

				break;
			}
		}
	}

	void UpdateActiveWave () {

		if(activeWave != null){
			spawnCounter += Time.deltaTime;
		

			if(spawnCounter >= activeWave.timeBetweenSpawnInSeconds){
				spawnCounter = 0f;
			

				//Check that a wave wasn't already started before, and the time spent in the level is past the start time of that wave
				if(activeWave.listOfEnemies.Count != 0){
					GameObject enemy = (GameObject)Instantiate(activeWave.listOfEnemies[0], WayPointManager.Instance.GetSpawnPosition(activeWave.pathIndex), Quaternion.identity);
					enemy.GetComponent<Enemy>().pathIndex = activeWave.pathIndex;
					activeWave.listOfEnemies.RemoveAt(0);
				}else{
					activeWave = null;

					if(activatedWaves.Count == enemyWaves.Count){
						// All waves are over
						GameManager.Instance.enemySpawningOver = true;
					}	
				}
			}
		}	
	}

	public void StopSpawning () {
		elapsedTime = 0;
		spawnCounter = 0;
		activeWave = null;
		activatedWaves.Clear();
		enabled = false;
	}

}
