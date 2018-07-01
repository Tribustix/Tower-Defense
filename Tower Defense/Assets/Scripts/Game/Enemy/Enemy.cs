using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float maxHealth;
	public float health;
	private float moveSpeed;
	public float secondsEnemyStaysFrozen = 2f;

	private float freezeTimer;

	private int goldDrop;
	public int pathIndex = 0;
	public int wayPointIndex = 0;

	public bool frozen;

	void Start () {

		SetEnemyByDifficulty();
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

	void SetEnemyByDifficulty () {

		

		if(PlayerPrefs.HasKey("Difficulty")){

			switch(PlayerPrefs.GetInt("Difficulty")){
			
			// Difficulty Easy

			case 0:

				SetEnemyValues(this.tag, 100f, 2f, 85);
				


			break;
			
			// Difficulty Medium
			case 1:

				SetEnemyValues(this.tag, 150, 3f, 170);
				

			break;

			// Difficulty Hard

			case 2:

				SetEnemyValues(this.tag, 300, 3.5f, 200);
				

			break;	 

			}
			
		}else{
			Debug.Log("Pa casa");
		}
		


	}

	void SetEnemyValues (string type,  float maxHealthVal, float moveSpeedVal, int goldDropVal) {
		
		switch(type){

					case "Fast":

						maxHealth = maxHealthVal - (maxHealth * 25 / 100);

						health = maxHealth;

						moveSpeed = moveSpeedVal + (moveSpeedVal * 50 / 100);

						goldDrop = goldDropVal + (goldDropVal * 25 / 100);
				
					break;

					case "Normal":

						maxHealth = maxHealthVal;

						health = maxHealth;

						moveSpeed = moveSpeedVal;

						goldDrop = goldDropVal;



					break;

					case "Tank":

						maxHealth = maxHealthVal + (maxHealthVal * 80 / 100);

						health = maxHealth;

						moveSpeed = moveSpeedVal - (moveSpeedVal * 25 / 100);

						goldDrop = goldDropVal + (goldDropVal * 50 / 100);

					break;
				}	

	}

	
}
