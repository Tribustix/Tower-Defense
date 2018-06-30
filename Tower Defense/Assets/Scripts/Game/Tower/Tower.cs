using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType {
	Stone,
	Fire,
	Ice
}

public class Tower : MonoBehaviour {

	public float attackPower = 3f;
	public float timeBetweenAttacksInSeconds = 1f;
	public float aggroRadius = 15f;
	public int towerLevel = 1;

	public TowerType type;
	public Transform towerPieceToAim;
	public Enemy targetEnemy = null;
	
	private float attackCounter;

	private AudioSource audioSource;

	
	public virtual void Update () {

		attackCounter -= Time.deltaTime;

		if(targetEnemy == null){

			if(towerPieceToAim){
				SmoothlyLookAtTarget(towerPieceToAim.transform.position - new Vector3(0, 0, 1));
			} 

			if(GetNearestEnemyInRange() != null && Vector3.Distance(transform.position, GetNearestEnemyInRange().transform.position) <= aggroRadius){
				
				targetEnemy = GetNearestEnemyInRange();
			}

		}else{

			if(towerPieceToAim){
				SmoothlyLookAtTarget(targetEnemy.transform.position);
			}

			if(attackCounter <= 0f){
				AttackEnemy(type);

				attackCounter = timeBetweenAttacksInSeconds;
			}

			if(Vector3.Distance(transform.position, targetEnemy.transform.position) > aggroRadius){
				targetEnemy = null;
			}

		}

	}


	void SmoothlyLookAtTarget (Vector3 target) {
		towerPieceToAim.localRotation = UtilityMethods.SmoothyLook(towerPieceToAim, target);
	}

	protected virtual void AttackEnemy (TowerType type) {

		switch(type){
			case TowerType.Stone:
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.stoneTowerAttack, .45f);
			break;
			case TowerType.Ice:
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.iceTowerAttack, .45f);
			break;
			case TowerType.Fire:
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.fireTowerAttack, .45f);
			break;
		}
	}

	public List<Enemy> GetEnemiesInAggroRange () {
		List<Enemy> enemiesInRange = new List<Enemy>();

		foreach (Enemy enemy in EnemyManager.Instance.Enemies){

			if(IsEnemyInRange(enemy, aggroRadius)){
				enemiesInRange.Add(enemy);
			}
		}

		return enemiesInRange;
	}

	public Enemy GetNearestEnemyInRange () {
		
		Enemy nearestEnemy = null;
		float smallestDistance = float.PositiveInfinity;

		foreach (Enemy enemy in EnemyManager.Instance.Enemies){
			
			if(IsEnemyInRange(enemy, smallestDistance)){
				smallestDistance = Vector3.Distance(transform.position, enemy.transform.position);
				nearestEnemy = enemy;
			}

		}

		return nearestEnemy;
	}

	public bool IsEnemyInRange (Enemy enemy, float distance) {
		
		if(Vector3.Distance(transform.position, enemy.transform.position) <= distance){
			return true;
		}
		
		return false;

	}

	public void LevelUp () {
		towerLevel++;
		attackPower *= 2;
		timeBetweenAttacksInSeconds *= .7f;
		aggroRadius *= 1.2f;
	}

	public void ShowTowerInfo () {
		UIManager.Instance.ShowTowerInfoWindow(this);
	}

	public void ShowRemoveTowerInfo () {
		UIManager.Instance.ShowRemoveTowerInfoWindow(this);
	}

	public void DestroyTower () {
		Destroy(gameObject);
	}


}
