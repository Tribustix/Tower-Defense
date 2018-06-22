using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float maxHealth = 100f;
	public float health = 100f;
	public float moveSpeed = 3f;

	public int goldDrop = 10;
	public int pathIndex = 0;
	public int wayPointIndex = 0;

	void Start () {
		EnemyManager.Instance.RegisterEnemy(this);
	}
	
	
	void Update () {
		
		if(wayPointIndex < WayPointManager.Instance.Paths[pathIndex].WayPoints.Count){
			UpdateMovement();
		}else{
			OnGoToLastWayPoint();
		}

	}

	void OnGoToLastWayPoint () {
		Die();
	}

	void Die () {
		if(gameObject != null){
			EnemyManager.Instance.Unregister(this);
			gameObject.AddComponent<AutoScaler>().scaleSpeed = -2;
			enabled = false;
			Destroy(gameObject, 0.3f);
		}
	}

	public void TakeDamage (float amountOfDamage) {
		health -= amountOfDamage;

		if(health <= 0){
			Die();
		}
	}

	public void UpdateMovement () {
		Vector3 targetPosition = WayPointManager.Instance.Paths[pathIndex].WayPoints[wayPointIndex].position;

		transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

		transform.localRotation = UtilityMethods.SmoothyLook(transform, targetPosition);

		
		// If the enemy is very close to the targetWaypoint, set the next waypoint as target
		if(Vector3.Distance(transform.position, targetPosition) < .1f){
			wayPointIndex++;
		}
	}

	
}
