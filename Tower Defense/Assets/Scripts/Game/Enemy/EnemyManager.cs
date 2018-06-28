using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour {

	public static EnemyManager Instance;

	public List<Enemy> Enemies = new List<Enemy>();

	void Awake () {
		Instance = this;
	}

	public void RegisterEnemy (Enemy enemy) {
		Enemies.Add(enemy);
		UIManager.Instance.CreateHealthBarForEnemy(enemy);
	}

	public void Unregister (Enemy enemy) {
		Enemies.Remove(enemy);
	}

	// It returns a list of all enemies withing a certain range from the position given. LINQ line.
	public List<Enemy> GetEnemiesInRange(Vector3 position, float range) {
		return Enemies.Where(enemy => Vector3.Distance(position, enemy.transform.position) <= range).ToList();
	}

	public void DestroyAllEnemies () {
		foreach (Enemy enemy in Enemies){
			Destroy(enemy.gameObject);
		}
		Enemies.Clear();
	}

	void Start () {
		
	}
	

	void Update () {
		
	}
}
