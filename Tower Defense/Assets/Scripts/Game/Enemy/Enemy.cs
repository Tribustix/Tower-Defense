using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float maxHealth = 100f;
	public float health = 100f;
	public float moveSpeed = 3f;
	public float secondsEnemyStaysFrozen = 2f;

	private float freezeTimer;

	public int goldDrop = 10;
	public int pathIndex = 0;
	public int wayPointIndex = 0;

	public bool frozen;

	void Start () {
		EnemyManager.Instance.RegisterEnemy(this);
	}
	
	
	void Update () {
		
		if(wayPointIndex < WayPointManager.Instance.Paths[pathIndex].WayPoints.Count){
			UpdateMovement();
		}else{
			OnGoToLastWayPoint();
		}

		if(frozen){
			freezeTimer += Time.deltaTime;

			if(freezeTimer >= secondsEnemyStaysFrozen){
				Defrost();
			}
		}

	}

	void OnGoToLastWayPoint () {
		Die();
		GameManager.Instance.OnEnemyEscape();
	}

	void Die () {
		if(gameObject != null){
			EnemyManager.Instance.Unregister(this);
			gameObject.AddComponent<AutoScaler>().scaleSpeed = -2;
			enabled = false;
			Destroy(gameObject, 0.3f);
		}
	}

	void DropGold () {
		GameManager.Instance.gold += goldDrop;
	}

	public void TakeDamage (float amountOfDamage) {
		health -= amountOfDamage;

		if(health <= 0){
			Die();
			DropGold();
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

	public void Freeze () {

		if(!frozen) {
			frozen = true;
			moveSpeed /= 2;
		}

	}

	void Defrost () {
		freezeTimer = 0f;
		frozen = false;
		moveSpeed *= 2;
	}

	
}
