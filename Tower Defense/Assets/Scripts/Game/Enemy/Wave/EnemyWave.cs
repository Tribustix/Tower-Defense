﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class EnemyWave {

	public int pathIndex;
	public float startSpawnTimeInSeconds;
	public float timeBetweenSpawnInSeconds = 1f;

	public List<GameObject> listOfEnemies = new List<GameObject>();

}
